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
    public GameObject rewardStars, rewardBest, rewardFirstTime, rewardSideQuest, reward;
    private LevelLoader loader;

    //Best score variables
    public GameObject crown;
    private bool newBest;

    //Variables used to activate the restart/retry menu whenever the level challenge has been failed
    public GameObject tryAgainMenu;
    private bool isSuccessful; 

    //Data management variables
    private DataManagementSystem data;
    private GameMaster gameMaster;
    private int bestIndex;
    private bool isUpdated = false;

    [Space]
    [Tooltip("To activate if it is the last level of the game (it will deactivate the next level option at the game over screen)")]
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
    [HideInInspector]
    public bool sideQuestComplete;

    //Money gained with level completition and different bonuses options
    [Header("Rewards")]
    [Tooltip("Base money gained upon level completition")]
    public int baseMoneyReward;
    [Tooltip("Bonus reward gained for each new star obtained (if a star will be obtained a second time, the reward will be the same devided by 10)")]
    public int starBonus;
    [Tooltip("Bonus reward gained for creating a new best score")]
    public int bestScoreBonus;
    [Tooltip("Bonus reward gained upon first completition of the current level")]
    public int firstTimeBonus;
    [Tooltip("Bonus reward gained whether the player will complete the current level sidequest (if a sidequest will be completed a second time," +
        " the reward will be the same devided by 10)")]
    public int sideQuestBonus;
    private int totalReward;
    
    //Score-based variables
    [Header("Score-based settings")]
    [Tooltip("Required score to achieve before time ends to win the match")]
    public int requiredScore;
    private float showedReward = 0f;
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
        data = GameObject.FindGameObjectWithTag("Data").GetComponent<DataManagementSystem>();
        gameMaster = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
        loader = GameObject.FindGameObjectWithTag("Loader").GetComponent<LevelLoader>();
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
                if (isTimeBased)
                {
                    timeTaken = countDown.maxTime - countDown.timeLeft;
                }
                GameOver();
                for (int i = 0; i < acquiredStars; i++)
                {
                    stars[i].SetActive(true);
                }
                data.AutoSaveGame();
                player.plantsKilled = 0;
                if (newBest) crown.SetActive(true);
            }
            else TryAgain();
        }
    }

    //Load the next level
    public void GameStart()
    {        
        Time.timeScale = 1f;
        loader.LoadLevel(SceneManager.GetActiveScene().buildIndex + 1);
        gameOver = false;
        gameObject.SetActive(false);
    }

    //End the level showing up the gameover window
    private void GameOver()
    {
        gameOverMenu.SetActive(true);

        Time.timeScale = 0f;

        StarEvaluation();

        ShowReward();

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
            if (isScoreBased)
            {
                if (gameMaster.bestScores[bestIndex] < player.playerScore)
                {
                    gameMaster.bestScores[bestIndex] = player.playerScore;
                    newBest = true;
                }
            }

            if (isTimeBased)
            {
                player.playerScore = (int)(countDown.timeLeft * pointConvertModifier);
                if (gameMaster.bestTimes[bestIndex] > timeTaken || gameMaster.bestTimes[bestIndex] == 0f)
                {
                    gameMaster.bestTimes[bestIndex] = timeTaken;
                    newBest = true;
                }
            }

            if (isMoraleBased)
            {
                player.playerScore = cutnRun.morale * 100;
                if (gameMaster.bestMorales[bestIndex] < cutnRun.morale)
                {
                    gameMaster.bestMorales[bestIndex] = cutnRun.morale;
                    newBest = true;
                }
            }

            if (gameMaster.bestStars[bestIndex] < acquiredStars)
            {
                gameMaster.acquiredStars += acquiredStars - gameMaster.bestStars[bestIndex];
                gameMaster.bestStars[bestIndex] = acquiredStars;
            }

            TotalReward();

            gameMaster.totalMoney += totalReward;

            isUpdated = true;
        }
    }

    private void ShowReward()
    {
        UpdateGameMaster();

        reward.GetComponentInChildren<TextMeshProUGUI>().text = RewardCounter().ToString("0");

        if (isLastLevel)
        {
            nextLevel.SetActive(false);

            reward.GetComponentInChildren<TextMeshProUGUI>().text = RewardCounter().ToString("0");
        }
    }

    //Show the score and best score
    private void ShowScore()
    {
        score.text = player.playerScore.ToString("0");
            
        if (isLastLevel)
        {
            nextLevel.SetActive(false);     
        }
    }

    //Show the time and best time
    private void ShowTime()
    {        
        score.text = timeTaken.ToString("F2") + "s";
        

        if (isLastLevel)
        {
            nextLevel.SetActive(false);
        }
    }

    //Shows morale and best morale
    private void ShowMorale()
    {                
        score.text = cutnRun.morale.ToString() + "%";

        if (isLastLevel)
        {
            nextLevel.SetActive(false);
        }
    }

    //Checks using preset requirement whether the player reached the required points amount to obtain stars or not
    private void StarEvaluation()
    {
        Debug.Log("Star Evaluating");
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
                else if (player.playerScore >= threeStarsScore)
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
    private float RewardCounter()
    {
        if (showedReward < totalReward)
        {
            showedReward += (counterSpeed * Time.unscaledDeltaTime) * totalReward;
            if (showedReward >= totalReward) showedReward = totalReward;
        }
        return showedReward;
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
        if (acquiredStars > 0)
        {
            int starReward = (acquiredStars - preAcquiredStars) * starBonus + preAcquiredStars * (starBonus / 10);
            totalReward += starReward;
            rewardStars.SetActive(true);
            rewardStars.transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = starReward.ToString();
            for (int i = 0; i < acquiredStars; i++)
            {
                rewardStars.transform.GetChild(i).gameObject.SetActive(true);
            }
        }

        if (newBest)
        {
            totalReward += bestScoreBonus;
            rewardBest.SetActive(true);
            rewardBest.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = bestScoreBonus.ToString();
        }

        if (firstTime)
        {
            totalReward += firstTimeBonus;
            rewardFirstTime.SetActive(true);
            rewardFirstTime.transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = firstTimeBonus.ToString();
        }

        if (sideQuestComplete && gameMaster.sideQuestsCompleted[bestIndex])
        {
            totalReward += sideQuestBonus / 10;
            rewardSideQuest.SetActive(true);
            rewardSideQuest.transform.GetComponentInChildren<TextMeshProUGUI>().text = (sideQuestBonus/10).ToString();
        }
        else if (sideQuestComplete)
        {
            totalReward += sideQuestBonus;
            rewardSideQuest.SetActive(true);
            rewardSideQuest.transform.GetComponentInChildren<TextMeshProUGUI>().text = sideQuestBonus.ToString();
            gameMaster.sideQuestsCompleted[bestIndex] = true;
        }
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
