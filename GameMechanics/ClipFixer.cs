using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClipFixer : MonoBehaviour
{
    [Header("References")]
    [Tooltip("Transform to consider as the center of the anti-clip area")]
    public Transform areaCenter;
    [Space]
    [Tooltip("Range of the area to check for clip fixing")]
    public float areaRange;

    private void Update()
    {
        CheckIfClipping();
    }

    private void CheckIfClipping()
    {
        Collider2D[] items = Physics2D.OverlapCircleAll(areaCenter.position, areaRange);

        foreach(Collider2D item in items)
        {
            if(item.gameObject != gameObject)
            {
                if(item.transform.position.y > areaCenter.position.y && item.GetComponent<SpriteRenderer>().sortingOrder >= gameObject.GetComponent<SpriteRenderer>().sortingOrder)
                {
                    item.GetComponent<SpriteRenderer>().sortingOrder = gameObject.GetComponent<SpriteRenderer>().sortingOrder - 1;
                }
                else if(item.transform.position.y < areaCenter.position.y && item.GetComponent<SpriteRenderer>().sortingOrder <= gameObject.GetComponent<SpriteRenderer>().sortingOrder)
                {
                    item.GetComponent<SpriteRenderer>().sortingOrder = gameObject.GetComponent<SpriteRenderer>().sortingOrder + 1;
                }
            }
        }
    }

    //Small funciotn used to check the clip area in edit mode
    private void OnDrawGizmosSelected()
    {
        if (areaCenter.position == null) return;

        Gizmos.DrawWireSphere(areaCenter.position, areaRange);
    }
}
