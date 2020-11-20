using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwirlAttackSystem : MonoBehaviour
{
    public float skillRange;
    public Transform skillPoint;
    public Animator animator;
    public LayerMask plantLayers;
    public AudioSource audio;
    public float skillCD;
    public double skillCDTimer;


    //Bool used to check if reset timer in cutter run mode
    public bool hasHit, isActive, isReady;

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
            animator.SetTrigger("IsActivated");
        }

        if (isActive)
        {
            SwirlAttack();
        }
    }

    private void SwirlAttack()
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
            else if (plant.CompareTag("Bush") && !plant.GetComponent<Bush>().alreadyHit)
            {
                plant.GetComponent<Bush>().GotHit();
                audio.Play();
                plant.GetComponent<Bush>().alreadyHit = true;
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
