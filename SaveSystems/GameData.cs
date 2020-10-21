﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

//Class for creating save files of game information
[Serializable()]
public class GameData
{
    public string name;
    public DateTime date;
    public int timePlayed;

    public int totalPoints;
    public int totalMoney;
    public int totalStars;
    public List<float> bestTimes = new List<float>();
    public List<int> bestScores = new List<int>();
    public List<int> bestMorales = new List<int>();
    public List<int> bestStars = new List<int>();

    public GameData(GameMaster gameMaster)
    {
        totalPoints = gameMaster.totalPoints; 
        totalMoney = gameMaster.totalMoney;
        totalStars = gameMaster.totalStars;

        bestScores = gameMaster.bestScores;
        bestTimes = gameMaster.bestTimes;
        bestMorales = gameMaster.bestMorales;
        bestStars = gameMaster.bestStars;
    }
}

//Class for save file containing information needed in the main menú
[Serializable()]
public class StartData
{
    public int totalSlots;
    public int lastSlot;

    public StartData(GameMaster gameMaster)
    {
        totalSlots = gameMaster.totalSlots;
        lastSlot = gameMaster.lastSlot;
    }
}

