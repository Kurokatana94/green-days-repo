using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class FindBestTime : MonoBehaviour
{
    private GameMaster gameMaster;
    private TextMeshProUGUI text;
    private int index;
    public string levelName;
    public bool byName, byIndex;

    private void Awake()
    {
        gameMaster = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
    }

    private void Start()
    {
        if (byName)
        {
            index = SceneManager.GetSceneByName(levelName).buildIndex;
            text.text = "" + gameMaster.bestTimes[index] + "s";
        }
        else if(byIndex)
        {
            text.text = "" + gameMaster.bestScores[index];
        }
    }
}
