﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class SkillsManagementSystem : MonoBehaviour
{
    private GameMaster gameMaster;
    public GameObject[] skills;

    private void Awake()
    {
        gameMaster = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
    }

    private void Start()
    {
        for (int i = 0; i < gameMaster.skillActive.Length; i++)
        {
            if (gameMaster.skillActive[i])
            {
                skills[i].SetActive(true);
            }
            else
            {
                skills[i].SetActive(false);
            }
        }
    }
}
