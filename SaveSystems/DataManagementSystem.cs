using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class DataManagementSystem : MonoBehaviour
{
    private GameMaster gameMaster;
    public SaveSlotPage saveSlotPage;

    private void Awake()
    {
        gameMaster = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
    }

    //Function called every time we finish a level or complete some important actions and by quitting the game
    public void AutoSaveGame()
    {
        SaveSystem.SaveGame(gameMaster, gameMaster.lastSlot);
        SaveSystem.SaveStartData(gameMaster);
    }

    //Funciotn called when hitting continue button from main menú
    public void ContinueGame()
    {
        gameMaster = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();

        GameData data = SaveSystem.LoadGame(gameMaster.lastSlot);

        //Game data
        gameMaster.bestScores = data.bestScores;
        gameMaster.bestTimes = data.bestTimes;
        gameMaster.bestMorales = data.bestMorales;
        gameMaster.totalMoney = data.totalMoney;
        gameMaster.totalPoints = data.totalPoints;
        gameMaster.totalStars = data.totalStars;
        gameMaster.bestStars = data.bestStars;

        //Checks wheter skins have been unlocked or not
        gameMaster.haveSkin = data.haveSkin;

        //Checks which skin is currently activated
        gameMaster.skin = data.skin;

        //Basic info for the player
        gameMaster.name = data.name;
        gameMaster.date = data.date;
        gameMaster.timePlayed = data.timePlayed;
    }

    //Function called in game when willing to save via save/load option
    public void ManualSaveGame(int slotNumber)
    {
        gameMaster.lastSlot = slotNumber;
        SaveSystem.SaveGame(gameMaster, slotNumber);
        SaveSystem.SaveStartData(gameMaster);
    }

    //Function called via load/save game page in main menu or in game save/load option
    public void ManualLoadGame(int slotNumber)
    {
        gameMaster.lastSlot = slotNumber;
        GameData data = SaveSystem.LoadGame(slotNumber);

        //Game data
        gameMaster.bestScores = data.bestScores;
        gameMaster.bestTimes = data.bestTimes;
        gameMaster.bestMorales = data.bestMorales;
        gameMaster.totalMoney = data.totalMoney;
        gameMaster.totalPoints = data.totalPoints;
        gameMaster.totalStars = data.totalStars;
        gameMaster.bestStars = data.bestStars;

        //Checks wheter skins have been unlocked or not
        gameMaster.haveSkin = data.haveSkin;

        //Checks which skin is currently activated
        gameMaster.skin = data.skin;

        //Basic info for the player
        gameMaster.name = data.name;
        gameMaster.date = data.date;
        gameMaster.timePlayed = data.timePlayed;

        SaveSystem.SaveStartData(gameMaster);
    }

    //Function called when hitting new game button or in game save/load option
    public void NewSaveSlot()
    {
        if(gameMaster.totalSlots >= saveSlotPage.slots.Length)
        {
            Debug.Log("All save slots are currently full.");
            return;
        }
        gameMaster.totalSlots++;
        for(int i = 1; i <= gameMaster.totalSlots; i++)
        {
            string path = Application.persistentDataPath + "/data" + i + ".gd";
            if (!File.Exists(path))
            {
                gameMaster.lastSlot = i;
                SaveSystem.SaveGame(gameMaster, i);
                break;
            }
        }
        SaveSystem.SaveStartData(gameMaster);
    }

    //Function called in start screen and at all scene transitions
    public void LoadStartData()
    {
        StartData data = SaveSystem.LoadStartData();
        gameMaster.lastSlot = data.lastSlot;
        gameMaster.totalSlots = data.totalSlots;


        Debug.Log("StartDataLoaded");
    }

    //Function called in "NewSaveSlot", "ManualSaveGame", "ManualLoadGame", "AutoSaveGame" and by quitting the game
    public void SaveStartData()
    {
        SaveSystem.SaveStartData(gameMaster);
        Debug.Log("StartDataSaved");
    }
}
