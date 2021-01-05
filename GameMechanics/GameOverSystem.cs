using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverSystem : MonoBehaviour
{
    //General variables
    [HideInInspector]public bool gameOver = false;
    [Header("References")]
    public GameObject gameOverMenu;
    public TextMeshProUGUI score;
    public PlayerController player;
    public GameObject nextLevel;
    public GameObject[] stars;

    //Best score variables
    public GameObject crown;
    private bool newBest;

    //Variables used to activate the restart/retry menu whenever the level challenge has been failed
    public GameObject tryAgainMenu;
    private bool isSuccessful; 

    //Data management variables
    public DataManagementSystem data;
    private GameMaster gameMaster;
    private int bestIndex;
    private bool isUpdated = false;

    [Space]
    [Tooltip("To activate if it is the last level of the game (it will dwactivate the next level option at the game over screen)")]
    public bool isLastLevel;

    [Header("Game modes")]
    [Tooltip("Game mode that require to reach a certain amount of points in a given time")]
    public bool isScoreBased;
    [Tooltip("Game mode that require to cut a certain number of plants in a given time")]
    public bool isTimeBased;
    [Tooltip("Game mode that require the player to constantly cut weeds to never run out of morale")]
    public bool isMoraleBased;

    //Bool used in function to check whether it is the first time playing the level or not
    private bool firstTime = true;

    //Bool used to check if the specific level sidequest has been completed
    public bool sideQuestComplete;
    public bool sideQuestAlreadyComplete;

    //Money gained with level completition and different bonuses options
    [Header("Rewards")]
    [Tooltip("Base money gained upon level completition")]
    public int baseMoneyReward;
    public int starBonus;
    public int bestScoreBonus;
    public int firstTimeBonus;
    public int sideQuestBonus;
    private int totalReward;
    
    //Score-based variables
    [Header("Score-based settings")]
    [Tooltip("Required score to achieve before time ends to win the match")]
    public int requiredScore;
    private float showedScore = 0f;
    private float counterSpeed = .8f;
    [Tooltip("Minimum amount of points required to acquire each star")]
    public int oneStarScore, twoStarsScore, threeStarsScore;
    
    //Time-based variables
    [Header("Time-based settings")]
    [Tooltip("Insert the time management gameobject")]
    public CountDownSystem countDown;
    [Tooltip("Required plants to cut to win the match")]
    public int requiredPlants;
    [Tooltip("Modifier that converts the time left before the match end into points")]
    public float pointConvertModifier;
    private double timeTaken;
    [Tooltip("Max amount of time required to acquire each star")]
    public double oneStarTime, twoStarsTime, threeStarsTime;

    //Morale-based variables
    [Header("Morale-based reference")]
    [Tooltip("Insert time management gameobject")]
    public CutnRunSystem cutnRun;
    [Tooltip("Minimum amount of morale required to acquire each star")]
    public int oneStarMorale, twoStarsMorale, threeStarsMorale;

    [Space]
    //Star system variables
    [Tooltip("Shows how many stars  have been acquired in the current level (Not modify!)")]
    public int acquiredStars;
    private int preAcquiredStars;

    private void Awake()
    {
        gameMaster = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
    }

    private void Start()
    {
        bestIndex = SceneManager.GetActiveScene().buildIndex;
        acquiredStars = gameMaster.bestStars[bestIndex];
        preAcquiredStars = acquiredStars;
        CheckIfFirstTime();
    }

    private void Update()
    {
        if (player.plantsKilled >= requiredPlants && isTimeBased)
        {
            gameOver = true;
            isSuccessful = true;
        }

        if (gameOver)
        {
            CheckResult();
            if (isSuccessful)
            {
                timeTaken = countDown.maxTime - countDown.timeLeft;
                GameOver();
                for (int i = 0; i < acquiredStars; i++)
                {
                    stars[i].SetActive(true);
                }
                data.AutoSaveGame();
                player.plantsKilled = 0;
                isUpdated = true;
                if (newBest) crown.SetActive(true);
            }
            else TryAgain();
        }
    }

    //Load the next level
    public void GameStart()
    {        
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        gameOver = false;
    }

    //End the level showing up the gameover window
    private void GameOver()
    {
        gameOverMenu.SetActive(true);

        Time.timeScale = 0f;

        StarEvaluation();

        if (isScoreBased)
        {
            ShowScore();
        }
        else if (isTimeBased)
        {
            ShowTime();
        }
        else if (isMoraleBased)
        {
            ShowMorale();
        }
        else
        {
            Debug.LogError("The level type has not been assigned");
            return;
        }
    }

    //Function that is activated if the level is lost
    private void TryAgain()
    {
        tryAgainMenu.SetActive(true);
        Time.timeScale = 0f;
    }

    //Load the main menu
    public void MainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
        gameOver = false;
    }

    //Quit game
    public void Quit()
    {
        Application.Quit();
    }

    //Restart level
    public void Restart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        gameOver = false;
    }

    //It update the data in the game master gameobject
    private void UpdateGameMaster()
    {
        if (!isUpdated)
        {
            if (isTimeBased) player.playerScore = (int)(countDown.timeLeft * pointConvertModifier);

            if (isMoraleBased) player.playerScore = cutnRun.morale * 100;


            if (gameMaster.bestScores[bestIndex] < player.playerScore)
            {
                gameMaster.bestScores[bestIndex] = player.playerScore;
                newBest = true;
            }

            if (gameMaster.bestTimes[bestIndex] > timeTaken || gameMaster.bestTimes[bestIndex] == 0f)
            {
                gameMaster.bestTimes[bestIndex] = timeTaken;
                newBest = true;
            }

            if (gameMaster.bestMorales[bestIndex] < cutnRun.morale)
            {
                gameMaster.bestMorales[bestIndex] = cutnRun.morale;
                newBest = true;
            }

            if (gameMaster.bestStars[bestIndex] < acquiredStars)
            {
                gameMaster.totalStars += acquiredStars - gameMaster.bestStars[bestIndex];
                gameMaster.bestStars[bestIndex] = acquiredStars;
            }

            TotalReward();

            gameMaster.totalMoney += totalReward;
        }
    }

    //Show the score and best score
    private void ShowScore()
    {
        UpdateGameMaster();

        score.text = ScoreCounter().ToString("0");
            
        if (isLastLevel)
        {
            nextLevel.SetActive(false);
                        
            score.text = ScoreCounter().ToString("0");            
        }
    }

    //Show the time and best time
    private void ShowTime()
    {
        UpdateGameMaster();
        
        score.text = timeTaken.ToString("F2") + "s";
        

        if (isLastLevel)
        {
            nextLevel.SetActive(false);

            score.text = timeTaken.ToString("F2") + "s";
        }
    }

    //Shows morale and best morale
    private void ShowMorale()
    {
        UpdateGameMaster();
                
        score.text = cutnRun.morale.ToString() + "%";

        if (isLastLevel)
        {
            nextLevel.SetActive(false);
            
            score.text = cutnRun.morale.ToString() + "%";            
        }
    }

    //Checks using preset requirement whether the player reached the required points amount to obtain stars or not
    private void StarEvaluation()
    {
        if (acquiredStars < 3)
        {
            if (isScoreBased)
            {
                if (player.playerScore < oneStarScore)
                {
                    return;
                }
                else if (player.playerScore >= oneStarScore && player.playerScore < twoStarsScore && acquiredStars <= 1)
                {
                    acquiredStars = 1;
                }
                else if (player.playerScore >= twoStarsScore && player.playerScore < threeStarsScore && acquiredStars <= 2)
                {
                    acquiredStars = 2;
                }
                else if (player.playerScore > threeStarsScore)
                {
                    acquiredStars = 3;
                }
                else return;
            }
            else if (isTimeBased)
            {
                if (timeTaken > oneStarTime)
                {
                    return;
                }
                else if (timeTaken >= twoStarsTime && timeTaken < oneStarTime && acquiredStars <= 1)
                {
                    acquiredStars = 1;
                }
                else if (timeTaken >= threeStarsTime && timeTaken < twoStarsTime && acquiredStars <= 2)
                {
                    acquiredStars = 2;
                }
                else if (timeTaken < threeStarsTime)
                {
                    acquiredStars = 3;
                }
                else return;
            }
            else if (isMoraleBased)
            {
                if (cutnRun.morale <= oneStarMorale) return;
                else if (cutnRun.morale >= oneStarMorale && cutnRun.morale <= twoStarsMorale) acquiredStars = 1;
                else if (cutnRun.morale > twoStarsMorale && cutnRun.morale <= threeStarsMorale) acquiredStars = 2;
                else if (cutnRun.morale > threeStarsMorale) acquiredStars = 3;
                else return;
            }
            else
            {
                Debug.LogError("The level type has not been assigned");
                return;
            }
        }
        else return;
    }

    //Function that shows the score raising like a counter
    private float ScoreCounter()
    {
        if (showedScore < player.playerScore)
        {
            showedScore += (counterSpeed * Time.unscaledDeltaTime) * player.playerScore;
            if (showedScore >= player.playerScore) showedScore = player.playerScore;
        }
        return showedScore;
    }

    // Function that check whether the level was won or lost
    private void CheckResult()
    {
        if (isMoraleBased)
        {
            if (cutnRun.morale <= 0) isSuccessful = false;
            else isSuccessful = true;
        }
        else if (isTimeBased)
        {
            if (player.plantsKilled < requiredPlants) isSuccessful = false;
        }
        else if (isScoreBased)
        {
            if (player.playerScore < requiredScore) isSuccessful = false;
            else isSuccessful = true;
        }
        else
        {
            Debug.LogError("The level type has not been assigned");
            return;
        }
    }

    //Function that checks the bonuses and add the appropriate rewards to the total
    private void TotalReward()
    {
        totalReward += baseMoneyReward;
        if (acquiredStars > 0) totalReward += (acquiredStars - preAcquiredStars) * starBonus + preAcquiredStars * (starBonus / 10);
        //if (newBest) totalReward += bestScoreBonus;
        if (firstTime) totalReward += firstTimeBonus;
        //if (sideQuestAlreadyComplete) totalReward += sideQuestBonus / 10;
        //else if (sideQuestComplete) totalReward += sideQuestBonus;
    }

    //Functions that checks whther was the first time the level was completed or not
    private void CheckIfFirstTime()
    {
        if (isScoreBased)
        {
            if (gameMaster.bestScores[bestIndex] != 0) firstTime = false;
        }
        else if (isTimeBased)
        {
            if (gameMaster.bestTimes[bestIndex] != 0) firstTime = false;
        }
        else if (isMoraleBased)
        {
            if (gameMaster.bestMorales[bestIndex] != 0) firstTime = false;
        }
        else
        {
            Debug.LogError("The level type has not been assigned");
            return;
        }
    }
}
