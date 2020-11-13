using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlashSystem : MonoBehaviour
{
    public float slashRange;
    public Transform slashPoint;
    public Animator animator;
    public LayerMask plantLayers;
    public float slashRate = 3f;
    private float nextSlashTime;
    public AudioSource audio;

    //Bool used to check if reset timer in cutter run mode
    public bool hasHit;

    private void Update()
    {
        if (Time.time >= nextSlashTime)
        {
            if (Input.GetButtonDown("Slash"))
            {
                Slash();
                nextSlashTime = Time.time + 1f / slashRate;
            }
        }
    }

    private void Slash()
    {
        animator.SetTrigger("IsSlashing");

        Collider2D[] hitPlant = Physics2D.OverlapCircleAll(slashPoint.position, slashRange, plantLayers);

        foreach(Collider2D plant in hitPlant)
        {
            hasHit = true;

            if (plant.CompareTag("Evil"))
            {
                plant.GetComponent<EvilWeed>().GotHit();
                audio.Play();
            }else if (plant.CompareTag("Tulipa"))
            {
                plant.GetComponent<Tulipa>().GotHit();
                audio.Play();
            }else if (plant.CompareTag("Bush"))
            {
                plant.GetComponent<Bush>().GotHit();
                audio.Play();
            }else if (plant.CompareTag("Green"))
            {
                plant.GetComponent<GreenWeed>().GotHit();
                audio.Play();
            }else if (plant.CompareTag("Blade"))
            {
                plant.GetComponent<BladeWeed>().GotHit();
                audio.Play();
            }else if (plant.CompareTag("Gold"))
            {
                plant.GetComponent<GoldenWeed>().GotHit();
                audio.Play();
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (slashPoint == null) return;

        Gizmos.DrawWireSphere(slashPoint.position, slashRange);
    }
}
