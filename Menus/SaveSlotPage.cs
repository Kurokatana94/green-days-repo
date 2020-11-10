using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using TMPro;

//Class that take care of the load page
public class SaveSlotPage : MonoBehaviour
{
    private GameMaster gameMaster;
    public GameObject[] slots;

    private void Awake()
    {
        gameMaster = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
    }

    //Function that finds which slots are occupied activating them and it fills
    //them up with the info for the player
    public void SlotsToGM()
    {
        gameMaster.slots = slots;
        for (int i = 1; i <= gameMaster.totalSlots; i++)
        {
            string path = Application.persistentDataPath + "/data" + i + ".gd";
            if (File.Exists(path))
            {
                slots[i - 1].SetActive(true);
                GameData data = SaveSystem.LoadGame(i);
                slots[i - 1].GetComponent<SaveSlot>().nameTxt.text = data.name;
                slots[i - 1].GetComponent<SaveSlot>().timeTxt.text = data.date.ToString();
                slots[i - 1].GetComponent<SaveSlot>().moneyTxt.text = data.totalMoney.ToString();
                slots[i - 1].GetComponent<SaveSlot>().starsTxt.text = data.totalStars.ToString();
            }
            else break;
        }
    }
}
