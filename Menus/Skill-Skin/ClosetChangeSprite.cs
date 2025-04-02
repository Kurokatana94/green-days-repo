using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClosetChangeSprite : StateMachineBehaviour
{
    private SkinMenuSystem system;
    private GameMaster gameMaster;

    private void Awake()
    {
        gameMaster = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        system = animator.transform.parent.parent.GetComponent<SkinMenuSystem>();

        for (int i = 0; i < gameMaster.skinActive.Length; i++)
        {
            if (i == system.selectedSkin)
            {
                gameMaster.skinActive[i] = true;
                system.skinPreviews[i].SetActive(true);
            }
            else
            {
                gameMaster.skinActive[i] = false;
                system.skinPreviews[i].SetActive(false);
            }
        }
    }
}
