using System.Collections.Generic;
using UnityEngine;

public class SideQuestHitWithThrowSkill : MonoBehaviour
{
    [Tooltip("Insert the skill GameObject")]
    public RangedAttackSystem skill;
    [Tooltip("Insert the tags of the plants that have to be cut")]
    public string[] tags;
    [Space]
    [Tooltip("Insert the quantity of the required plants that have to be cut")]
    public int hitQuantity;
    private int hitCounter;
    [HideInInspector]
    public bool completed = false;

    private GameOverSystem gameOver;
    private List<GameObject> alreadyHitPlants = new List<GameObject>();

    private void Awake()
    {
        gameOver = gameObject.GetComponent<GameOverSystem>();
    }

    private void Update()
    {
        if (!completed && skill.isActive)
        {
            foreach (string tag in tags)
            {
                Collider2D[] hitPlant = Physics2D.OverlapCircleAll(skill.skillPoint.position, skill.skillRange, skill.plantLayers);

                foreach (Collider2D plant in hitPlant)
                {
                    bool goAhead = true;
                    foreach (GameObject item in alreadyHitPlants)
                    {
                        if(plant.gameObject == item)
                        {
                            goAhead = false;
                        }
                    }

                    if (plant.CompareTag(tag) && goAhead)
                    {
                        switch (tag)
                        {
                            case "Tulipa":
                                if (!plant.GetComponent<RedTulipa>().isDead)
                                {
                                    hitCounter++;
                                    alreadyHitPlants.Add(plant.gameObject);
                                }
                                break;

                            case "Green":
                                if (!plant.GetComponent<GreenWeed>().isDead)
                                {
                                    hitCounter++;
                                    alreadyHitPlants.Add(plant.gameObject);
                                }
                                break;

                            case "Gold":
                                if (!plant.GetComponent<GoldenWeed>().isDead)
                                {
                                    hitCounter++;
                                    alreadyHitPlants.Add(plant.gameObject);
                                }
                                break;

                            case "Evil":
                                if (!plant.GetComponent<EvilWeed>().isDead)
                                {
                                    hitCounter++;
                                    alreadyHitPlants.Add(plant.gameObject);
                                }
                                break;

                            case "Blade":
                                if (!plant.GetComponent<BladeWeed>().isDead)
                                {
                                    hitCounter++;
                                    alreadyHitPlants.Add(plant.gameObject);
                                }
                                break;

                            case "Bush":
                                if (!plant.GetComponent<Bush>().isDead)
                                {
                                    hitCounter++;
                                    alreadyHitPlants.Add(plant.gameObject);
                                }
                                break;

                            default:
                                break;
                        }

                        if(hitCounter >= hitQuantity)
                        {
                            gameOver.sideQuestComplete = true;
                            completed = true;
                            break;
                        }
                    }
                }
                if (completed) break;
            }            
        }

        RemoveDeadPlants();
    }

    private void RemoveDeadPlants()
    {
        for (int i = 0; i < alreadyHitPlants.Count; i++)
        {
            if (alreadyHitPlants[i] == null)
            {
                alreadyHitPlants.Remove(alreadyHitPlants[i]);
            }
        }
    }
}
