using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GoldenWeed : MonoBehaviour
{
    private WeedsSpawnSystem weeds;
    private PlayerController player;
    public Animator animator;
    private GameOverSystem gameOver;
    public GameObject scoreFeedback;
    public TextMeshPro scoreText;
    public int health = 1;
    private int points = 1000;

    public bool isDead;
    private void Awake()
    {
        gameOver = GameObject.FindGameObjectWithTag("GO").GetComponent<GameOverSystem>();
        weeds = GameObject.FindGameObjectWithTag("Spawner").GetComponent<WeedsSpawnSystem>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    private void Update()
    {
        if (health <= 0)
        {
            weeds.goldCounter -= 1;
            animator.SetTrigger("IsDead");
            if (!gameOver.isMoraleBased) UpdateScore();
            isDead = true;
            this.enabled = false;
        }
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("FreePos"))
        {
            weeds.goldCounter -= 1;
            weeds.SpawnBush();
            Destroy(gameObject);
        }
        else return;
    }

    public void GotHit()
    {
        health -= 1;
    }

    private void UpdateScore()
    {
        scoreFeedback.SetActive(true);
        if (gameOver.isScoreBased)
        {
            player.playerScore += points;
            scoreText.text = "+ " + points.ToString();
        }
        else if (gameOver.isTimeBased)
        {
            player.plantsKilled+=2;
            scoreText.text = "+ 2";
        }
        else
        {
            Debug.LogError("The level type has not been assigned");
            return;
        }
    }
}
