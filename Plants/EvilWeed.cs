using TMPro;
using UnityEngine;
using GDTools;
using System;

public class EvilWeed : MonoBehaviour
{
    private WeedsSpawnSystem weeds;
    private PlayerController player;
    public Animator animator;
    private PowerUpSpawnSystem powerUp;
    private GameOverSystem gameOver;
    public CircleCollider2D collider, triggerCollider;
    private CountDownSystem countDown;
    private CutnRunSystem cutnRun;

    //References to text objects to show points gained or lost
    public GameObject scoreFeedback;
    public TextMeshPro scoreText;
    public GameObject scoreLost;
    public TextMeshPro scoreLostText;

    //Bools used to check whether the plant is ready to grow or should apply its mechanic
    public bool isSmall, isLarge;

    //Floats used to keep track of growing and points removal cooldowns
    public float growCDTimer, growCD;
    public float removePointsCDTimer, removePointsCD;

    //Ints keeping basic data of the plant
    private int 
        weedPoints = 100,
        weedMinusPoins = 50,
        weedHealth = 1,
        timeLost = 5;

    //Bool used to check if plant is still alive or not
    public bool isDead;

    private void Awake()
    {
        weeds = GameObject.FindGameObjectWithTag("Spawner").GetComponent<WeedsSpawnSystem>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        powerUp = GameObject.FindGameObjectWithTag("Player").GetComponent<PowerUpSpawnSystem>();
        gameOver = GameObject.FindGameObjectWithTag("GO").GetComponent<GameOverSystem>();
        cutnRun = GameObject.FindGameObjectWithTag("CD").GetComponent<CutnRunSystem>();
        countDown = GameObject.FindGameObjectWithTag("CD").GetComponent<CountDownSystem>();
    }

    private void Start()
    {
        isSmall = true;
        growCDTimer = growCD;
    }

    private void FixedUpdate()
    {
        if (isSmall && Timer.CoolDown(growCDTimer, growCD) == true)
        {
            isSmall = false;
            isLarge = true;
            animator.SetBool("IsMedium", true);
        }

        if (isLarge)
        {
            if(Timer.CoolDown(removePointsCDTimer,removePointsCDTimer))
            {
                RemovePoints();
            }
        }
    }


    private void Update()
    {
        if (isLarge) collider.enabled = true;

        if (weedHealth <= 0)
        {
            collider.enabled = false;
            if(!gameOver.isMoraleBased) UpdateScore();
            weeds.evilWeedCounter -= 1;
            animator.SetTrigger("IsDead");
            isDead = true;
            this.enabled = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("FreePos"))
        {
            weeds.evilWeedCounter -= 1;
            weeds.SpawnEvilWeed();
            Destroy(gameObject);
        }
        else return;
    }

    public void GotHit()
    {
        weedHealth -= 1;
    }

    private void UpdateScore()
    {
        scoreFeedback.SetActive(true);
        if (gameOver.isScoreBased)
        {
            if (powerUp.haveX2 == true)
            {
                player.playerScore += weedPoints * 2;
                scoreText.text = "+ " + (weedPoints * 2).ToString();
            }
            else
            {
                player.playerScore += weedPoints;
                scoreText.text = "+ " + weedPoints.ToString();
            }
        }
        else if (gameOver.isTimeBased)
        {
            player.plantsKilled++;
            scoreText.text = "+ 1";
        }
        else
        {
            Debug.LogError("The level type has not been assigned");
            return;
        }
    }
    public void RemovePoints()
    {
        if (gameOver.isTimeBased)
        {
            countDown.timeLeft -= timeLost;
            scoreLostText.text = "- " + timeLost.ToString() + "s";
        }
        else if (gameOver.isScoreBased)
        {
            scoreLostText.text = "- " + weedMinusPoins.ToString();
            player.playerScore -= weedMinusPoins;
        }
        else if (gameOver.isMoraleBased)
        {
            cutnRun.time = 0;
            scoreLostText.text = "-25%";
        }
        else
        {
            Debug.LogError("The level type has not been assigned");
        }
        scoreLost.SetActive(true);
        weeds.evilWeedCounter -= 1;
        animator.SetTrigger("IsDead");
        isDead = true;
        this.enabled = false;
    }
}

