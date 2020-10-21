using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Script containing various function used in shops
public class ShopSystem : MonoBehaviour
{
    private GameMaster gameMaster;
    public DataManagementSystem data;
    public float currentExchangeRate;

    //Items list with 'name', 'cost' and 'starting quantity' as requirement
    public Item item = new Item("Sample Item 750", 500, 0);

    //Special items list with 'name', 'cost', and boolean to check if the item 
    //will be aquired at the start
    public SpecialItem basicSkin = new SpecialItem("Basic skin beff", 0, true);
    public SpecialItem skin1 = new SpecialItem("Skin 1 Beff", 10000, false);
    public SpecialItem skin2 = new SpecialItem("Skin 2 Beff", 10000, false);
    private void Awake()
    {
        gameMaster = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
    }

    //Function used to buy unique items (unlock skils, skins, etc.)
    public void BuySkin1()
    {
        BuyOnce(skin1);
    }

    public void BuySkin2()
    {
        BuyOnce(skin2);
    }


    //Function used to buy consumable items
    public void BuyItem()
    {
        BuyMany(item);
        data.AutoSaveGame();
    }

    private void BuyMany(Item item)
    {
        if(gameMaster.totalMoney >= item.cost)
        {
            gameMaster.totalMoney -= item.cost;
            item.quantity++;
            Debug.Log("You bought an item");
        }
        else
        {
            Debug.Log("Not enough money");
        }
    }

    private void BuyOnce(SpecialItem specialItem)
    {
        if(gameMaster.totalMoney >= specialItem.cost && !specialItem.isAquired)
        {
            gameMaster.totalMoney -= specialItem.cost;
            specialItem.isAquired = true;
            Debug.Log("You buyed a special item");
        }
        else
        {
            Debug.Log("Not enough money");
        }
    }

    //Function used to exchange points earned in game with currency usable in shops
    public void ExchangeValues()
    {
        if(gameMaster.totalPoints != 0)
        {
            Debug.Log("You received: " + (int)(gameMaster.totalPoints * currentExchangeRate));
            gameMaster.totalMoney += (int)(gameMaster.totalPoints * currentExchangeRate);
            gameMaster.totalPoints = 0;
        }
        else
        {
            Debug.Log("You didn't have any points to exchange!");
        }

        data.AutoSaveGame();
    }
}
