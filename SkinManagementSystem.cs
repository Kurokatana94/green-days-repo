﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkinManagementSystem : MonoBehaviour
{
    private GameMaster gameMaster;
    public Button[] skinButtons;

    private void Awake()
    {
        gameMaster = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
    }

    private void Start()
    {
        for (int i = 0; i < skinButtons.Length; i++)
        {
            if (gameMaster.haveSkin[i]) skinButtons[i].interactable = true;
        }
    }

    public void SkinButton(int button)
    {
        for (int i = 0; i < gameMaster.skin.Length; i++)
        {
            if (i == button) gameMaster.skin[i] = true;
            else gameMaster.skin[i] = false;
        }
    }
}
