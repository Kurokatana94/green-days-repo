using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;
using UnityEngine.UI;

public class StartScreenSystem : MonoBehaviour
{
    public Animator logo;
    public GameObject pressButtonText;
    public GameObject panel;
    public Button continueButton;
    private GameMaster gameMaster;
    public DataManagementSystem data;

    private void Awake()
    {
        gameMaster = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
    }

    private void Start()
    {
        if (File.Exists(Application.persistentDataPath + "/startData.gd")) data.LoadStartData();
        else data.SaveStartData();

        if (gameMaster.isMainMenuActive)
        {
            logo.enabled = false;
            pressButtonText.SetActive(false);
            panel.SetActive(true);
            panel.GetComponent<Animator>().enabled = false;
            panel.GetComponent<RectTransform>().localPosition = new Vector3(0, 0, 0);
        }

        if (!File.Exists(Application.persistentDataPath + "/data1.gd"))
        {
            continueButton.interactable = false;
        }
    }

    private void Update()
    {
        if (Input.anyKeyDown)
        {
            logo.SetBool("KeyIsPressed", true);
            pressButtonText.GetComponent<Animator>().SetBool("KeyIsPressed", true);
            panel.SetActive(true);
            gameMaster.isMainMenuActive = true;
        }
    }
}
