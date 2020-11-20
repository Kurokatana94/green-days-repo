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
    private PowerUpSpawnSystem powerUp;
    public CircleCollider2D collider;
    private RangedAttackSystem ranged;
    private SwirlAttackSystem swirl;

    private int
        currentHealth,
        bushPoints,
        hitPoints = 50,
        deathPoints = 200;
    public bool isDead, alreadyHit;
    public GameObject scoreGainedBush;
    public GameObject scoreFeedback;
    public TextMeshPro scoreText, scoreGainedText;
    public int bushHealth = 3;
    private GameOverSystem gameOver;

    private void Awake()
    {
        gameOver = GameObject.FindGameObjectWithTag("GO").GetComponent<GameOverSystem>();
        bushes = GameObject.FindGameObjectWithTag("Spawner").GetComponent<WeedsSpawnSystem>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        powerUp = GameObject.FindGameObjectWithTag("Player").GetComponent<PowerUpSpawnSystem>();
        ranged = GameObject.FindGameObjectWithTag("Skill").GetComponent<RangedAttackSystem>();
        swirl = GameObject.FindGameObjectWithTag("Skill").GetComponent<SwirlAttackSystem>();
    }

    private void Start()
    {
        currentHealth = bushHealth;
    }

    private void Update()
    {
        if (!ranged.isActive && ranged.isReady || !swirl.isActive && swirl.isReady) alreadyHit = false;

        if (bushHealth < currentHealth && bushHealth != 0 && gameOver.isScoreBased)
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
        bushHealth -= 1;
        animator.SetInteger("CurrentHealth", bushHealth);
        alreadyHit = true;
    }

    private void UpdateScore()
    {
        scoreFeedback.SetActive(true);
        if (gameOver.isScoreBased)
        {
            if (powerUp.haveX2 == true)
            {
                player.playerScore += bushPoints * 2;
                scoreText.text = "+ " + (bushPoints * 2).ToString();
            }
            else
            {
                player.playerScore += bushPoints;
                scoreText.text = "+ " + bushPoints.ToString();
            }
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

        if (powerUp.haveX2 == true)
        {
            player.playerScore += bushPoints * 2;
            scoreGainedText.text = "+ " + (bushPoints * 2).ToString();
        }
        else
        {
            player.playerScore += bushPoints;
            scoreGainedText.text = "+ " + bushPoints.ToString();
        }
        currentHealth = bushHealth;
        Instantiate(scoreGainedBush, transform.position, Quaternion.identity, transform);
    }   
}
