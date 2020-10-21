using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlashEnd : StateMachineBehaviour
{
    private PlayerController player;
    private Rigidbody2D rb;
    private PowerUpSpawnSystem powerUp;


    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        powerUp = GameObject.FindGameObjectWithTag("Player").GetComponent<PowerUpSpawnSystem>();
        rb = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();

        if (powerUp.haveScythe == false)
        {
            player.canWalk = false;
            animator.SetFloat("WalkSpeed", 0f);
            rb.velocity = new Vector2(0f, 0f);
        }
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player.canWalk = true;
    }
}
