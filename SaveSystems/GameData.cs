using System.Collections;
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
    public List<double> bestTimes = new List<double>();
    public List<int> bestScores = new List<int>();
    public List<int> bestMorales = new List<int>();
    public List<int> bestStars = new List<int>();

    public bool[] haveSkin = new bool[4];
    public bool[] skinActive = new bool[4];

    public bool[] haveSkill = new bool[3];
    public bool[] skillActive = new bool[3];

    public GameData(GameMaster gameMaster)
    {
        totalPoints = gameMaster.totalPoints; 
        totalMoney = gameMaster.totalMoney;
        totalStars = gameMaster.totalStars;

        bestScores = gameMaster.bestScores;
        bestTimes = gameMaster.bestTimes;
        bestMorales = gameMaster.bestMorales;
        bestStars = gameMaster.bestStars;

        haveSkin = gameMaster.haveSkin;
        haveSkill = gameMaster.haveSkill;

        skinActive = gameMaster.skinActive;
        skillActive = gameMaster.skillActive;

        name = gameMaster.name;
        date = gameMaster.date;
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

