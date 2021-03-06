﻿using System.Collections.Generic;
using System;

//Class for creating save files of game information
[Serializable()]
public class GameData
{
    public string name;
    public double timePlayed;

    public int totalPoints;
    public int totalMoney;
    public int totalStars;
    public int acquiredStars;

    public List<double> bestTimes = new List<double>();
    public List<int> bestScores = new List<int>();
    public List<int> bestMorales = new List<int>();
    public List<int> bestStars = new List<int>();
    public List<bool> sideQuestsCompleted = new List<bool>();

    public bool[] haveSkin = new bool[6];
    public bool[] skinActive = new bool[6];

    public bool[] haveSkill = new bool[6];
    public bool[] skillActive = new bool[6];

    public bool[] tutorial = new bool[15];

    public GameData(GameMaster gameMaster)
    {
        totalPoints = gameMaster.totalPoints; 
        totalMoney = gameMaster.totalMoney;
        totalStars = gameMaster.totalStars;
        acquiredStars = gameMaster.acquiredStars;

        bestScores = gameMaster.bestScores;
        bestTimes = gameMaster.bestTimes;
        bestMorales = gameMaster.bestMorales;
        bestStars = gameMaster.bestStars;
        sideQuestsCompleted = gameMaster.sideQuestsCompleted;

        haveSkin = gameMaster.haveSkin;
        haveSkill = gameMaster.haveSkill;

        skinActive = gameMaster.skinActive;
        skillActive = gameMaster.skillActive;

        tutorial = gameMaster.tutorial;

        name = gameMaster.name;
        timePlayed = gameMaster.timePlayed;
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

