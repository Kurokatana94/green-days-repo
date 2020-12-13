using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DestroyBush : MonoBehaviour
{
    public Bush bush;
    private WeedsSpawnSystem bushes;

    private void Awake()
    {
        bushes = GameObject.FindGameObjectWithTag("Spawner").GetComponent<WeedsSpawnSystem>();
    }



    private void Update()
    {
        if (bush.isDead == true)
        {
            foreach (Vector3 item in bushes.occupiedSpawnPos.ToList())
            {
                if (item == transform.position)
                {
                    bushes.occupiedSpawnPos.Remove(item);
                    Debug.Log("Vector3 Removed" + item.ToString());
                    break;
                }
            }
            Destroy(gameObject, 1f);
        }
    }

}
