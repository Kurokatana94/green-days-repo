using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HubTutorialsConditions : MonoBehaviour
{
    private GameMaster gameMaster;

    private void Awake()
    {
        gameMaster = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
    }

    private void Start()
    {
        if (gameMaster.tutorial[10])
        {
            transform.GetChild(1).gameObject.SetActive(true);
        }    
    }
}
