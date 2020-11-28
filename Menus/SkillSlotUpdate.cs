using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillSlotUpdate : MonoBehaviour
{
    private GameMaster gameMaster;

    private void Awake()
    {
        gameMaster = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
    }

    private void Update()
    {
        for (int i = 0; i < gameMaster.skillActive.Length; i++)
        {
            if (gameMaster.skillActive[i])
            {
                switch (i)
                {
                    case 0:
                        gameObject.transform.GetChild(i).gameObject.SetActive(true);
                        for (int j = 0; j < gameMaster.skillActive.Length; j++)
                        {
                            if(j != i)
                            {
                                gameObject.transform.GetChild(j).gameObject.SetActive(false);
                            }
                        }
                        break;
                    case 1:
                        gameObject.transform.GetChild(i).gameObject.SetActive(true);
                        for (int j = 0; j < gameMaster.skillActive.Length; j++)
                        {
                            if (j != i)
                            {
                                gameObject.transform.GetChild(j).gameObject.SetActive(false);
                            }
                        }
                        break;
                    case 2:
                        gameObject.transform.GetChild(i).gameObject.SetActive(true);
                        for (int j = 0; j < gameMaster.skillActive.Length; j++)
                        {
                            if (j != i)
                            {
                                gameObject.transform.GetChild(j).gameObject.SetActive(false);
                            }
                        }
                        break;
                    default:
                        break;
                }
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
    }
}
