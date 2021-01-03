using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Bush : MonoBehaviour
{
    private WeedsSpawnSystem bushes;
    private PlayerController player;
    public Animator animator;
    public CircleCollider2D collider;

    private int
        currentHealth,
        bushPoints,
        hitPoints = 50,
        deathPoints = 200;
    public int 
        bushHealth = 3, 
        hitRate = 3;
    private float nextHitTime;
    public bool isDead;
    public GameObject scoreGainedBush;
    public GameObject scoreFeedback;
    public TextMeshPro scoreText, scoreGainedText;
    private GameOverSystem gameOver;

    private void Awake()
    {
        gameOver = GameObject.FindGameObjectWithTag("GO").GetComponent<GameOverSystem>();
        bushes = GameObject.FindGameObjectWithTag("Spawner").GetComponent<WeedsSpawnSystem>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    private void Start()
    {
        currentHealth = bushHealth;
    }

    private void Update()
    {

        if (bushHealth < currentHealth && bushHealth > 0 && gameOver.isScoreBased)
        {
            bushPoints = hitPoints;
            AddPoints();
        }
        else if (bushHealth <= 0)
        {
            collider.enabled = false;
            bushPoints = deathPoints;
            bushes.bushCounter -= 1;
            animator.SetTrigger("IsDead");
            if(!gameOver.isMoraleBased) UpdateScore();
            isDead = true;
            this.enabled = false;
        }
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("FreePos"))
        {
            bushes.bushCounter -= 1;
            bushes.SpawnBush();
            Destroy(gameObject);
        }
        else return;
    }

    public void GotHit()
    {
        if (Time.time >= nextHitTime)
        {
            bushHealth -= 1;
            animator.SetInteger("CurrentHealth", bushHealth);
            nextHitTime = Time.time + 1f / hitRate;
        }
    }

    private void UpdateScore()
    {
        scoreFeedback.SetActive(true);
        if (gameOver.isScoreBased)
        {
            player.playerScore += bushPoints;
            scoreText.text = "+ " + bushPoints.ToString();
        }else if (gameOver.isTimeBased)
        {
            player.plantsKilled++;
            scoreText.text = "+ 1";
        }
        else
        {
            Debug.LogError("The level type has not been assigned");
            return;
        }
        currentHealth = bushHealth;
    }

    private void AddPoints()
    {
        scoreGainedText.text = "";

        player.playerScore += bushPoints;
        scoreGainedText.text = "+ " + bushPoints.ToString();

        currentHealth = bushHealth;
        Instantiate(scoreGainedBush, transform.position, Quaternion.identity, transform);
    }   
}
