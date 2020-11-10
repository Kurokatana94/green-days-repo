using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HubSystem : MonoBehaviour
{
    public TextMeshProUGUI moneyTxt, totalStarsTxt, acquiredStarsTxt;
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
        acquiredStarsTxt.text = "" + acquiredStars.ToString();

        totalStars = gameMaster.totalStars;
        totalStarsTxt.text = "" + totalStars.ToString();
    }

    private void Update()
    {
        moneyTxt.text = "" + gameMaster.totalMoney.ToString();
    }
}