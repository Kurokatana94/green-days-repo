using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreSpawnPlantSystem : MonoBehaviour
{
    public GameObject plant;
    public double preSpawnCD;

    private void FixedUpdate()
    {
        preSpawnCD -= Time.fixedDeltaTime;
    }

    private void Update()
    {
        if (preSpawnCD <= 0)
        {
            Instantiate(plant, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
