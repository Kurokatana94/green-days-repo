using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

//Script containing various function used in shops
public class ShopSystem : MonoBehaviour
{
    private GameMaster gameMaster;
    public DataManagementSystem data;
    public float currentExchangeRate;
    public Button[] skinButtons;
    public Button[] skillButtons;
    public GameObject skills, skins;
    public TextMeshProUGUI skinPrice, skillPrice;

    //Items list with 'name', 'cost' and 'starting quantity' as requirement
    public Item item = new Item("Sample Item 750", 500, 0);

    //Special items list with 'name', 'cost', and boolean to check if the item 
    //will be aquired at the start (skins)
    public SpecialItem basicSkin = new SpecialItem("Basic skin beff", 0, true);
    public SpecialItem skin1 = new SpecialItem("Old Beff", 20000, false);
    public SpecialItem skin2 = new SpecialItem("Skin 2", 10000, false);
    public SpecialItem skin3 = new SpecialItem("Skin 3", 10000, false);

    private SpecialItem[] skin = new SpecialItem[4];

    //Special items list with 'name', 'cost', and boolean to check if the item 
    //will be aquired at the start (skills)
    public SpecialItem skill1 = new SpecialItem("Fast Walk", 1000, false);
    public SpecialItem skill2 = new SpecialItem("Boomerang Blade", 5000, false);
    public SpecialItem skill3 = new SpecialItem("Tornado Blade", 10000, false);
    public SpecialItem skill4 = new SpecialItem("Blade Seeds", 7000, false);
    public SpecialItem skill5 = new SpecialItem("Tulipa Seeds", 7000, false);
    public SpecialItem skill6 = new SpecialItem("Green Thumb", 10000, false);

    private SpecialItem[] skill = new SpecialItem[6];

    //Ints needed to navigate throught the shops items
    private int skinN, skillN;

    private void Awake()
    {
        gameMaster = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
        
        skin[0] = basicSkin;
        skin[1] = skin1;
        skin[2] = skin2;
        skin[3] = skin3;

        skill[0] = skill1;
        skill[1] = skill2;
        skill[2] = skill3;
        skill[3] = skill4;
        skill[4] = skill5;
        skill[5] = skill6;

        skillButtons = new Button[skills.transform.childCount];
        skinButtons = new Button[skins.transform.childCount];
    }

    private void Start()
    {
        skinN = 0;
        skillN = 0;

        UpdateButtonArray(skills, skillButtons);
        UpdateButtonArray(skins, skinButtons);

        UpdateSelection(skillButtons[skillN]);
        UpdateSelection(skinButtons[skinN]);

        for (int i = 1; i <= skinButtons.Length; i++)
        {
            skinButtons[i - 1].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = skin[i].name;
            Debug.Log("Updated "+skinButtons[i-1]+" name to " + skin[i].name);
            if (gameMaster.haveSkin[i])
            {
                skinButtons[i-1].interactable = false;
            }
        }

        for (int i = 0; i < skillButtons.Length; i++)
        {
            skillButtons[i].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = skill[i].name;
            Debug.Log("Updated "+ skillButtons[i]+" name to " + skill[i].name);
            if (gameMaster.haveSkill[i])
            {
                skillButtons[i].interactable = false;
            }
        }
    }

    //Function that autoset an array with the correct needed button in the hierarchy
    private void UpdateButtonArray(GameObject type, Button[] list)
    {
        for (int i = 0; i < type.transform.childCount; i++)
        {
            list[i] = type.transform.GetChild(i).gameObject.GetComponent<Button>();
            Debug.Log("Adding " + list[i] + " to list.");
        }
    }

    //Function used to update current selected item in the shop
    public void UpdateSelection(Button button)
    {
        for (int i = 1; i <= skinButtons.Length; i++)
        {
            if(skinButtons[i-1] == button)
            {
                skinN = i;
                skinPrice.text = skin[skinN].cost.ToString();
                if (gameMaster.haveSkin[skinN])
                {
                    skinPrice.text = "Sold Out!";
                }
                Debug.Log("Updated price.");
            }
        }

        for (int i = 0; i < skillButtons.Length; i++)
        {
            if(skillButtons[i] == button)
            {
                skillN = i;
                skillPrice.text = skill[skillN].cost.ToString();
                if (gameMaster.haveSkill[skillN])
                {
                    skillPrice.text = "Sold Out!";
                }
                Debug.Log("Updated price.");
            }
        }
    }

    //Functions used to buy unique items (unlock skills, skins, etc.)
    public void BuySkin()
    {
        BuyOnce(skin[skinN]);
        if (skin[skinN].isAquired)
        {
            gameMaster.haveSkin[skinN] = true;
            skinButtons[skinN - 1].interactable = false;
            skinPrice.text = "Sold Out!";
        }
    }

    public void BuySkill()
    {
        BuyOnce(skill[skillN]);
        if (skill[skillN].isAquired)
        {
            gameMaster.haveSkill[skillN-1] = true;
            skillButtons[skillN].interactable = false;
            skillPrice.text = "Sold Out!";
        }
    }

    //Function used to buy consumable items
    /*public void BuyItem()
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
    }*/

    //Function used to buy special items that can be bought just once (e.g. skins)
    private void BuyOnce(SpecialItem specialItem)
    {
        if(gameMaster.totalMoney >= specialItem.cost && !specialItem.isAquired)
        {
            gameMaster.totalMoney -= specialItem.cost;
            specialItem.isAquired = true;
            Debug.Log("You bought a special item");
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

    public void ResetAtClose()
    {
        skinN = 0;
        skillN = 0;
        UpdateSelection(skinButtons[skinN]);
        UpdateSelection(skillButtons[skillN]);
    }
}
