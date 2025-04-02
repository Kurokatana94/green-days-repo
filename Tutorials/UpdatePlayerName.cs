using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UpdatePlayerName : MonoBehaviour
{
    private GameMaster gameMaster;

    private void Awake()
    {
        gameMaster = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
    }

    public void UpdateName(TMP_InputField inputField)
    {
        gameMaster.name = inputField.text;
    }

    public void ResetName(TMP_InputField inputField)
    {
        inputField.text = "";
    }
}
