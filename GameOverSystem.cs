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
    public bool gameOver = false;
    public GameObject gameOverMenu;
    public TextMeshProUGUI score;
    public TextMeshProUGUI best;
    public PlayerController player;
    public GameObject nextLevel;
    public GameObject restartLevel;
    public bool isLastLevel;
    public bool isScoreBased, isTimeBased, isMoraleBased;
    
    //Score-based variables
    public int requiredScore;
    private float showedScore = 0f;
    private float counterSpeed = .8f;
    
    //Time-based variables
    public CountDownSystem countDown;
    public int requiredPlants;
    public float pointConvertModifier;
    private float timeTaken;

    //Morale-based variables
    public CutnRunSystem cutnRun;

    //Data management variables
    public DataManagementSystem data;
    private GameMaster gameMaster;
    private int bestIndex;
    private bool isUpdated = false;

    //Star system variables
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
        }

        if (gameOver)
        {
            timeTaken = countDown.maxTime - countDown.timeLeft;
            GameOver();
            data.AutoSaveGame();
            player.plantsKilled = 0;
            isUpdated = true;
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
            BestScore();
        }
        else if (isTimeBased)
        {
            BestTime();
        }
        else if (isMoraleBased)
        {
            BestMorale();
        }
        else
        {
            Debug.LogError("The level type has not been assigned");
            return;
        }
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

            if (gameMaster.bestScores[bestIndex] <= player.playerScore)
            {
                gameMaster.bestScores[bestIndex] = player.playerScore;
            }

            if (gameMaster.bestTimes[bestIndex] >= timeTaken || gameMaster.bestTimes[bestIndex] == 0f)
            {
                gameMaster.bestTimes[bestIndex] = timeTaken;
            }

            if (gameMaster.bestMorales[bestIndex] <= cutnRun.morale)
            {
                gameMaster.bestMorales[bestIndex] = cutnRun.morale;
            }

            if (gameMaster.bestStars[bestIndex] <= aquiredStars)
            {
                gameMaster.totalStars += aquiredStars - gameMaster.bestStars[bestIndex];
                gameMaster.bestStars[bestIndex] = aquiredStars;
            } 
        }
    }

    //Show the score and best score
    private void BestScore()
    {
        UpdateGameMaster();

        if (player.playerScore >= requiredScore)
        {
            score.text = "Congratulations!!\nYour Score Is:\n\n" + ScoreCounter().ToString("0");
            nextLevel.SetActive(true);
        }
        else
        {
            score.text = "Try Again!!\nYour Score Is:\n" + ScoreCounter().ToString("0");
            restartLevel.SetActive(true);
        }
    
        if (isLastLevel)
        {
            nextLevel.SetActive(false);
            restartLevel.SetActive(false);

            if (player.playerScore >= requiredScore)
            {
                score.text = "Congrats!! You Finished our Game!\nYour Score Is:\n" + player.playerScore;
            }
        }

        best.text = "Best Score: " + gameMaster.bestScores[bestIndex].ToString();
    }

    //Show the time and best time
    private void BestTime()
    {
        UpdateGameMaster();

        if (countDown.timeLeft > 0)
        {
            score.text = "Congratulations!!\nYour Time Is:\n\n" + timeTaken.ToString("F2");
            nextLevel.SetActive(true);
        }
        else
        {
            score.text = "Try Again!!\nCut Them Faster!";
            restartLevel.SetActive(true);
        }

        if (isLastLevel)
        {
            nextLevel.SetActive(false);
            restartLevel.SetActive(false);

            if (countDown.timeLeft > 0f)
            {
                score.text = "Congrats!! You Finished our Game!\nYour Time Is:\n" + timeTaken.ToString("F2");
            }
        }

        best.text = "Best Time: " + gameMaster.bestTimes[bestIndex].ToString("F2");
    }

    //Shows morale and best morale
    private void BestMorale()
    {
        UpdateGameMaster();

        if(cutnRun.morale > 0)
        {
            score.text = "Congrats!! You Managed to Complete Your Task!\nYour Morale Is:\n" + cutnRun.morale.ToString();
            nextLevel.SetActive(true);
        }
        else
        {
            score.text = "Try Again!! Your Morale Is Not Strong Enough!";
            restartLevel.SetActive(true);
        }

        if (isLastLevel)
        {
            nextLevel.SetActive(false);
            restartLevel.SetActive(false);
            if (cutnRun.morale > 0)
            {
                score.text = "Congrats!! You Finished our game!\nYour Morale Is:\n" + cutnRun.morale.ToString();
            }
        }
        best.text = "Best Morale: " + gameMaster.bestMorales[bestIndex].ToString() + "%";
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
}
