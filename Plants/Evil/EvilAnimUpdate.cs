using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvilAnimUpdate : StateMachineBehaviour
{
    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.gameObject.GetComponent<EvilWeed>().cycleEnded = true;
    }
}
