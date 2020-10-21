using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpSpawnSystem : MonoBehaviour
{
    public GameObject boots, scythe, x2;
    public AudioSource audio;

    public bool haveBoots = false,
                haveScythe = false,
                haveX2 = false,
                powerUpReady;
    private bool havePowerUp;
    public float powerUpCD,
                 powerUpCDTimer,
                 powerUpSpawnCD,
                 powerUpSpawnCDTimer;


    private int choice;
    public bool destroyPowerUp;
    private int powerUpCounter;
    private float minX = -5.9f, maxX = 5.9f, minY = -4.3f, maxY = 2.2f;

    private void Start()
    {
        powerUpCDTimer = powerUpCD;
        powerUpSpawnCDTimer = powerUpSpawnCD;
        powerUpCounter = 0;
    }

    private void FixedUpdate()
    {
        if (powerUpCounter == 0)
        {
            if (!powerUpReady && powerUpSpawnCDTimer > 0)
            {
                powerUpSpawnCDTimer -= Time.fixedDeltaTime;
            }
            else
            {
                havePowerUp = false;
                powerUpReady = true;
                powerUpSpawnCDTimer = powerUpSpawnCD;
            }

        }

        if (powerUpCDTimer > 0)
        {
            if (haveBoots || haveScythe || haveX2)
            {
                powerUpCDTimer -= Time.fixedDeltaTime;
            }
        }
        else
        {
            haveBoots = false;
            haveScythe = false;
            haveX2 = false;
            powerUpCDTimer = powerUpCD;
            powerUpCounter = 0;
        }

    }

    private void Update()
    {

        if (powerUpReady && havePowerUp == false)
        {
            choice = Random.Range(1, 4);

            if(choice == 1)
            {
                SpawnBoots();
                powerUpReady = false;
            }else if(choice == 2)
            {
                SpawnSchyte();
                powerUpReady = false;
            }else if(choice == 3)
            {
                SpawnX2();
                powerUpReady = false;
            }
            powerUpCounter = 1;
        }

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Boots"))
        {
            haveBoots = true;
            havePowerUp = true;
            destroyPowerUp = true;
        }

        if (other.CompareTag("Scythe"))
        {
            haveScythe = true;
            havePowerUp = true;
            destroyPowerUp = true;
        }

        if (other.CompareTag("x2"))
        {
            haveX2 = true;
            havePowerUp = true;
            destroyPowerUp = true;
        }

    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Boots"))
        {
            destroyPowerUp = false;
        }

        if (other.CompareTag("Scythe"))
        {
            destroyPowerUp = false;
        }

        if (other.CompareTag("x2"))
        {
            destroyPowerUp = false;
        }

    }


    private void SpawnBoots()
    {
        Vector3 powerUpSpawnPos = new Vector3(Random.Range(minX, maxX), Random.Range(minY, maxY), 0f);
        Instantiate(boots, powerUpSpawnPos, Quaternion.identity);
        powerUpReady = false;
    }

    private void SpawnSchyte()
    {
        Vector3 powerUpSpawnPos = new Vector3(Random.Range(minX, maxX), Random.Range(minY, maxY), 0f);
        Instantiate(scythe, powerUpSpawnPos, Quaternion.identity);
        powerUpReady = false;
    }

    private void SpawnX2()
    {
        Vector3 powerUpSpawnPos = new Vector3(Random.Range(minX, maxX), Random.Range(minY, maxY), 0f);
        Instantiate(x2, powerUpSpawnPos, Quaternion.identity);
        powerUpReady = false;
    }
}
