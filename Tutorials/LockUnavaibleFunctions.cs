using UnityEngine;
using UnityEngine.UI;

public class LockUnavaibleFunctions : MonoBehaviour
{
    private GameMaster gameMaster;
    [Tooltip("Insert here the buttons that need to be deactivated")]
    public Button[] buttons;
    public GameObject[] levelLocks;

    //Small function that deactivate or cover buttons that aren't supposed to be pressed before the skill shop tutorial completition
    private void Awake()
    {
        gameMaster = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
    }

    private void Start()
    {
        if (!gameMaster.tutorial[10])
        {
            for (int i = 0; i < buttons.Length; i++)
            {
                buttons[i].interactable = false;
            }

            for (int i = 0; i < levelLocks.Length; i++)
            {
                levelLocks[i].SetActive(true);
            }
        }
    }

    //This function will be called from the skip tutorial system allowing to activate the buttons which would otherwise be inactive or covered
    public void Unlock()
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].interactable = true;
        }

        for (int i = 0; i < levelLocks.Length; i++)
        {
            levelLocks[i].SetActive(false);
        }
    }
}
