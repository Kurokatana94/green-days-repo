using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PowerUpCDManager : MonoBehaviour
{
    public Image boots;
    public Image scythe;
    public Image x2;
    public TextMeshProUGUI powerUpText;
    private PowerUpSpawnSystem powerUp;
    
    private void Start()
    {
        powerUp = GameObject.FindGameObjectWithTag("Player").GetComponent<PowerUpSpawnSystem>();
    }

    private void Update()
    {
        if (powerUp.haveBoots)
        {
            powerUpText.text = "" + powerUp.powerUpCDTimer.ToString("F0");
            boots.enabled = true;
            scythe.enabled = false;
            x2.enabled = false;
        }else if (powerUp.haveScythe)
        {
            powerUpText.text = "" + powerUp.powerUpCDTimer.ToString("F0");
            boots.enabled = false;
            scythe.enabled = true;
            x2.enabled = false;
        }else if (powerUp.haveX2)
        {
            powerUpText.text = "" + powerUp.powerUpCDTimer.ToString("F0");
            boots.enabled = false;
            scythe.enabled = false;
            x2.enabled = true;
        }
        else
        {
            powerUpText.text = "-";
            boots.enabled = false;
            scythe.enabled = false;
            x2.enabled = false;
        }
    }
}
