using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkinMenuSystem : MonoBehaviour
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
        for (int i = 0; i < gameMaster.skinActive.Length; i++)
        {
            if (i == button) gameMaster.skinActive[i] = true;
            else gameMaster.skinActive[i] = false;
        }
    }
}
