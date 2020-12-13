using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class Tulipa : MonoBehaviour
{
    private WeedsSpawnSystem weeds;
    private PlayerController player;
    private PowerUpSpawnSystem powerUp;
    public Animator animator;
    private Transform transform;
    public int tulipaPoints;
    private int tulipaGainedPoints;
    public bool isDead;
    private int tulipaHealth = 1;
    private double addPointsCDTimer;
    public float addPointsCD;

    public float byeTulipaCD;
    private double byeTulipaCDTimer;
    private bool byeTulipa = false;
    public bool isBye, cycleEnded;
    public GameObject scoreGained;
    public TextMeshPro scoreGainedText;
    public GameObject scoreLost;
    public TextMeshPro scoreLostText;

    private GameOverSystem gameOver;
    private CountDownSystem countDown;
    private CutnRunSystem cutnRun;
    private int timeLost;

    private void Awake()
    {
        transform = GetComponent<Transform>();
        countDown = GameObject.FindGameObjectWithTag("CD").GetComponent<CountDownSystem>();
        cutnRun = GameObject.FindGameObjectWithTag("CD").GetComponent<CutnRunSystem>();
        gameOver = GameObject.FindGameObjectWithTag("GO").GetComponent<GameOverSystem>();
        weeds = GameObject.FindGameObjectWithTag("Spawner").GetComponent<WeedsSpawnSystem>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        powerUp = GameObject.FindGameObjectWithTag("Player").GetComponent<PowerUpSpawnSystem>();
    }

    private void Start()
    {
        addPointsCDTimer = addPointsCD;
        byeTulipaCDTimer = byeTulipaCD;
        timeLost = (int)(countDown.maxTime / 10);
    }

    private void FixedUpdate()
    {
        if (gameOver.isScoreBased)
        {
            if(addPointsCDTimer > 0 && cycleEnded)
            {
                addPointsCDTimer -= Time.fixedDeltaTime;
            }
            else if(addPointsCDTimer <= 0)
            {
                AddPoints();
                addPointsCDTimer = addPointsCD;
                cycleEnded = false;
            }
        }

        if (!byeTulipa && byeTulipaCDTimer > 0)
        {
            byeTulipaCDTimer -= Time.fixedDeltaTime;
        }
        else
        {
            ByeTulipa();
        }
    }

    private void Update()
    {
        if (tulipaHealth <= 0)
        {
            if (gameOver.isTimeBased)
            {
                countDown.timeLeft -= timeLost;
                scoreLostText.text = "- " + timeLost.ToString() + "s";
            }
            else if (gameOver.isScoreBased)
            {
                scoreLostText.text = "- " + tulipaGainedPoints.ToString();
                player.playerScore -= tulipaGainedPoints;
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
            weeds.tulipaCounter -= 1;
            animator.SetTrigger("IsDead");
            isDead = true;
            this.enabled = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("FreePos"))
        {
            weeds.tulipaCounter -= 1;
            weeds.SpawnTulipa();
            Destroy(gameObject);
        }
        else return;
    }

    //If plant get hit
    public void GotHit()
    {
        tulipaHealth -= 1;
    }

    //Add points to the player while showing it
    private void AddPoints()
    {
        Instantiate(scoreGained, transform.position, Quaternion.identity, transform);

        if (powerUp.haveX2 == true)
        {
            player.playerScore += tulipaPoints * 2;
            tulipaGainedPoints += tulipaPoints * 2;
            scoreGainedText.text = "+ " + (tulipaPoints * 2).ToString();
        }
        else
        {
            player.playerScore += tulipaPoints;
            tulipaGainedPoints += tulipaPoints;
            scoreGainedText.text = "+ " + tulipaPoints.ToString();
        }
    }

    //Make tulipa despawn after her life span finished
    private void ByeTulipa()
    {
        animator.SetTrigger("ByeTulipa");
        weeds.tulipaCounter -= 1;
        isBye = true;
        this.enabled = false;
    }
}
