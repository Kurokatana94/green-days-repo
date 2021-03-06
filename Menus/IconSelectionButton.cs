﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IconSelectionButton : Selectable
{
    public GameObject highlighting;

    private void Update()
    {
        if (IsHighlighted() == true)
        {
            highlighting.SetActive(true);
        }
        else
        {
            highlighting.SetActive(false);
        }
    }
}
