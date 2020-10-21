using System.Collections.Generic;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Random = UnityEngine.Random;
using Vector3 = UnityEngine.Vector3;
using GDTools;

public class WeedsSpawnSystem : MonoBehaviour
{
    [Header ("Reference to Prefabs")]
    public GameObject evilWeed;
    public GameObject tulipa;
    public GameObject bush;
    public GameObject greenWeed;
    public GameObject bladeWeed;
    public GameObject goldenWeed;

    [Header ("Green Weed Settings")]
    public int greenCounter;
    public int maxGreenWeedCounter;
    public float greenGrowCD, greenGrowSpeedModifier, fastestGreenGrowSpeed;

    [Header ("Blade Weed Settings")]
    public int bladeCounter;
    public int maxBladeWeedConter;
    public float bladeGrowCD, bladeGrowSpeedModifier, fastestBladeGrowSpeed;

    [Header ("Golden Weed Settings")]
    public int goldCounter;
    public int maxGoldenWeedCounter;
    public float goldGrowCD, goldGrowSpeedModifier, fastestGoldGrowSpeed;

    [Header ("Bush Settings")]
    public int bushCounter = 0;
    public int maxBushCounter;
    public float bushGrowCD, bushGrowSpeedModifier, fastestBushGrowSpeed;

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
    
    [Header ("Tulipa Settings")]
    [Tooltip("Shows how many Tuilpas currently on the field (Not to modify!)")]
    public int tulipaCounter = 0;
    public int maxTulipaCounter;
    public float tulipaGrowCD;

    //Ints that keep track of how many plants of each kind are on the field at any given time
    //(public just to check in development)

    //Ints that keep a cap on how many of each lnt can be spawned at the same time

    //Bools that gives the ok whenever a plant should spawn
    private bool 
        goldCanGrow, bladeCanGrow, greenCanGrow,
        tulipaCanGrow, evilWeedCanGrow, bushCanGrow;

    //Floats used to manage the spawn rate of each plant
    private float 
        evilGrowCDTimer, tulipaGrowCDTimer, bushGrowCDTimer,
        greenGrowCDTimer, goldGrowCDTimer, bladeGrowCDTimer;

    //Ints used to determine with what quantity of each plant the game start
    public int
        evilWeedStartQuantity, bushStartQuantity, greenStartQuantity, bladeStartQuantity, goldStartQuantity;

    public GameOverSystem gameOver;

    private float minX = -5.9f, maxX = 5.9f, minY = -4.3f, maxY = 2.2f;

    //Spawn methods variables --------

    public Collider2D[] colliders;
    public float radius;
    public List<Vector3> occupiedSpawnPos;

    private void Awake()
    {
        occupiedSpawnPos = new List<Vector3>();    
    }

    private void Start()
    {
        for (int i = 0; i < evilWeedStartQuantity; i++)
        {
            //SpawnPlant(weed);
            SpawnEvilWeed();
        }

        for (int i = 0; i < bushStartQuantity; i++)
        {
            //SpawnPlant(bush);
            SpawnBush();
        }
    }

    private void FixedUpdate()
    {
        if (gameOver.isScoreBased)
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
        }

        if (evilWeedGrowCD <= fastestEvilWeedGrowSpeed)
        {
            evilWeedGrowSpeedModifier = 0;
        }

        if (bushCounter < maxBushCounter && bushCanGrow)
        {
            //SpawnPlant(bush);
            SpawnBush();
            bushGrowCD -= bushGrowSpeedModifier;
        }

        if(bushGrowCD <= fastestBushGrowSpeed)
        {
            bushGrowSpeedModifier = 0;
        }

        if (tulipaCounter < maxTulipaCounter && tulipaCanGrow)
        {
            //SpawnPlant(tulipa);
            SpawnTulipa();
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



