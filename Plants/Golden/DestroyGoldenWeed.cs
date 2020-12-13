using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DestroyGoldenWeed : MonoBehaviour
{
    public GoldenWeed weed;
    private WeedsSpawnSystem weeds;

    private void Start()
    {
        weeds = GameObject.FindGameObjectWithTag("Spawner").GetComponent<WeedsSpawnSystem>();
    }

    private void Update()
    {
        if(weed.isDead == true)
        {
            foreach (Vector3 item in weeds.occupiedSpawnPos.ToList())
            {
                if (item == transform.position)
                {
                    weeds.occupiedSpawnPos.Remove(item);
                    Debug.Log("Vector3 Removed " + item.ToString());
                    break;
                }
            }
            Destroy(gameObject, 1f);
        }        
    }
}
