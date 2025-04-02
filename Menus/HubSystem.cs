using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HubSystem : MonoBehaviour
{
    public TextMeshProUGUI moneyTxt, acquiredStarsTxt;
    private int totalStars, acquiredStars;
    private GameMaster gameMaster;
    private void Awake()
    {
        gameMaster = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
    }

    private void Start()
    {
        foreach (int stars in gameMaster.bestStars)
        {
            acquiredStars += stars;
        }
        gameMaster.acquiredStars = acquiredStars;
        totalStars = gameMaster.totalStars;

        acquiredStarsTxt.text = acquiredStars.ToString() + " / " + totalStars.ToString();
    }

    private void Update()
    {
        moneyTxt.text = "" + gameMaster.totalMoney.ToString();
    }
}