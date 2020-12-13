using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwirlEnd : StateMachineBehaviour
{
    private SwirlAttackSystem swirl;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        swirl = GameObject.FindGameObjectWithTag("Skill").GetComponent<SwirlAttackSystem>();
        Debug.Log("GameObject was found");
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        swirl.isActive = false;
        swirl.isReady = false;
        swirl.frame.SetActive(false);
        Debug.Log("Process completed");
    }
}
