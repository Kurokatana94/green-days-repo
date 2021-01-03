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
    
    //Score-based variables
    [Header("Score-based settings")]
    [Tooltip("Required score to achieve before time ends to win the match")]
    public int requiredScore;
    private float showedScore = 0f;
    private float counterSpeed = .8f;
    
    //Time-based variables
    [Header("Time-based settings")]
    [Tooltip("Insert the time management gameobject")]
    public CountDownSystem countDown;
    [Tooltip("Required plants to cut to win the match")]
    public int requiredPlants;
    [Tooltip("Modifier that converts the time left before the match end into points")]
    public float pointConvertModifier;
    private double timeTaken;

    //Morale-based variables
    [Header("Morale-based reference")]
    [Tooltip("Insert time management gameobject")]
    public CutnRunSystem cutnRun;

    [Space]
    //Star system variables
    [Tooltip("Shows how many stars  have been acquired in the current level (Not modify!)")]
    public int aquiredStars;

    private void Awake()
    {
        gameMaster = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
    }

    private void Start()
    {
        bestIndex = SceneManager.GetActiveScene().buildIndex;
        aquiredStars = gameMaster.bestStars[bestIndex];
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
                for (int i = 0; i < aquiredStars; i++)
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

            gameMaster.totalPoints += player.playerScore;

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

            if (gameMaster.bestStars[bestIndex] < aquiredStars)
            {
                gameMaster.totalStars += aquiredStars - gameMaster.bestStars[bestIndex];
                gameMaster.bestStars[bestIndex] = aquiredStars;
            } 
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

    private void StarEvaluation()
    {
        if (aquiredStars < 3)
        {
            if (isScoreBased)
            {
                if (player.playerScore < requiredScore / 2)
                {
                    return;
                }
                else if (player.playerScore >= requiredScore / 2 && player.playerScore < requiredScore && aquiredStars <= 1)
                {
                    aquiredStars = 1;
                }
                else if (player.playerScore >= requiredScore && player.playerScore < requiredScore * 1.5f && aquiredStars <= 2)
                {
                    aquiredStars = 2;
                }
                else if (player.playerScore > requiredScore * 1.5)
                {
                    aquiredStars = 3;
                }
                else return;
            }
            else if (isTimeBased)
            {
                if (timeTaken > countDown.maxTime)
                {
                    return;
                }
                else if (timeTaken >= countDown.maxTime * 0.6f && timeTaken < countDown.maxTime && aquiredStars <= 1)
                {
                    aquiredStars = 1;
                }
                else if (timeTaken >= countDown.maxTime * 0.4f && timeTaken < countDown.maxTime * 0.9f && aquiredStars <= 2)
                {
                    aquiredStars = 2;
                }
                else if (timeTaken < countDown.maxTime * 0.4f)
                {
                    aquiredStars = 3;
                }
                else return;
            }
            else if (isMoraleBased)
            {
                if (cutnRun.morale <= 0) return;
                else if (cutnRun.morale >= 1 && cutnRun.morale <= 35) aquiredStars = 1;
                else if (cutnRun.morale > 35 && cutnRun.morale <= 85) aquiredStars = 2;
                else if (cutnRun.morale > 85) aquiredStars = 3;
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

    //Funzione che mostra il punteggio come contatore
    private float ScoreCounter()
    {
        if (showedScore < player.playerScore)
        {
            showedScore += (counterSpeed * Time.unscaledDeltaTime) * player.playerScore;
            if (showedScore >= player.playerScore) showedScore = player.playerScore;
        }
        return showedScore;
    }

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
}
