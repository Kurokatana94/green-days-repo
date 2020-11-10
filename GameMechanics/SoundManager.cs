using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private PowerUpSpawnSystem player;
    public AudioSource audioPowerUp;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PowerUpSpawnSystem>();
    }

    private void Update()
    {
        if (player.destroyPowerUp)
        {
            audioPowerUp.Play();
        }
    }
}
