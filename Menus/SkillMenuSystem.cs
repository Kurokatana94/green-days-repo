﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillMenuSystem : MonoBehaviour
{
    private GameMaster gameMaster;
    public GameObject[] skillButtons;

    private void Awake()
    {
        gameMaster = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
    }

    private void Start()
    {
        for (int i = 0; i < skillButtons.Length; i++)
        {
            if (gameMaster.haveSkill[i])
            {
                skillButtons[i].GetComponent<IconSelectionButton>().interactable = true;
                skillButtons[i].GetComponent<Button>().interactable = true;
            }
        }
    }

    public void SkillButton(int button)
    {
        for (int i = 0; i < gameMaster.skillActive.Length; i++)
        {
            if (i == button) gameMaster.skillActive[i] = true;
            else gameMaster.skillActive[i] = false;
        }
    }
}