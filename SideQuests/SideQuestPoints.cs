using UnityEngine;

//Sidequest that require a given amount of points to be completed
public class SideQuestPoints : MonoBehaviour
{
    [Tooltip("Points required to complete the sidequest")]
    public int requiredPoints;
    [HideInInspector]
    public bool completed = false;
    private PlayerController playerController;
    private GameOverSystem gameOver;

    private void Awake()
    {
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        gameOver = gameObject.GetComponent<GameOverSystem>();
    }

    private void Update()
    {
        if(!completed && gameOver.gameOver && playerController.playerScore >= requiredPoints)
        {
            gameOver.sideQuestComplete = true;
            completed = true;
        }
    }
}
