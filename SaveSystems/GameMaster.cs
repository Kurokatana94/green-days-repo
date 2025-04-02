using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//Class used to store informations on the current game played
public class GameMaster : MonoBehaviour
{
    private static GameMaster instance;
    private bool isReset = true;
    public bool isMainMenuActive = false;

    //Save Slot data variables
    public string name;
    public double timePlayed;

    // GameData variables to store
    public int totalPoints;
    public int totalMoney;
    public int totalStars = 20;
    public List<double> bestTimes = new List<double>();
    public List<int> bestScores = new List<int>();
    public List<int> bestMorales = new List<int>();
    public List<int> bestStars = new List<int>();
    public List<bool> sideQuestsCompleted = new List<bool>();

    //StartData for checking availables save files
    public int lastSlot;
    public int totalSlots;
    public GameObject[] slots;
    public int acquiredStars;

    //Items variables to check if acquired and activated
    public bool[] haveSkin = new bool[6];
    public bool[] skinActive = new bool[6];
    public bool[] haveSkill = new bool[6];
    public bool[] skillActive = new bool[6];

    //Variables needed to keep track of the storyline and tutorial
    public bool[] tutorial = new bool[10];

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(instance);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void FixedUpdate()
    {
        if(SceneManager.GetActiveScene().buildIndex != 0)
        {
            timePlayed += Time.fixedDeltaTime;
        }
    }

    private void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0 && isReset)
        {
            ResetVariables();
            isReset = false;
        }
        else if (SceneManager.GetActiveScene().buildIndex != 0)
        {
            isReset = true;
        }
    }

    private void ResetVariables()
    {
        // List of all GameData variables to reset
        name = null;
        totalMoney = 0;
        totalPoints = 0;
        acquiredStars = 0;
        timePlayed = 0;

        skinActive[0] = true;
        for (int i = 1; i < skinActive.Length; i++)
        {
            skinActive[i] = false;
        }

        haveSkin[0] = true;
        for (int i = 1; i < haveSkin.Length; i++)
        {
            haveSkin[i] = false;
        }

        for (int i = 0; i < skillActive.Length; i++)
        {
            skillActive[i] = false;
        }

        for (int i = 0; i < haveSkill.Length; i++)
        {
            haveSkill[i] = false;
        }

        for (int i = 0; i < bestScores.Count; i++)
        {
            bestScores[i] = 0;
        }

        for (int i = 0; i < bestTimes.Count; i++)
        {
            bestTimes[i] = 0;
        }

        for (int i = 0; i < bestMorales.Count; i++)
        {
            bestMorales[i] = 0;
        }

        for (int i = 0; i < sideQuestsCompleted.Count; i++)
        {
            sideQuestsCompleted[i] = false;
        }

        for (int i = 0; i < tutorial.Length; i++)
        {
            tutorial[i] = false;
        }
    }
}
