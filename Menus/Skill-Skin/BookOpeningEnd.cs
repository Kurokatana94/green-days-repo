using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BookOpeningEnd : StateMachineBehaviour
{
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        for (int i = 0; i < animator.gameObject.transform.childCount; i++)
        {
            animator.gameObject.transform.GetChild(i).gameObject.SetActive(false);
        }
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        for (int i = 0; i < animator.gameObject.transform.childCount; i++)
        {
            animator.gameObject.transform.GetChild(i).gameObject.SetActive(true);
        }
    }
}
