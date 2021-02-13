using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountDownSystem : MonoBehaviour
{
    public double maxTime;
    public double timeLeft;
    private GameOverSystem gameOver;
    public AudioSource weeds;

    private void Awake()
    {
        gameOver = GameObject.FindGameObjectWithTag("GO").GetComponent<GameOverSystem>();
        weeds.mute = true;
        timeLeft = maxTime;
    }

    private void FixedUpdate()
    {
        if (timeLeft > 0f)
        {
            timeLeft -= Time.fixedDeltaTime;
        }
        else if(timeLeft <= 0f)
        {
            gameOver.gameOver = true;
            Debug.Log("Fine Partita");
        }
    }

    private void Update()
    {
        if (timeLeft <= maxTime - 2)
        {
            weeds.mute = false;
        }
    }
}
