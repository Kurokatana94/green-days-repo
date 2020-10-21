using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ContinueButton : MonoBehaviour
{
    public TextMeshProUGUI text;
    private int clickCounter;
    public string text1, text2, text3, text4, text5, text6, text7;
    public GameObject legend;

    private void Update()
    {
        if (clickCounter == 0)
        {
            text.text = text1;
        }
        else if (clickCounter == 1)
        {
            text.text = text2;
        }
        else if (clickCounter == 2)
        {
            text.text = "";
            

            legend.SetActive(true);
        }
        else if (clickCounter == 3)
        {
            text.text = text3;
            
            legend.SetActive(false);


        }
        else if (clickCounter == 4)
        {
            clickCounter = 0;
        }
    }

    public void Click()
    {
        clickCounter += 1;
    }

}