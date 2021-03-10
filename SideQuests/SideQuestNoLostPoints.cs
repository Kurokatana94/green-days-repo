using UnityEngine;

public class SideQuestNoLostPoints : MonoBehaviour
{
    private GameOverSystem gameOver;
    private PlayerController playerController;
    private int currentPoints = 0;
    [HideInInspector]
    public bool failed = false;

    private void Awake()
    {
        gameOver = gameObject.GetComponent<GameOverSystem>();
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    private void Update()
    {
        if (!failed)
        {
            gameOver.sideQuestComplete = true;
            if(playerController.playerScore < currentPoints)
            {
                gameOver.sideQuestComplete = false;
                failed = true;
            }
            currentPoints = playerController.playerScore;
        }
    }
}
