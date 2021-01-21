using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueTulipaSeedsSystem : MonoBehaviour
{
    //General variables
    [Header("References")]
    public GameObject specialTulipa;
    public GameObject icon;
    public GameObject frame;
    public Transform seed;
    public TrackPosition tracker;
    [HideInInspector]
    public Vector3 spawnPoint;
    public Animator animator;
    private PlayerController player;
    private WeedsSpawnSystem spawner;

    [Space]
    //Variables that defines how often the skill can be used
    [Tooltip("Set the skill CD")]
    public float skillCD;
    [Tooltip("Shows the skill CD in real time (Not to modify!)")]
    public double skillCDTimer;

    //Bool used to check if reset timer in cutter run mode
    public bool hasHit;

    //Bool used to check if the different steps of the skill system have been reached granting access to the next one
    public bool isActive, isReady, isComplete = false;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        spawner = GameObject.FindGameObjectWithTag("Spawner").GetComponent<WeedsSpawnSystem>();
    }

    private void Start()
    {
        icon.SetActive(true);
    }

    private void FixedUpdate()
    {
        if (skillCDTimer > 0 && !isReady)
        {
            skillCDTimer -= Time.fixedDeltaTime;
        }
        else
        {
            isReady = true;
            skillCDTimer = skillCD;
        }
    }

    private void Update()
    {
        if (Input.GetButtonDown("Skill"))
        {
            ActivateSkill();
        }

        if (isActive)
        {
            if (isComplete)
            {
                Instantiate(specialTulipa, spawnPoint, Quaternion.identity);
                isActive = false;
                isReady = false;
                tracker.enabled = true;
                isComplete = false;
            }
        }

        CheckIfBouncing();
    }

    public void ActivateSkill()
    {
        if (!isActive && isReady)
        {
            tracker.enabled = false;
            isActive = true;
            if (player.facingRight)
            {
                transform.parent.transform.eulerAngles = new Vector3(0, 0, 0);
            }
            else
            {
                transform.parent.transform.eulerAngles = new Vector3(0, 180, 0);
            }
            animator.SetTrigger("IsActivated");
            player.animator.SetTrigger("IsThrowing");
            frame.SetActive(true);
        }
    }

    private void CheckIfBouncing()
    {
        if (transform.position.x >= spawner.maxX)
        {
            float newRot = transform.parent.eulerAngles.y + 180;
            transform.parent.position = new Vector3((spawner.maxX - transform.parent.position.x) + spawner.maxX, transform.parent.position.y, 0);
            transform.parent.eulerAngles = new Vector3(0, newRot, 0);
        }
        else if(transform.position.x <= spawner.minX)
        {
            float newRot = transform.parent.eulerAngles.y + 180;
            transform.parent.position = new Vector3((spawner.minX - transform.parent.position.x) + spawner.minX, transform.parent.position.y, 0);
            transform.parent.eulerAngles = new Vector3(0, newRot, 0);
        }
    }
}
