using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstTutorial : MonoBehaviour
{
    private GameMaster gameMaster;
    
    private void Awake()
    {
        gameMaster = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();    
    }

    private void Start()
    {
        if (gameMaster.firstTutorialCompleted) gameObject.SetActive(false);
    }

    private void Update()
    {
        
    }
}
