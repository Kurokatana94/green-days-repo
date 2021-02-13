using System.Collections.Generic;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Random = UnityEngine.Random;
using Vector3 = UnityEngine.Vector3;
using GDTools;

public class WeedsSpawnSystem : MonoBehaviour
{
    //List of all variables needed for the script with headers and tooltips to help to set everything in the inspector
    private GameOverSystem gameOver;
    [Header ("Reference to Prefabs")]
    public GameObject evilWeed;
    public GameObject tulipa;
    public GameObject bush;
    public GameObject greenWeed;
    public GameObject bladeWeed;
    public GameObject goldenWeed;

    [Header ("Green Weed Settings")]
    [Tooltip("Shows how many Green Weeds currently on the field (Not to modify!)")]
    public int greenCounter;
    [Tooltip("Max amount able to spawn together")]
    public int maxGreenWeedCounter;
    [Tooltip("CoolDown before next spawn")]
    public float greenGrowCD;
    [Tooltip("Amount of time removed from grow CoolDown duration at each spawn")]
    public float greenGrowSpeedModifier;
    [Tooltip("Cap set for fastest spawn CoolDown")]
    public float fastestGreenGrowSpeed;
    [Tooltip("Preset quantity that spawn at the start of each game")]
    public int greenStartQuantity;

    [Header ("Blade Weed Settings")]
    [Tooltip("Shows how many Blade Weeds currently on the field (Not to modify!)")]
    public int bladeCounter;
    [Tooltip("Max amount able to spawn together")]
    public int maxBladeWeedConter;
    [Tooltip("CoolDown before next spawn")]
    public float bladeGrowCD;
    [Tooltip("Amount of time removed from grow CoolDown duration at each spawn")]
    public float bladeGrowSpeedModifier;
    [Tooltip("Cap set for fastest spawn CoolDown")]
    public float fastestBladeGrowSpeed;
    [Tooltip("Preset quantity that spawn at the start of each game")]
    public int bladeStartQuantity;

    [Header ("Golden Weed Settings")]
    [Tooltip("Shows how many Golden Weeds currently on the field (Not to modify!)")]
    public int goldCounter;
    [Tooltip("Max amount able to spawn together")]
    public int maxGoldenWeedCounter;
    [Tooltip("CoolDown before next spawn")]
    public float goldGrowCD;
    [Tooltip("Amount of time removed from grow CoolDown duration at each spawn")]
    public float goldGrowSpeedModifier;
    [Tooltip("Cap set for fastest spawn CoolDown")]
    public float fastestGoldGrowSpeed;
    [Tooltip("Preset quantity that spawn at the start of each game")]
    public int goldStartQuantity;

    [Header ("Bush Settings")]
    [Tooltip("Shows how many Bushes currently on the field (Not to modify!)")]
    public int bushCounter = 0;
    [Tooltip("Max amount able to spawn together")]
    public int maxBushCounter;
    [Tooltip("CoolDown before next spawn")]
    public float bushGrowCD;
    [Tooltip("Amount of time removed from grow CoolDown duration at each spawn")]
    public float bushGrowSpeedModifier;
    [Tooltip("Cap set for fastest spawn CoolDown")]
    public float fastestBushGrowSpeed;
    [Tooltip("Preset quantity that spawn at the start of each game")]
    public int bushStartQuantity;

    [Header ("Evil Weed Settings")]
    [Tooltip("Shows how many Evil Weeds currently on the field (Not to modify!)")]
    public int evilWeedCounter = 0;
    [Tooltip("Max amount able to spawn together")]
    public int maxEvilWeedCounter;
    [Tooltip("CoolDown before next spawn")]
    public float evilWeedGrowCD;
    [Tooltip("Amount of time removed from grow CoolDown duration at each spawn")]
    public float evilWeedGrowSpeedModifier;
    [Tooltip("Cap set for fastest spawn CoolDown")]
    public float fastestEvilWeedGrowSpeed;
    [Tooltip("Preset quantity that spawn at the start of each game")]
    public int evilStartQuantity;
    
    [Header ("Tulipa Settings")]
    [Tooltip("Shows how many Tuilpas currently on the field (Not to modify!)")]
    public int tulipaCounter = 0;
    [Tooltip("Max amount able to spawn together")]
    public int maxTulipaCounter;
    [Tooltip("CoolDown before next spawn")]
    public float tulipaGrowCD;
    [Tooltip("Amount of time removed from grow CoolDown duration at each spawn")]
    public float tulipaGrowSpeedModifier;
    [Tooltip("Cap set for fastest spawn CoolDown")]
    public float fastestTulipaGrowSpeed;
    [Tooltip("Preset quantity that spawn at the start of each game")]
    public int tulipaStartQuantity;

    //Bools that gives the ok whenever a plant should spawn
    private bool 
        goldCanGrow, bladeCanGrow, greenCanGrow,
        tulipaCanGrow, evilWeedCanGrow, bushCanGrow;

    //Floats used to manage the spawn rate of each plant
    private double 
        evilGrowCDTimer, tulipaGrowCDTimer, bushGrowCDTimer,
        greenGrowCDTimer, goldGrowCDTimer, bladeGrowCDTimer;

    [Header("Spawn area limits")]
    public float minX = -5.92f;
    public float maxX = 5.89f;
    public float minY = -3.6f;
    public float maxY = 2.45f;


    //Spawn methods variables --------
    public Collider2D[] colliders;
    [Tooltip("Max radius for the plants to check if able to spawn or not by checking if the area is already occupied")]
    public float radius;
    [Tooltip("List of all items currently spawned on the game area")]
    public List<Vector3> occupiedSpawnPos;

    private void Awake()
    {
        gameOver = GameObject.FindGameObjectWithTag("GO").GetComponent<GameOverSystem>();
        occupiedSpawnPos = new List<Vector3>();    
    }

    private void Start()
    {
        evilGrowCDTimer = evilWeedGrowCD;
        bladeGrowCDTimer = bladeGrowCD;
        tulipaGrowCDTimer = tulipaGrowCD;
        bushGrowCDTimer = bushGrowCD;
        greenGrowCDTimer = greenGrowCD;
        goldGrowCDTimer = goldGrowCD;

        for (int i = 0; i < evilStartQuantity; i++)
        {
            //SpawnPlant(weed);
            SpawnEvilWeed();
        }

        for (int i = 0; i < bushStartQuantity; i++)
        {
            //SpawnPlant(bush);
            SpawnBush();
        }

        for (int i = 0; i < greenStartQuantity; i++)
        {
            SpawnGreenWeed();
        }

        for (int i = 0; i < bladeStartQuantity; i++)
        {
            SpawnBladeWeed();
        }

        for (int i = 0; i < goldStartQuantity; i++)
        {
            SpawnGoldWeed();
        }
    }

    private void FixedUpdate()
    {
        if (gameOver.isScoreBased || gameOver.isMoraleBased)
        {
            if (!evilWeedCanGrow && evilGrowCDTimer > 0)
            {
                evilGrowCDTimer -= Time.fixedDeltaTime;
            }
            else
            {
                evilWeedCanGrow = true;
                evilGrowCDTimer = evilWeedGrowCD;
            }

            if(!bushCanGrow && bushGrowCDTimer > 0)
            {
                bushGrowCDTimer -= Time.fixedDeltaTime;
            }
            else
            {
                bushCanGrow = true;
                bushGrowCDTimer = bushGrowCD;
            }

            if(!greenCanGrow && greenGrowCDTimer > 0)
            {
                greenGrowCDTimer -= Time.fixedDeltaTime;
            }
            else
            {
                greenCanGrow = true;
                greenGrowCDTimer = greenGrowCD;
            }

            if(!bladeCanGrow && bladeGrowCDTimer > 0)
            {
                bladeGrowCDTimer -= Time.fixedDeltaTime;
            }
            else
            {
                bladeCanGrow = true;
                bladeGrowCDTimer = bladeGrowCD;
            }

            if(!goldCanGrow && goldGrowCDTimer > 0)
            {
                goldGrowCDTimer -= Time.fixedDeltaTime;
            }
            else
            {
                goldCanGrow = true;
                goldGrowCDTimer = goldGrowCD;
            }
        }

        if (!tulipaCanGrow && tulipaGrowCDTimer > 0)
        {
            tulipaGrowCDTimer -= Time.fixedDeltaTime;
        }
        else
        {
            tulipaCanGrow = true;
            tulipaGrowCDTimer = tulipaGrowCD;
        }
    }

    private void Update()
    {
        if (evilWeedCounter < maxEvilWeedCounter && evilWeedCanGrow)
        {
            //SpawnPlant(weed);
            SpawnEvilWeed();
            evilWeedGrowCD -= evilWeedGrowSpeedModifier;
            if (evilWeedGrowCD <= fastestEvilWeedGrowSpeed)
            {
                evilWeedGrowCD = fastestEvilWeedGrowSpeed;
                evilWeedGrowSpeedModifier = 0;
            }
        }

        if (bushCounter < maxBushCounter && bushCanGrow)
        {
            //SpawnPlant(bush);
            SpawnBush();
            bushGrowCD -= bushGrowSpeedModifier;
            if (bushGrowCD <= fastestBushGrowSpeed)
            {
                bushGrowCD = fastestBushGrowSpeed;
                bushGrowSpeedModifier = 0;
            }
        }

        if (tulipaCounter < maxTulipaCounter && tulipaCanGrow)
        {
            //SpawnPlant(tulipa);
            SpawnTulipa();
            tulipaGrowCD -= tulipaGrowSpeedModifier;
            if(tulipaGrowCD <= fastestTulipaGrowSpeed)
            {
                tulipaGrowCD = fastestTulipaGrowSpeed;
                tulipaGrowSpeedModifier = 0;
            }

        }

        if(greenCounter < maxGreenWeedCounter && greenCanGrow)
        {
            SpawnGreenWeed();
            greenGrowCD -= greenGrowSpeedModifier;
            if (greenGrowCD <= fastestGreenGrowSpeed)
            {
                greenGrowCD = fastestGreenGrowSpeed;
                greenGrowSpeedModifier = 0;
            }
        }

        if(bladeCounter < maxBladeWeedConter && bladeCanGrow)
        {
            SpawnBladeWeed();
            bladeGrowCD -= bladeGrowSpeedModifier;
            if(bladeGrowCD <= fastestBladeGrowSpeed)
            {
                bladeGrowCD = fastestBladeGrowSpeed;
                bladeGrowSpeedModifier = 0;
            }
        }

        if(goldCounter < maxGoldenWeedCounter && goldCanGrow)
        {
            SpawnGoldWeed();
            goldGrowCD -= goldGrowSpeedModifier;
            if(goldGrowCD <= fastestGoldGrowSpeed)
            {
                goldGrowCD = fastestGoldGrowSpeed;
                goldGrowSpeedModifier = 0;
            }
        }
    }

    //Make tulipas spawn
    public void SpawnTulipa()
    {
        bool canSpawn = CheckSpawnPoint();
        if(canSpawn)
        {
            Vector3 spawnPos = transform.position;
            Instantiate(tulipa, spawnPos, Quaternion.identity);
            tulipaCounter += 1;
            tulipaCanGrow = false;
        }
        else
        {
            Debug.Log("Tulipa couldn't spawn");
            return;
        }
    }

    //Make bushes spawn
    public void SpawnBush()
    {
        bool canSpawn = CheckSpawnPoint();
        if (canSpawn)
        {
            Vector3 spawnPos = transform.position;
            Instantiate(bush, spawnPos, Quaternion.identity);
            bushCounter += 1;
            bushCanGrow = false;
        }
        else
        {
            Debug.Log("Bush couldn't spawn");
            return;
        }
    }

    // Make weeds spawn
    public void SpawnEvilWeed()
    {
        bool canSpawn = CheckSpawnPoint();
        if (canSpawn)
        {
            Vector3 spawnPos = transform.position;
            Instantiate(evilWeed, spawnPos, Quaternion.identity);
            evilWeedCounter += 1;
            evilWeedCanGrow = false;
        }
        else
        {
            Debug.Log("Evil Weed couldn't spawn");
            return;
        }
    }

    public void SpawnGreenWeed()
    {
        bool canSpawn = CheckSpawnPoint();
        if (canSpawn)
        {
            Vector3 spawnPos = transform.position;
            Instantiate(greenWeed, spawnPos, Quaternion.identity);
            greenCounter += 1;
            greenCanGrow = false;
        }
        else
        {
            Debug.Log("Green Weed couldn't spawn");
            return;
        }
    }

    public void SpawnBladeWeed()
    {
        bool canSpawn = CheckSpawnPoint();
        if (canSpawn)
        {
            Vector3 spawnPos = transform.position;
            Instantiate(bladeWeed, spawnPos, Quaternion.identity);
            bladeCounter += 1;
            bladeCanGrow = false;
        }
        else
        {
            Debug.Log("Blade Weed couldn't spawn");
            return;
        }
    }

    public void SpawnGoldWeed()
    {
        bool canSpawn = CheckSpawnPoint();
        if (canSpawn)
        {
            Vector3 spawnPos = transform.position;
            Instantiate(goldenWeed, spawnPos, Quaternion.identity);
            goldCounter += 1;
            goldCanGrow = false;
        }
        else
        {
            Debug.Log("Golden Weed couldn't spawn");
            return;
        }
    }

    private bool CheckSpawnPoint()
    {
        bool canSpawn = true;
        int safetyNet = 0, maxSafetyNet = 30;
        transform.position = new Vector3(Random.Range(minX, maxX), Random.Range(minY , maxY), 0f);
        if(occupiedSpawnPos != null)
        {
            while(safetyNet < maxSafetyNet)
            {
                foreach (Vector3 item in occupiedSpawnPos)
                {
                    if (Vector3.Distance(item, transform.position) >= radius)
                    {
                        canSpawn = true;
                    }
                    else if (Vector3.Distance(item,transform.position) < radius)
                    {
                        canSpawn = false;
                        break;
                    }
                }

                if (canSpawn)
                {
                    occupiedSpawnPos.Add(transform.position);
                    break;
                }
                else
                {
                    transform.position = new Vector3(Random.Range(minX, maxX), Random.Range(minY, maxY), 0f);
                }

                safetyNet++;

                if(safetyNet >= maxSafetyNet)
                {
                    canSpawn = false;
                    break;
                }
            }
        }
        else
        {
            occupiedSpawnPos[0] = transform.position;
        }

        return canSpawn;
    }    

    private void OnDrawGizmos()
    {
        if (transform.position == null) return;

        Gizmos.DrawWireSphere(transform.position, radius);
    }
}



