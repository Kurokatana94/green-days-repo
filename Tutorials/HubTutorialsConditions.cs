using UnityEngine;

public class HubTutorialsConditions : MonoBehaviour
{
    private GameMaster gameMaster;
    public GameObject skinShop;
    public GameObject skillSelection;
    public GameObject skinSelection;

    private void Awake()
    {
        gameMaster = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
    }

    private void Start()
    {
        if (!gameMaster.tutorial[0])
        {
            transform.GetChild(0).gameObject.SetActive(true);
        }

        if (gameMaster.tutorial[10] && !gameMaster.tutorial[1])
        {
            transform.GetChild(1).gameObject.SetActive(true);
        }
    }

    private void Update()
    {
        if(skinShop.activeInHierarchy && !gameMaster.tutorial[2])
        {
            transform.GetChild(2).gameObject.SetActive(true);
        }

        if(skinSelection.activeInHierarchy && !gameMaster.tutorial[3])
        {
            transform.GetChild(3).gameObject.SetActive(true);
        }

        if(skillSelection.activeInHierarchy && !gameMaster.tutorial[4])
        {
            transform.GetChild(4).gameObject.SetActive(true);
        }
    }
}
