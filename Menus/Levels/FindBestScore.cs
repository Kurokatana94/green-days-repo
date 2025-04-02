using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class FindBestScore : MonoBehaviour
{
    private GameMaster gameMaster;
    private TextMeshProUGUI text;
    public int index;
    public string levelName;
    public bool byIndex, byName;

    private void Awake()
    {
        gameMaster = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
        text = gameObject.GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {
        if (byName)
        {
            index = SceneManager.GetSceneByName(levelName).buildIndex;
            text.text = "" + gameMaster.bestScores[index];
        }
        else if (byIndex)
        {
            text.text = "" + gameMaster.bestScores[index];
        }
    }
}
