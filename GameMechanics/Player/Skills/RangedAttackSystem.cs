using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedAttackSystem : MonoBehaviour
{
    public Transform skillPoint;
    public Animator animator;
    public LayerMask plantLayers;
    public AudioSource audio;
    public GameObject icon;
    public GameObject frame;
    private PlayerController player;
    public float skillRange;
    public float skillCD;
    public double skillCDTimer;


    //Bool used to check if reset timer in cutter run mode
    public bool hasHit, isActive, isReady;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
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
        if (Input.GetButtonDown("Skill") && !isActive && isReady)
        {
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
            frame.SetActive(true);
        }

        if (isActive)
        {
            RangedAttack();
        }
    }

    private void RangedAttack()
    {
        Collider2D[] hitPlant = Physics2D.OverlapCircleAll(skillPoint.position, skillRange, plantLayers);

        foreach (Collider2D plant in hitPlant)
        {
            hasHit = true;

            if (plant.CompareTag("Evil"))
            {
                plant.GetComponent<EvilWeed>().GotHit();
                audio.Play();
            }
            else if (plant.CompareTag("Tulipa"))
            {
                plant.GetComponent<Tulipa>().GotHit();
                audio.Play();
            }
            else if (plant.CompareTag("Bush"))
            {
                plant.GetComponent<Bush>().GotHit();
                audio.Play();
            }
            else if (plant.CompareTag("Green"))
            {
                plant.GetComponent<GreenWeed>().GotHit();
                audio.Play();
            }
            else if (plant.CompareTag("Blade"))
            {
                plant.GetComponent<BladeWeed>().GotHit();
                audio.Play();
            }
            else if (plant.CompareTag("Gold"))
            {
                plant.GetComponent<GoldenWeed>().GotHit();
                audio.Play();
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (skillPoint == null) return;

        Gizmos.DrawWireSphere(skillPoint.position, skillRange);
    }
}
