using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeedSpawnTest : MonoBehaviour
{
    public GameObject weed;
    public GameObject tulipa;
    public GameObject bush;
    public int bushCounter = 0;
    public int maxBushCounter;
    public int tulipaCounter = 0;
    public int weedCounter = 0;
    public int maxTulipaCounter;
    public int maxWeedCounter;

    private bool tulipaCanGrow;
    private bool weedCanGrow;
    private bool bushCanGrow;
    public float weedGrowCD, weedGrowSpeedModifier, fastestWeedGrowSpeed, tulipaGrowCD, bushGrowCD, bushGrowSpeedModifier, fastestBushGrowSpeed;

    private float weedGrowCDTimer, tulipaGrowCDTimer, bushGrowCDTimer;
    public int weedStartQuantity;
    public int bushStartQuantity;

    //Spawn methods variables --------

    public List<RectTransform> spawnPos;
    public int randomSpawnPos;
    public Collider2D[] colliders;
    public float radius;
    public float sideLength;
    List<Vector2> occupiedSpawnPos;

    private void Start()
    {
        for (int i = 0; i < weedStartQuantity; i++)
        {
            //SpawnPlant(weed);
            SpawnWeed();
        }

        for (int i = 0; i < bushStartQuantity; i++)
        {
            //SpawnPlant(bush);
            SpawnBush();
        }
    }


    private void FixedUpdate()
    {
        if (!weedCanGrow && weedGrowCDTimer > 0)
        {
            weedGrowCDTimer -= Time.fixedDeltaTime;
        }
        else
        {
            weedCanGrow = true;
            weedGrowCDTimer = weedGrowCD;
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

        if (!bushCanGrow && bushGrowCDTimer > 0)
        {
            bushGrowCDTimer -= Time.fixedDeltaTime;
        }
        else
        {
            bushCanGrow = true;
            bushGrowCDTimer = bushGrowCD;
        }

    }

    private void Update()
    {
        if (weedCounter < maxWeedCounter && weedCanGrow)
        {
            //SpawnPlant(weed);
            SpawnWeed();
            weedGrowCD -= weedGrowSpeedModifier;
        }

        if (weedGrowCD <= fastestWeedGrowSpeed)
        {
            weedGrowSpeedModifier = 0;
        }

        if (bushCounter < maxBushCounter && bushCanGrow)
        {
            //SpawnPlant(bush);
            SpawnBush();
            bushGrowCD -= bushGrowSpeedModifier;
        }

        if (bushGrowCD <= fastestBushGrowSpeed)
        {
            bushGrowSpeedModifier = 0;
        }

        if (tulipaCounter < maxTulipaCounter && tulipaCanGrow)
        {
            //SpawnPlant(tulipa);
            SpawnTulipa();
        }

    }

    //List check spawn method ---------------------

    /*public void SpawnPlant(GameObject plant)
    {
        occupiedSpawnPos = new List<Vector2>();
        int count = 0;

        while (count < 20)
        {
            float xPos = Random.Range(-10f, 10f);
            float yPos = Random.Range(-4f, 5f);

            bool canSpawn = true;

            for (int i = 0; i < occupiedSpawnPos.Count && canSpawn; i++)
            {
                if(occupiedSpawnPos[i].x < xPos + sideLength && occupiedSpawnPos[i].x + sideLength > xPos &&
                   occupiedSpawnPos[i].y < yPos + sideLength && occupiedSpawnPos[i].y + sideLength > yPos)
                {
                    canSpawn = false;
                }
            }
            if (canSpawn)
            {
                occupiedSpawnPos.Add(new Vector2(xPos, yPos));
                Instantiate(plant, new Vector2(xPos, yPos), Quaternion.identity);
                if(plant == bush)
                {
                    bushCounter += 1;
                    bushCanGrow = false;

                }
                else if(plant == weed)
                {
                    weedCounter += 1;
                    weedCanGrow = false;

                }
                else if(plant == tulipa)
                {
                    tulipaCounter += 1;
                    tulipaCanGrow = false;
                }
                break;
            }
            if(count > 20)
            {
                Debug.Log("noSpawn");
                break;
            }
            count++;
        }
    }*/

    // Grid spawn method ----------------------------------

    public void SpawnTulipa()
    {
        var random = new System.Random();
        int randomSpawnPos = random.Next(spawnPos.Count);
        Instantiate(tulipa, spawnPos[randomSpawnPos].position, Quaternion.identity);
        spawnPos.RemoveAt(randomSpawnPos);
        tulipaCounter += 1;
        tulipaCanGrow = false;

    }
    public void SpawnBush()
    {
        var random = new System.Random();
        int randomSpawnPos = random.Next(spawnPos.Count);
        Instantiate(bush, spawnPos[randomSpawnPos].position, Quaternion.identity);
        spawnPos.RemoveAt(randomSpawnPos);
        bushCounter += 1;
        bushCanGrow = false;

    }
    public void SpawnWeed()
    {
        var random = new System.Random();
        int randomSpawnPos = random.Next(spawnPos.Count);
        Instantiate(weed, spawnPos[randomSpawnPos].position, Quaternion.identity);
        spawnPos.RemoveAt(randomSpawnPos);
        weedCounter += 1;
        weedCanGrow = false;

    }

    //Collider2D Spawn method ---------------------------

    /*
    public void SpawnTulipa()
    {
        Vector3 tulipaSpawnPos = new Vector3(0f, 0f, 0f);
        bool canSpawnHere = false;
        int safetyNet = 0;

        while (!canSpawnHere)
        {
            tulipaSpawnPos = new Vector3(UnityEngine.Random.Range(-10f, 10f), UnityEngine.Random.Range(-4f, 5f), 0f);
            canSpawnHere = PreventSpawnOverLap(tulipaSpawnPos);
            safetyNet++;

            if (canSpawnHere)
            {
                Instantiate(tulipa, tulipaSpawnPos, Quaternion.identity);
                tulipaCounter += 1;
                tulipaCanGrow = false;
                break;
            }

            if (safetyNet > 50)
            {
                Debug.Log("Spawn non riuscito, tulipano");
                break;
            }
        }

    }

    public void SpawnWeed()
    {
        Vector3 weedSpawnPos = new Vector3(0f, 0f, 0f);
        bool canSpawnHere = false;
        int safetyNet = 0;

        while (!canSpawnHere)
        {
            weedSpawnPos = new Vector3(UnityEngine.Random.Range(-10f, 10f), UnityEngine.Random.Range(-4f, 5f), 0f);
            canSpawnHere = PreventSpawnOverLap(weedSpawnPos);
            safetyNet++;

            if (canSpawnHere)
            {
                Instantiate(weed, weedSpawnPos, Quaternion.identity);
                weedCounter += 1;
                weedCanGrow = false;
                break;
            }

            if (safetyNet > 50)
            {
                Debug.Log("Spawn non riuscito, erbetta");
                break;
            }

        }

    }
    
    public void SpawnBush()
    {
        Vector3 bushSpawnPos = new Vector3(0f, 0f, 0f);
        bool canSpawnHere = false;
        int safetyNet = 0;

        while (!canSpawnHere)
        {
            bushSpawnPos = new Vector3(UnityEngine.Random.Range(-10f, 10f), UnityEngine.Random.Range(-4f, 5f), 0f);
            canSpawnHere = PreventSpawnOverLap(bushSpawnPos);
            safetyNet++;

            if (canSpawnHere)
            {
                Instantiate(bush, bushSpawnPos, Quaternion.identity);
                bushCounter += 1;
                bushCanGrow = false;
                break;
            }

            if(safetyNet > 50)
            {
                Debug.Log("Spawn non riuscito, cespuglio");
                break;
            }
        }
    }
    
    bool PreventSpawnOverLap(Vector3 spawnPos)
    {
        colliders = Physics2D.OverlapCircleAll(transform.position, radius);

        for (int i = 0; i < colliders.Length; i++)
        {
            Vector3 centerPoint = colliders[i].bounds.center;
            float width = colliders[i].bounds.extents.x;
            float height = colliders[i].bounds.extents.y;

            float leftExtent = centerPoint.x - width;
            float rightExtent = centerPoint.x + width;
            float lowerExtent = centerPoint.y - height;
            float upperExtent = centerPoint.y + height;

            if (spawnPos.x >= leftExtent && spawnPos.x <= rightExtent)
            {
                if (spawnPos.y >= lowerExtent && spawnPos.y <= upperExtent)
                {
                    return false;
                }
            }
        }
        return true;
    }

    private void OnDrawGizmos()
    {
        if (transform == null) return;

        Gizmos.DrawWireSphere(transform.position, radius);
    }*/

}
