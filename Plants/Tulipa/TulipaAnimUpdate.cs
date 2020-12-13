using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TulipaAnimUpdate : StateMachineBehaviour
{
    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.gameObject.GetComponent<Tulipa>().cycleEnded = true;
    }
}