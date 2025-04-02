using UnityEngine;

public class SideQuestCheck : MonoBehaviour
{
    private GameMaster gameMaster;

    private void Awake()
    {
        gameMaster = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
    }

    private void Start()
    {
        if (gameMaster.sideQuestsCompleted[transform.parent.Find("Stars").GetComponent<FindBestStar>().index])
        {
            transform.GetChild(1).gameObject.SetActive(true);
        }
    }
}
