using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillSlotUpdate : MonoBehaviour
{
    private GameMaster gameMaster;
    public GameObject description;

    private void Awake()
    {
        gameMaster = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
    }

    private void Start()
    {
        if (gameObject.GetComponent<Button>().interactable)
        {
            gameObject.GetComponent<Button>().interactable = false;
        }
    }

    private void Update()
    {
        for (int i = 0; i < gameMaster.skillActive.Length; i++)
        {
            if (gameMaster.skillActive[i])
            {
                if (!gameObject.GetComponent<Button>().interactable)
                {
                    gameObject.GetComponent<Button>().interactable = true;
                }
                gameObject.transform.GetChild(i).gameObject.SetActive(true);
                description.transform.GetChild(i).gameObject.SetActive(true);
                description.GetComponent<Image>().enabled = false;
            }
            else
            {
                gameObject.transform.GetChild(i).gameObject.SetActive(false);
                description.transform.GetChild(i).gameObject.SetActive(false);
            }
        }
    }

    public void ResetSlot()
    {
        for (int i = 0; i < gameObject.transform.childCount-1; i++)
        {
            gameObject.transform.GetChild(i).gameObject.SetActive(false);
            gameMaster.skillActive[i] = false;
        }
        description.transform.GetChild(gameObject.transform.childCount - 1).gameObject.SetActive(true);
        gameObject.GetComponent<Button>().interactable = false;
    }
}
