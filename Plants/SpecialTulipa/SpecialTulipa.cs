using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class SpecialTulipa : MonoBehaviour
{
    [Header("References")]
    private WeedsSpawnSystem weeds;
    private PlayerController player;
    public Animator animator;
    private Transform transform;
    public TextMeshPro scoreGainedText;
    public GameObject scoreGained;
    private GameOverSystem gameOver;
    //private CountDownSystem countDown;
    //private CutnRunSystem cutnRun;

    [Space]
    [Tooltip("Points given overtime by the Tulipa")]
    public int tulipaPoints;

    [Space]
    //Cooldown variables needed in order to add points overtime
    [Tooltip("Time passing between on tick and the next one for the added points overtime")]
    public float addPointsCD;
    private double addPointsCDTimer;

    //Tulipa's health
    private int tulipaHealth = 1;
    
    //Bools needed to keep track of Tulipa current status and activating despawn animation whenever it should be triggered
    [HideInInspector]
    //If killed
    public bool isDead;
    //If life period ended
    private bool byeTulipa = false;
    [HideInInspector]
    public bool isBye;
    
    //It syncronize the bonus feedback for the player by starting the CD for the next one after the end of the animation
    [HideInInspector]
    public bool cycleEnded;

    [Tooltip("Total Tulipa's life length in seconds")]
    public float byeTulipaCD;
    private double byeTulipaCDTimer;

    //Used to check if the bonus addition is applicable or not
    [Space]
    [Tooltip("Range around the Tulipa where if multiple plants are spawned at the same time, they will trigger the shy status disabling the bonus given over time")]
    public float shyRange;
    [Tooltip("Minimum quantity of plants required to trigger the shy status")]
    public int shyLimit;
    [Tooltip("Layers to check in order to count how many plants are in the area")]
    public LayerMask plantLayer;
    private bool isShy = false;

    private void Awake()
    {
        transform = GetComponent<Transform>();
        //countDown = GameObject.FindGameObjectWithTag("CD").GetComponent<CountDownSystem>();
        //cutnRun = GameObject.FindGameObjectWithTag("CD").GetComponent<CutnRunSystem>();
        gameOver = GameObject.FindGameObjectWithTag("GO").GetComponent<GameOverSystem>();
        weeds = GameObject.FindGameObjectWithTag("Spawner").GetComponent<WeedsSpawnSystem>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    private void Start()
    {
        addPointsCDTimer = addPointsCD;
        byeTulipaCDTimer = byeTulipaCD;
    }

    private void FixedUpdate()
    {
        if (!isShy)
        {
            if (gameOver.isScoreBased)
            {
                if (addPointsCDTimer > 0 && cycleEnded)
                {
                    addPointsCDTimer -= Time.fixedDeltaTime;
                }
                else if (addPointsCDTimer <= 0)
                {
                    AddPoints();
                    addPointsCDTimer = addPointsCD;
                    cycleEnded = false;
                }
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
            weeds.tulipaCounter -= 1;
            animator.SetTrigger("IsDead");
            isDead = true;
            this.enabled = false;
        }

        CheckIfShy();
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

    private void CheckIfShy()
    {
        Collider2D[] plants = Physics2D.OverlapCircleAll(gameObject.transform.GetChild(0).transform.position, shyRange, plantLayer);

        if (plants.Length > shyLimit) isShy = true;
        else isShy = false;

        Debug.Log("There are " + plants.Length + " plants in the area.");
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

        player.playerScore += tulipaPoints;

        scoreGainedText.text = "+ " + tulipaPoints.ToString();
    }

    //Make tulipa despawn after her life span finished
    private void ByeTulipa()
    {
        animator.SetTrigger("ByeTulipa");
        weeds.tulipaCounter -= 1;
        isBye = true;
        this.enabled = false;
    }

    private void OnDrawGizmosSelected()
    {
        if (transform == null) return;

        Gizmos.DrawWireSphere(gameObject.transform.GetChild(0).transform.position, shyRange);
    }
}