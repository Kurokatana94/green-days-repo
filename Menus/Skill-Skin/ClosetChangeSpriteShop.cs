using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClosetChangeSpriteShop : StateMachineBehaviour
{
    private ShopSystem shop;
    private GameMaster gameMaster;

    private void Awake()
    {
        gameMaster = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        shop = animator.transform.parent.parent.parent.GetComponent<ShopSystem>();

        for (int i = 1; i <= shop.skinButtons.Length; i++)
        {
            if (i == shop.skinN)
            {
                shop.skinPrice.text = shop.skin[i].cost.ToString();
                shop.skinPreviews[i - 1].SetActive(true);
                if (gameMaster.haveSkin[i])
                {
                    shop.skinPrice.text = "Sold Out!";
                }
                Debug.Log("Updated price.");
            }
            else
            {
                shop.skinPreviews[i - 1].SetActive(false);
            }
        }
    }
}
