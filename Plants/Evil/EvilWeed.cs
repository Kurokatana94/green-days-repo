using TMPro;
using UnityEngine;
using System;

public class EvilWeed : MonoBehaviour
{
    private WeedsSpawnSystem weeds;
    private PlayerController player;
    public Animator animator;
    private GameOverSystem gameOver;
    public CircleCollider2D collider, triggerCollider;
    private CountDownSystem countDown;
    private CutnRunSystem cutnRun;

    //References to text objects to show points gained or lost
    public GameObject scoreFeedback;
    public TextMeshPro scoreText;
    public GameObject scoreLost;
    public TextMeshPro scoreLostText;
    public bool cycleEnded;

    //Bools used to check whether the plant is ready to grow or should apply its mechanic
    public bool isSmall, isLarge;

    //Floats used to keep track of growing and points removal cooldowns
    public float growCDTimer, growCD;
    public float removePointsCDTimer, removePointsCD;

    //Ints keeping basic data of the plant
    private int 
        evilPoints = 100,
        evilMinusPoins = 50,
        evilHealth = 1,
        timeLost = 5;

    //Bool used to check if plant is still alive or not
    public bool isDead;

    private void Awake()
    {
        weeds = GameObject.FindGameObjectWithTag("Spawner").GetComponent<WeedsSpawnSystem>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        gameOver = GameObject.FindGameObjectWithTag("GO").GetComponent<GameOverSystem>();
        cutnRun = GameObject.FindGameObjectWithTag("CD").GetComponent<CutnRunSystem>();
        countDown = GameObject.FindGameObjectWithTag("CD").GetComponent<CountDownSystem>();
    }

    private void Start()
    {
        isSmall = true;
        growCDTimer = growCD;
        removePointsCDTimer = removePointsCD;
    }

    private void FixedUpdate()
    {
        if (isSmall) growCDTimer -= Time.fixedDeltaTime;

        if (isLarge)
        {
            if (removePointsCDTimer > 0 && cycleEnded) removePointsCDTimer -= Time.fixedDeltaTime;
        }
    }


    private void Update()
    {
        if (isSmall & growCDTimer <= 0)
        {
            isSmall = false;
            isLarge = true;
            collider.enabled = true;
            animator.SetBool("IsLarge", true);
        }

        if (isLarge)
        {
            if(removePointsCDTimer <= 0)
            {
                RemovePoints();
                removePointsCDTimer = removePointsCD;
                cycleEnded = false;
            }
        }

        if (evilHealth <= 0)
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
        evilHealth -= 1;
    }

    private void UpdateScore()
    {
        scoreFeedback.SetActive(true);
        if (gameOver.isScoreBased)
        {
            player.playerScore += evilPoints;
            scoreText.text = "+ " + evilPoints.ToString();
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
        Instantiate(scoreLost, transform.position, Quaternion.identity, transform);

        if (gameOver.isTimeBased)
        {
            countDown.timeLeft -= timeLost;
            scoreLostText.text = "- " + timeLost.ToString() + "s";
        }
        else if (gameOver.isScoreBased)
        {
            scoreLostText.text = "- " + evilMinusPoins.ToString();
            if (player.playerScore >= evilMinusPoins) player.playerScore -= evilMinusPoins;
            else player.playerScore = 0;
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
    }
}

