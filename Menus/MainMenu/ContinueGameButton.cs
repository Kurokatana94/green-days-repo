using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class ContinueGameButton : MonoBehaviour
{
    private GameMaster gameMaster;

    private void Awake()
    {
        gameMaster = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
    }

    private void Start()
    {
        if (gameMaster.lastSlot == 0 || !File.Exists(Application.persistentDataPath + "/data" + gameMaster.lastSlot + ".gd"))
        { 
            gameObject.GetComponent<Button>().interactable = false;
            Debug.Log("Continue button disabled");
        }
    }
}
