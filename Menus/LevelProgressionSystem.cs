using UnityEngine;
using UnityEngine.UI;

public class LevelProgressionSystem : MonoBehaviour
{
    public GameObject levelButtonFolder, tutorialButtonFolder;

    private GameMaster gameMaster;

    private void Awake()
    {
        gameMaster = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();

    }

    private void Start()
    {

        for (int i = 1; i < levelButtonFolder.transform.childCount-1; i++)
        {
            Button start = levelButtonFolder.transform.GetChild(i).Find("StartButton").GetComponent<Button>();
            FindBestStar star = levelButtonFolder.transform.GetChild(i).Find("Stars").GetComponent<FindBestStar>();

            if(gameMaster.bestStars[star.index-1] == 0)
            {
                start.interactable = false;
            }
        }

        int sideCompleted = 0;

        for (int i = 1; i <= levelButtonFolder.transform.childCount; i++)
        {
            FindBestStar star = levelButtonFolder.transform.GetChild(i-1).Find("Stars").GetComponent<FindBestStar>();

            if (gameMaster.sideQuestsCompleted[star.index])
            {
                sideCompleted++;
            }

            Debug.Log(sideCompleted);
            if(i == levelButtonFolder.transform.childCount && sideCompleted != levelButtonFolder.transform.childCount-1)
            {
                Button start = levelButtonFolder.transform.GetChild(i-1).Find("StartButton").GetComponent<Button>();
                start.interactable = false;
            }

        }

        for (int i = 1; i < tutorialButtonFolder.transform.childCount; i++)
        {
            Button start = tutorialButtonFolder.transform.GetChild(i).Find("StartButton").GetComponent<Button>();
            FindBestStar star = tutorialButtonFolder.transform.GetChild(i).Find("Stars").GetComponent<FindBestStar>();

            if (gameMaster.bestStars[star.index - 1] == 0)
            {
                start.interactable = false;
            }    
        }
    }
}
