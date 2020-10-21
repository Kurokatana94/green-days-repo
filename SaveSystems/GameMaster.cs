using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEditorInternal.VersionControl;
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
    public DateTime date;
    public int timePlayed;

    // GameData variables to store
    public int totalPoints;
    public int totalMoney;
    public int totalStars = 9;
    public List<float> bestTimes = new List<float>();
    public List<int> bestScores = new List<int>();
    public List<int> bestMorales = new List<int>();
    public List<int> bestStars = new List<int>();

    //StartData for checking availables save files
    public int lastSlot;
    public int totalSlots;
    public GameObject[] slots;
    public int acquiredStars;

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
        totalMoney = 0;
        totalPoints = 0;
        acquiredStars = 0;

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
    }
}
