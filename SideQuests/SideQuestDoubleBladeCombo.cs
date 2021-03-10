﻿using System.Collections.Generic;
using UnityEngine;

public class SideQuestDoubleBladeCombo : MonoBehaviour
{
    [Tooltip("Insert the tags of the plants that have to be cut")]
    public string[] tags;
    [Space]
    [Tooltip("Insert how many times there has to be a combo")]
    public int comboQuantity = 1;
    private int comboCounter;

    private GameOverSystem gameOver;
    private List<GameObject> alreadyHitPlants = new List<GameObject>();
    private List<GameObject> comboHitPlants = new List<GameObject>();
    [HideInInspector]
    public bool completed = false;

    private void Awake()
    {
        gameOver = gameObject.GetComponent<GameOverSystem>();
    }

    private void Update()
    {
        if (!completed)
        {
            RemoveDeadPlants();

            foreach (GameObject item in GameObject.FindGameObjectsWithTag("Blade"))
            {
                if (item.GetComponent<BladeWeed>().isDead)
                {
                    for (int i = 0; i < item.transform.childCount; i++)
                    {
                        LeafBladeSystem blade = item.transform.Find("Blades").GetChild(i).GetComponent<LeafBladeSystem>();
                        Collider2D[] hitPlants = Physics2D.OverlapCircleAll(blade.transform.position, blade.leafRange, blade.plantLayers);

                        foreach (Collider2D plant in hitPlants)
                        {
                            bool goAhead = true;
                            foreach (GameObject p in alreadyHitPlants)
                            {
                                if (plant.gameObject == p)
                                {
                                    goAhead = false;
                                }
                            }

                            if (plant.CompareTag("Blade") && goAhead && !plant.GetComponent<BladeWeed>().isDead)
                            {
                                alreadyHitPlants.Add(plant.gameObject);
                            }
                        }
                    }
                }                
            }

            foreach (GameObject item in alreadyHitPlants)
            {
                foreach (string tag in tags)
                {
                    if (item.GetComponent<BladeWeed>().isDead)
                    {
                        Debug.LogWarning("Found active blades");
                        for (int i = 0; i < item.transform.childCount; i++)
                        {
                            LeafBladeSystem blade = item.transform.Find("Blades").GetChild(i).GetComponent<LeafBladeSystem>();
                            Collider2D[] hitPlants = Physics2D.OverlapCircleAll(blade.transform.position, blade.leafRange, blade.plantLayers);

                            foreach (Collider2D plant in hitPlants)
                            {
                                bool goAhead = true;
                                foreach (GameObject p in comboHitPlants)
                                {
                                    if (plant.gameObject == p)
                                    {
                                        goAhead = false;
                                        Debug.LogWarning("Can't go ahead");
                                    }
                                }

                                if (plant.CompareTag(tag) && goAhead)
                                {
                                    Debug.LogWarning("Looking for blade");
                                    switch (tag)
                                    {
                                        case "Tulipa":
                                            if (!plant.GetComponent<RedTulipa>().isDead)
                                            {
                                                comboCounter++;
                                                comboHitPlants.Add(plant.gameObject);
                                                Debug.LogWarning("Made Combo");
                                            }
                                            break;

                                        case "Green":
                                            if (!plant.GetComponent<GreenWeed>().isDead)
                                            {
                                                comboCounter++;
                                                comboHitPlants.Add(plant.gameObject);
                                                Debug.LogWarning("Made Combo");
                                            }
                                            break;

                                        case "Gold":
                                            if (!plant.GetComponent<GoldenWeed>().isDead)
                                            {
                                                comboCounter++;
                                                comboHitPlants.Add(plant.gameObject);
                                                Debug.LogWarning("Made Combo");
                                            }
                                            break;

                                        case "Evil":
                                            if (!plant.GetComponent<EvilWeed>().isDead)
                                            {
                                                comboCounter++;
                                                comboHitPlants.Add(plant.gameObject);
                                                Debug.LogWarning("Made Combo");
                                            }
                                            break;

                                        case "Blade":
                                            if (!plant.GetComponent<BladeWeed>().isDead)
                                            {
                                                comboCounter++;
                                                comboHitPlants.Add(plant.gameObject);
                                                Debug.LogWarning("Made Combo");
                                            }
                                            break;

                                        case "Bush":
                                            if (!plant.GetComponent<Bush>().isDead)
                                            {
                                                comboCounter++;
                                                comboHitPlants.Add(plant.gameObject);
                                                Debug.LogWarning("Made Combo");
                                            }
                                            break;

                                        default:
                                            break;
                                    }

                                    if (comboCounter >= comboQuantity)
                                    {
                                        gameOver.sideQuestComplete = true;
                                        completed = true;
                                        break;
                                    }
                                }
                            }

                            if (completed) break;
                        }

                        if (completed) break;
                    }
                }
            }
        }
    }

    private void RemoveDeadPlants()
    {
        for (int i = 0; i < alreadyHitPlants.Count; i++)
        {
            if (alreadyHitPlants[i] == null)
            {
                alreadyHitPlants.RemoveAt(i);
            }
        }

        for (int i = 0; i < comboHitPlants.Count; i++)
        {
            if(comboHitPlants[i] == null)
            {
                comboHitPlants.RemoveAt(i);
            }
        }
    }
}
