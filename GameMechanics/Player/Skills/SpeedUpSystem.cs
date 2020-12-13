using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedUpSystem : MonoBehaviour
{
    private PlayerController player;
    public GameObject icon;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    private void Start()
    {
        icon.SetActive(true);
        player.speedBoost = player.speedMultiplier;
    }
}
