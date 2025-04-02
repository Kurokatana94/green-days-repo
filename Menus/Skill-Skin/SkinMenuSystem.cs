using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkinMenuSystem : MonoBehaviour
{
    private GameMaster gameMaster;
    public Button[] skinButtons;
    public GameObject buttonFolder;
    public GameObject skinPreview;
    public GameObject[] skinPreviews;
    public Animator closet;

    private bool wasOpen = false;
    public int selectedSkin;

    private void Awake()
    {
        gameMaster = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
    }

    private void Start()
    {
        UpdatePreviewArray();
        UpdateButtonArray();
    }

    private void Update()
    {
        for (int i = 0; i < skinButtons.Length; i++)
        {
            if (gameMaster.haveSkin[i]) skinButtons[i].interactable = true;
        }
    }

    public void SkinButton(int button)
    {
        selectedSkin = button;

        if (!wasOpen)
        {
            closet.SetTrigger("Open");
            wasOpen = true;
            for (int i = 0; i < gameMaster.skinActive.Length; i++)
            {
                if (i == button)
                {
                    gameMaster.skinActive[i] = true;
                    skinPreviews[i].SetActive(true);
                }
                else
                {
                    gameMaster.skinActive[i] = false;
                    skinPreviews[i].SetActive(false);
                }
            }
        }
        else
        {
            closet.SetTrigger("Closed Selection");
        }
    }

    private void UpdatePreviewArray()
    {
        skinPreviews = new GameObject[skinPreview.transform.childCount];
        for (int i = 0; i < skinPreview.transform.childCount; i++)
        {
            skinPreviews[i] = skinPreview.transform.GetChild(i).gameObject;
        }
    }

    private void UpdateButtonArray()
    {
        skinButtons = new Button[buttonFolder.transform.childCount];
        for (int i = 0; i < buttonFolder.transform.childCount; i++)
        {
            skinButtons[i] = buttonFolder.transform.GetChild(i).gameObject.GetComponent<Button>();
        }
    }
}
