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
    public int health = 1;

    //Bool used to check if the plant is still alive or not
    public bool isDead;

    private void Awake()
    {
        weeds = GameObject.FindGameObjectWithTag("Spawner").GetComponent<WeedsSpawnSystem>();
    }

    private void Update()
    {
        if (health <= 0)
        {
            transform.GetChild(0).gameObject.SetActive(true);
            weeds.bladeCounter -= 1;
            animator.SetTrigger("IsDead");
            isDead = true;
            this.enabled = false;
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
}
