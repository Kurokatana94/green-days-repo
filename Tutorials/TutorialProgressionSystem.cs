using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialProgressionSystem : MonoBehaviour
{
    private GameMaster gameMaster;
    [Tooltip("Insert tutorial number to mark it as completed once finished")]
    public int tutorialNumber;
    private int pageNumber = 0;

    private void Awake()
    {
        gameMaster = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
    }

    private void Start()
    {
        Time.timeScale = 0;

        if (gameMaster.tutorial[tutorialNumber])
        {
            gameObject.SetActive(false);
            Time.timeScale = 1;
        }
        else
        {
            gameObject.transform.GetChild(pageNumber).gameObject.SetActive(true);
        }
    }

    public void NextPage()
    {
        pageNumber++;
        if (pageNumber >= gameObject.transform.childCount)
        {
            gameMaster.tutorial[tutorialNumber] = true;
            Time.timeScale = 1;
            gameObject.SetActive(false);
        }
        else
        {
            gameObject.transform.GetChild(pageNumber).gameObject.SetActive(true);
            gameObject.transform.GetChild(pageNumber-1).gameObject.SetActive(false);
        }
    }
}
