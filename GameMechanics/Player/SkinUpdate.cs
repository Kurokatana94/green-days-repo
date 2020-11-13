using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinUpdate : MonoBehaviour
{
    private GameMaster gameMaster;
    public Animator[] skins;
    private Animator animator;

    private void Awake()
    {
        gameMaster = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
    }

    private void Start()
    {
        for (int i = 0; i < gameMaster.haveSkin.Length; i++)
        {
            if(gameMaster.skin[i] == true) animator.runtimeAnimatorController = skins[i].runtimeAnimatorController;
        }
    }
}
