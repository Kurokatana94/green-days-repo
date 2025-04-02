using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideQuestsManagement : MonoBehaviour
{
    public MonoBehaviour[] sideQuests;
    private GameOverSystem gameOver;
    private int successCounter;
    private bool alreadyChecked = false;

    private void Awake()
    {
        gameOver = gameObject.GetComponent<GameOverSystem>();
    }

    private void Update()
    {
        if (gameOver.gameOver && !alreadyChecked)
        {
            foreach(MonoBehaviour sideQuest in sideQuests)
            {
                Debug.Log(sideQuest.GetType().ToString());
                switch (sideQuest.GetType().ToString())
                {
                    case "SideQuestNoCut":
                        SideQuestNoCut noCut = (SideQuestNoCut)sideQuest;
                        if (!noCut.failed)
                        {
                            successCounter++;
                        }
                        break;

                    case "SideQuestNoCutWithBlade":
                        SideQuestNoCutWithBlade noCutBlade = (SideQuestNoCutWithBlade)sideQuest;
                        if (!noCutBlade.failed)
                        {
                            successCounter++;
                        }
                        break;

                    case "SideQuestBladeCombo":
                        SideQuestBladeCombo bladeCombo = (SideQuestBladeCombo)sideQuest;
                        if (bladeCombo.completed)
                        {
                            successCounter++;
                        }
                        break;

                    case "SideQuestHitWithThrowSkill":
                        SideQuestHitWithThrowSkill throwSkill = (SideQuestHitWithThrowSkill)sideQuest;
                        if (throwSkill.completed)
                        {
                            successCounter++;
                        }
                        break;

                    case "SideQuestNoLostPoints":
                        SideQuestNoLostPoints noLostPoints = (SideQuestNoLostPoints)sideQuest;
                        if (noLostPoints.failed)
                        {
                            successCounter++;
                        }
                        break;

                    case "SideQuestPoints":
                        SideQuestPoints points = (SideQuestPoints)sideQuest;
                        if (points.completed)
                        {
                            successCounter++;
                        }
                        break;

                    default:
                        break;
                }
            }

            if(successCounter >= sideQuests.Length)
            {
                gameOver.sideQuestComplete = true;
            }
            else
            {
                gameOver.sideQuestComplete = false;
            }

            alreadyChecked = true;
        }
    }
}
