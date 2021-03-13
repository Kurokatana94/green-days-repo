using UnityEngine;
using System.IO;
using System;

//Class that take care of the load page
public class SaveSlotPage : MonoBehaviour
{
    [Tooltip("Insert the saveslot folder")]
    public GameObject slotsFolder;

    private GameMaster gameMaster;
    public GameObject[] slots;

    private void Awake()
    {
        gameMaster = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
        slots = new GameObject[slotsFolder.transform.childCount];
    }

    private void Start()
    {
        slots = new GameObject[slotsFolder.transform.childCount];

        for (int i = 0; i < slots.Length; i++)
        {
            slots[i] = slotsFolder.transform.GetChild(i).GetChild(0).gameObject;
        }
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
                slots[i - 1].GetComponent<SaveSlot>().timeTxt.text = TimeSpan.FromSeconds(data.timePlayed).ToString(@"hh\:mm\:ss");
                slots[i - 1].GetComponent<SaveSlot>().moneyTxt.text = data.totalMoney.ToString();
                slots[i - 1].GetComponent<SaveSlot>().starsTxt.text = data.acquiredStars.ToString();
            }
            else break;
        }
    }
}
