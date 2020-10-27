using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;

public class BladeWeed : MonoBehaviour
{
    private WeedsSpawnSystem weeds;
    public Animator animator;
    private SlashSystem slash;

    public float slashRange;
    public Transform slashPoint;
    public LayerMask plantLayers;
    public int health = 1;

    private double delayTimer = .2;
    private bool isReady;

    //Bool used to check if the plant is still alive or not
    public bool isDead;

    private void Awake()
    {
        weeds = GameObject.FindGameObjectWithTag("Spawner").GetComponent<WeedsSpawnSystem>();
        slash = GameObject.FindGameObjectWithTag("Player").GetComponent<SlashSystem>();
    }

    private void FixedUpdate()
    {
        if (health <= 0)
        {
            if (delayTimer <= 0) isReady = true;
            else delayTimer -= Time.fixedDeltaTime;
        }
    }
    private void Update()
    {
        if (health <= 0)
        {
            if (isReady)
            {
                AoE();
                weeds.bladeCounter -= 1;
                animator.SetTrigger("IsDead");
                isDead = true;
                this.enabled = false;
            }
        }
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("FreePos"))
        {
            weeds.bladeCounter -= 1;
            weeds.SpawnBush();
            Destroy(gameObject);
        }
        else return;
    }

    public void GotHit()
    {
        health -= 1;
    }

    private void AoE()
    {
        Collider2D[] hitPlant = Physics2D.OverlapCircleAll(slashPoint.position, slashRange, plantLayers);

        foreach (Collider2D plant in hitPlant)
        {
            if (plant.transform.position != gameObject.transform.position)
            {
                if (plant.CompareTag("Evil"))
                {
                    plant.GetComponent<EvilWeed>().GotHit();
                    slash.audio.Play();
                }
                else if (plant.CompareTag("Tulipa"))
                {
                    plant.GetComponent<Tulipa>().GotHit();
                    slash.audio.Play();
                }
                else if (plant.CompareTag("Bush"))
                {
                    plant.GetComponent<Bush>().GotHit();
                    slash.audio.Play();
                }
                else if (plant.CompareTag("Green"))
                {
                    plant.GetComponent<GreenWeed>().GotHit();
                    slash.audio.Play();
                }
                else if (plant.CompareTag("Blade"))
                {
                    plant.GetComponent<BladeWeed>().GotHit();
                    slash.audio.Play();
                }
                else if (plant.CompareTag("Gold"))
                {
                    plant.GetComponent<GoldenWeed>().GotHit();
                    slash.audio.Play();
                }
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (slashPoint == null) return;

        Gizmos.DrawWireSphere(slashPoint.position, slashRange);
    }
}
