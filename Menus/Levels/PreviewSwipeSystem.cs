using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PreviewSwipeSystem : MonoBehaviour
{
    public Color[] color;
    public GameObject scrollbar;
    public float scrollPos = 0;
    public float[] pos;
    public float distance;

    private void Awake()
    {
        pos = new float[transform.childCount];
        distance = 1f / (pos.Length - 1f);

        for (int i = 0; i < pos.Length; i++)
        {
            pos[i] = distance * i;
        }
    }

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            scrollPos = scrollbar.GetComponent<Scrollbar>().value;
        }
        else
        {
            for (int i = 0; i < pos.Length; i++)
            {
                if(scrollPos < pos[i] + (distance/2) && scrollPos > pos[i] - (distance / 2))
                {
                    scrollbar.GetComponent<Scrollbar>().value = Mathf.Lerp(scrollbar.GetComponent<Scrollbar>().value, pos[i], 0.15f);
                }
            }
        }

        for (int i = 0; i < pos.Length; i++)
        {
            if (scrollPos < pos[i] + (distance / 2) && scrollPos > pos[i] - (distance / 2))
            {
                transform.GetChild(i).localScale = Vector2.Lerp(transform.GetChild(i).localScale, new Vector2(1f, 1f), 0.1f);
                transform.GetChild(i).GetChild(0).GetComponent<Image>().color = color[0];
                transform.GetChild(i).GetChild(transform.GetChild(i).childCount - 1).gameObject.SetActive(false);
                for (int j = 0; j < pos.Length; j++)
                {
                    if (j != i)
                    {
                        transform.GetChild(j).localScale = Vector2.Lerp(transform.GetChild(j).localScale, new Vector2(0.8f, 0.8f), 0.1f);
                        transform.GetChild(j).GetChild(transform.GetChild(j).childCount-1).gameObject.SetActive(true);
                    }
                }
            }
        }
    }

    public void ScrollRight(Button button)
    {
        for (int i = 0; i < button.transform.parent.transform.parent.transform.childCount; i++)
        {
            if(button.transform.parent.transform.parent.transform.GetChild(i).transform.name == button.transform.parent.transform.name)
            {
                i++;
                scrollPos = pos[i];
                Swipe(pos, i);
            }
        }
    }

    public void ScrollLeft(Button button)
    {
        for (int i = button.transform.parent.transform.parent.transform.childCount - 1; i > 0; i--)
        {
            if (button.transform.parent.transform.parent.transform.GetChild(i).transform.name == button.transform.parent.transform.name)
            {
                i--;
                scrollPos = pos[i];
                Swipe(pos, i);
            }
        }
    }

    private void Swipe(float[] pos, int nextPosIndex)
    {
        for (int i = 0; i < pos.Length; i++)
        {
            if(scrollPos < pos[i] + (distance/2) && scrollPos > pos[i] - (distance / 2))
            {
                scrollbar.GetComponent<Scrollbar>().value = Mathf.Lerp(scrollbar.GetComponent<Scrollbar>().value, pos[nextPosIndex], 1f * Time.deltaTime);
            } 
        }
    }

    /*public void OpenWindow(int index)
    {
        scrollPos = pos[index-1];
        Swipe(pos, index);
    }*/

    public void OpenWindow(int index)
    {
        scrollPos = pos[index - 1];
        for (int i = 0; i < pos.Length; i++)
        {
            if (scrollPos < pos[i] + (distance / 2) && scrollPos > pos[i] - (distance / 2))
            {
                scrollbar.GetComponent<Scrollbar>().value = scrollPos;
            }
        }
    }
}
