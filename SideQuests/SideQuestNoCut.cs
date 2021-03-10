using UnityEngine;

public class SideQuestNoCut : MonoBehaviour
{
    [Tooltip("Instert the tags of the plants that cannot be cut")]
    public string[] tags;
    [HideInInspector]
    public bool failed = false;
    private GameOverSystem gameOver;

    private void Awake()
    {
        gameOver = gameObject.GetComponent<GameOverSystem>();
    }

    private void Update()
    {
        if (!failed)
        {
            gameOver.sideQuestComplete = true;

            foreach (string tag in tags)
            {
                foreach (GameObject item in GameObject.FindGameObjectsWithTag(tag))
                {
                    switch (tag)
                    {
                        case "Tulipa":
                            if (item.GetComponent<RedTulipa>().isDead)
                            {
                                gameOver.sideQuestComplete = false;
                                failed = true;
                            }
                            break;

                        case "Green":
                            if (item.GetComponent<GreenWeed>().isDead)
                            {
                                gameOver.sideQuestComplete = false;
                                failed = true;
                            }
                            break;

                        case "Gold":
                            if (item.GetComponent<GoldenWeed>().isDead)
                            {
                                gameOver.sideQuestComplete = false;
                                failed = true;
                            }
                            break;

                        case "Evil":
                            if (item.GetComponent<EvilWeed>().isDead)
                            {
                                gameOver.sideQuestComplete = false;
                                failed = true;
                            }
                            break;

                        case "Blade":
                            if (item.GetComponent<BladeWeed>().isDead)
                            {
                                gameOver.sideQuestComplete = false;
                                failed = true;
                            }
                            break;

                        case "Bush":
                            if (item.GetComponent<Bush>().isDead)
                            {
                                gameOver.sideQuestComplete = false;
                                failed = true;
                            }
                            break;

                        default:
                            break;
                    }

                    if (failed) break;
                }

                if (failed) break;
            }
        }
    }
}
