﻿using UnityEngine;

public class BlueTulipaSeedsEnd : StateMachineBehaviour
{
    private BlueTulipaSeedsSystem seeds;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        seeds = GameObject.FindGameObjectWithTag("Skill").GetComponent<BlueTulipaSeedsSystem>();        
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        seeds.isComplete = true;
        seeds.spawnPoint = new Vector3(seeds.seed.position.x, seeds.seed.position.y, seeds.seed.position.z);
        seeds.frame.SetActive(false);
    }
}
