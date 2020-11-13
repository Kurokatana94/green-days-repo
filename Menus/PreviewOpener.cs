using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PreviewOpener : MonoBehaviour
{
    public GameObject previewMenu;
    public PreviewSwipeSystem swipeSystem;

    public void OpenWindow(int index)
    {
        previewMenu.SetActive(true);
        swipeSystem.OpenWindow(index);
    }
}
