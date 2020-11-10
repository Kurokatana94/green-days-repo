using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountDownSystem : MonoBehaviour
{
    public double maxTime;
    public double timeLeft;
    public GameOverSystem gameOverSystem;
    public AudioSource weeds;

    private void Awake()
    {
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
            gameOverSystem.gameOver = true;
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
