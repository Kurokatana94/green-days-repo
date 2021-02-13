using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldenWeedTutorial : MonoBehaviour
{
    private GameObject tutorial;

    private void Awake()
    {
        tutorial = transform.GetChild(1).gameObject;
    }

    private void Update()
    {
        if(GameObject.Find("GoldenWeed(Clone)") != null)
        {
            tutorial.SetActive(true);
            this.enabled = false;
        }
    }
}
