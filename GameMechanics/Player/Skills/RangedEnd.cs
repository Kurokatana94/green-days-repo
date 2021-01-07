using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnd : StateMachineBehaviour
{
    private Rigidbody2D rb;
    private RangedAttackSystem ranged;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        rb = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
        ranged = GameObject.FindGameObjectWithTag("Skill").GetComponent<RangedAttackSystem>();

        animator.SetFloat("WalkSpeed", 0f);
        rb.velocity = new Vector2(0f, 0f);
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        ranged.isActive = false;
        ranged.isReady = false;
        ranged.frame.SetActive(false);
    }
}
