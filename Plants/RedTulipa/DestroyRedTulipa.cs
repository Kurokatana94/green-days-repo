using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DestroyRedTulipa : MonoBehaviour
{
    public RedTulipa tulipa;
    private WeedsSpawnSystem weeds;

    private void Awake()
    {
        weeds = GameObject.FindGameObjectWithTag("Spawner").GetComponent<WeedsSpawnSystem>();
    }


    void Update()
    {
        if (tulipa.isBye)
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

        if (tulipa.isDead)
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
