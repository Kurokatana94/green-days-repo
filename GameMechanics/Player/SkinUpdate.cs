using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinUpdate : MonoBehaviour
{
    private GameMaster gameMaster;
    public AnimatorOverrideController[] skins;
    private Animator animator;

    private void Awake()
    {
        gameMaster = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
        animator = gameObject.GetComponent<Animator>();
    }

    private void Start()
    {
        for (int i = 1; i < gameMaster.haveSkin.Length; i++)
        {
            if (gameMaster.skinActive[i] == true) animator.runtimeAnimatorController = skins[i-1] as RuntimeAnimatorController;
        }
    }
}
