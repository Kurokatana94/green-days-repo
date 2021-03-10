using UnityEngine;
using UnityEngine.SceneManagement;

public class FindBestStar: MonoBehaviour
{
    private GameMaster gameMaster;
    public GameObject[] stars;
    public int index;
    public string levelName;
    public bool byIndex, byName;

    private void Awake()
    {
        gameMaster = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
    }

    private void Start()
    {
        if (byName)
        {
            index = SceneManager.GetSceneByName(levelName).buildIndex;
            for (int i = 0; i < gameMaster.bestStars[index]; i++)
            {
                stars[i].SetActive(true);
            }
        }
        else if (byIndex)
        {
            for (int i = 0; i < gameMaster.bestStars[index]; i++)
            {
                stars[i].SetActive(true);
            }
        }
    }
}
