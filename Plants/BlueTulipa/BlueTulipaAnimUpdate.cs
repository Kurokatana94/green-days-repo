﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueTulipaAnimUpdate : StateMachineBehaviour
{
    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.gameObject.GetComponent<BlueTulipa>().cycleEnded = true;
    }
}
