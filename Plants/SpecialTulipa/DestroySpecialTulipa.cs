using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DestroySpecialTulipa : MonoBehaviour
{
    public SpecialTulipa specialTulipa;
    private WeedsSpawnSystem weeds;

    private void Awake()
    {
        weeds = GameObject.FindGameObjectWithTag("Spawner").GetComponent<WeedsSpawnSystem>();
    }


    void Update()
    {
        if (specialTulipa.isBye)
        { 
            foreach (Vector3 item in weeds.occupiedSpawnPos.ToList())
            {
                if (item == transform.position)
                {
                    weeds.occupiedSpawnPos.Remove(item);
                    Debug.Log("Vector3 Removed" + item.ToString());
                    break;
                }
            }
            Destroy(gameObject, .3f);
        }

        if (specialTulipa.isDead)
        {
            foreach (Vector3 item in weeds.occupiedSpawnPos.ToList())
            {
                if (item == transform.position)
                {
                    weeds.occupiedSpawnPos.Remove(item);
                    Debug.Log("Vector3 Removed" + item.ToString());
                    break;
                }
            }
            Destroy(gameObject, 1f);
        }
    }
}