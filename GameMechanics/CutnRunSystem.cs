using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Class that take care of the Cut N' Run play mode
public class CutnRunSystem : MonoBehaviour
{
    public int morale = 100;
    public float
        maxTime = 2,
        time;
    private GameOverSystem gameOver;
    public CountDownSystem countDown;
    public Slider timeBar;
    public Slider moraleBar;
    private SlashSystem player;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<SlashSystem>();
        gameOver = GameObject.FindGameObjectWithTag("GO").GetComponent<GameOverSystem>();
    }

    private void Start()
    {
        time = maxTime;
        if (gameOver.isMoraleBased)
        {
            timeBar.gameObject.SetActive(true);
            moraleBar.gameObject.SetActive(true);
        }
    }

    private void FixedUpdate()
    {
        if (gameOver.isMoraleBased)
        {
            time -= Time.deltaTime;
            if (countDown.timeLeft <= countDown.maxTime -1)
            {
                maxTime -= .05f;
                countDown.maxTime--;
            }
        }
    }

    private void Update()
    {
        timeBar.value = time / maxTime;
        if (gameOver.isMoraleBased)
        {
            moraleBar.enabled = true;
            timeBar.enabled = true;

            if (time <= 0)
            {
                moraleBar.value = morale -= 25;
                if (morale <= 0 || countDown.timeLeft <= 0) gameOver.gameOver = true;
                time = maxTime;
                Debug.Log("" + morale.ToString());
            }

            if (player.hasHit)
            {
                time = maxTime;
                player.hasHit = false;
            }
        }
    }
}
