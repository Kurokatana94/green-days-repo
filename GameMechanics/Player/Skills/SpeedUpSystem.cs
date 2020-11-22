using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedUpSystem : MonoBehaviour
{
    private PlayerController player;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    private void Start()
    {
        player.speedBoost = player.speedMultiplier;
    }
}
