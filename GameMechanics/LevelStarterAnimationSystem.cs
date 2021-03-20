using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelStarterAnimationSystem : StateMachineBehaviour
{
    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Time.timeScale = 1f;
        GameObject.FindGameObjectWithTag("CD").GetComponent<CountDownSystem>().canStart = true;
    }
}
