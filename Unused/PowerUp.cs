using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    private PowerUpSpawnSystem powerUp;

    private void Start()
    {
        powerUp = GameObject.FindGameObjectWithTag("Player").GetComponent<PowerUpSpawnSystem>();
    }

    private void Update()
    {
        if (powerUp.destroyPowerUp)
        {
            powerUp.destroyPowerUp = false;
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("FreePos") || other.CompareTag("Weed") || other.CompareTag("Bush") || other.CompareTag("Tulipa"))
        {
            powerUp.powerUpReady = true;
            Destroy(gameObject);
        }
    }
}
