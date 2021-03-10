using System.Collections.Generic;
using UnityEngine;

public class SideQuestNoCutWithBlade : MonoBehaviour
{
    [Tooltip("Insert the tags of the plants that have to be cut")]
    public string[] tags;

    private GameOverSystem gameOver;
    [HideInInspector]
    public bool failed = false;

    private void Awake()
    {
        gameOver = gameObject.GetComponent<GameOverSystem>();
    }

    private void Update()
    {
        if (!failed)
        {
            gameOver.sideQuestComplete = true;
            foreach (GameObject item in GameObject.FindGameObjectsWithTag("Blade"))
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
                                if (plant.CompareTag(tag))
                                {
                                    Debug.LogWarning("Looking for blade");
                                    switch (tag)
                                    {
                                        case "Tulipa":
                                            if (!plant.GetComponent<RedTulipa>().isDead)
                                            {
                                                failed = true;
                                                gameOver.sideQuestComplete = false;
                                                Debug.LogWarning("Failed Quest");
                                            }
                                            break;

                                        case "Green":
                                            if (!plant.GetComponent<GreenWeed>().isDead)
                                            {
                                                failed = true;
                                                gameOver.sideQuestComplete = false;
                                                Debug.LogWarning("Failed Quest");
                                            }
                                            break;

                                        case "Gold":
                                            if (!plant.GetComponent<GoldenWeed>().isDead)
                                            {
                                                failed = true;
                                                gameOver.sideQuestComplete = false;
                                                Debug.LogWarning("Failed Quest");
                                            }
                                            break;

                                        case "Evil":
                                            if (!plant.GetComponent<EvilWeed>().isDead)
                                            {
                                                failed = true;
                                                gameOver.sideQuestComplete = false;
                                                Debug.LogWarning("Failed Quest");
                                            }
                                            break;

                                        case "Blade":
                                            if (!plant.GetComponent<BladeWeed>().isDead)
                                            {
                                                failed = true;
                                                gameOver.sideQuestComplete = false;
                                                Debug.LogWarning("Failed Quest");
                                            }
                                            break;

                                        case "Bush":
                                            if (!plant.GetComponent<Bush>().isDead)
                                            {
                                                failed = true;
                                                gameOver.sideQuestComplete = false;
                                                Debug.LogWarning("Failed Quest");
                                            }
                                            break;

                                        default:
                                            break;
                                    }
                                }
                            }
                            if (failed) break;
                        }
                        if (failed) break;
                    }
                }
            }
        }
    }
}

