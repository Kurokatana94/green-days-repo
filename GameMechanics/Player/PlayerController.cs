using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //General variables
    private Rigidbody2D rb;
    public bool facingRight;
    public bool facingLeft;
    public float speed;
    private float realSpeed;
    private float moveInputHorizontal;
    private float moveInputVertical;
    private float moveSpeed;
    public Animator animator;
    private PowerUpSpawnSystem powerUp;
    public bool canWalk = true;
    
    //Speed Up skill variables
    public float speedMultiplier, speedBoost;

    //Scythe power up variables
    private SlashSystem slash;
    private float slashRateDefault;

    // Score-Based level variables 
    public int playerScore;
    public int gainedPoints;

    // Time-Based level variables
    public GameOverSystem gameOver;
    public float timeLeft;
    public float plantsKilled;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        powerUp = GetComponent<PowerUpSpawnSystem>();
        slash = GetComponent<SlashSystem>();
        slashRateDefault = slash.slashRate;
        facingRight = true;
        realSpeed = speed;
    }

    private void FixedUpdate()
    {
        if (canWalk)
        {
            if (Input.GetButton("Horizontal") || Input.GetButton("Vertical"))
            {
                rb.velocity = new Vector2(0f, 0f);
                moveInputHorizontal = Input.GetAxisRaw("Horizontal");
                moveInputVertical = Input.GetAxisRaw("Vertical");
                rb.velocity = new Vector2(moveInputHorizontal * speed * speedBoost, moveInputVertical * speed * speedBoost);
                moveSpeed = Mathf.Abs(rb.velocity.x) + Mathf.Abs(rb.velocity.y);
                animator.SetFloat("WalkSpeed", moveSpeed);
                if (Input.GetButton("Horizontal") && Input.GetButton("Vertical"))
                {
                    rb.velocity *= 0.7f;
                }
            }
            else
            {
                animator.SetFloat("WalkSpeed", 0f);
                rb.velocity = new Vector2(0f, 0f);
            }
        }
    }

    private void Update()
    {
        if (moveInputHorizontal > 0)
        {
            facingRight = true;
            facingLeft = false;
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
        else if(moveInputHorizontal < 0)
        {
            facingLeft = true;
            facingRight = false;
            transform.eulerAngles = new Vector3(0, 180, 0);
        }

        /*if (powerUp.haveBoots) 
        {
            speedBoost = speedMultiplier;
        }
        else
        {
            speedBoost = 1;
        }

        if (powerUp.haveScythe)
        {
            slash.slashRate = 10f;
        }
        else
        {
            slash.slashRate = slashRateDefault;
        }

        if (powerUp.haveX2)
        {
            gainedPoints *= 2;
        }*/

        playerScore += gainedPoints;

        gainedPoints = 0;
    }
}
