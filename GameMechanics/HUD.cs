using TMPro;
using UnityEngine;

public class HUD : MonoBehaviour
{
    public GameOverSystem gameOver;
    public GameObject gameObject;
    public PlayerController player;
    public CountDownSystem countDown;
    public TextMeshProUGUI score;
    public TextMeshProUGUI time;


    private void Update()
    {
        if (gameOver.isScoreBased)
        {
            score.text = player.playerScore.ToString();
        }
        else if (gameOver.isTimeBased)
        {
            score.text = player.plantsKilled.ToString() + "/" + gameOver.requiredPlants.ToString();
        }

        time.text = countDown.timeLeft.ToString("F2");

        if(gameOver.gameOver == true)
        {
            gameObject.SetActive(false);
        }
    }
}
