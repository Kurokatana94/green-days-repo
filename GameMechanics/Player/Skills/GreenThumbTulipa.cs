using UnityEngine;

public class GreenThumbTulipa : MonoBehaviour
{
    //General variables
    [Header("References")]
    public GameObject icon;

    [HideInInspector]

    private void Start()
    {
        icon.SetActive(true);
    }

    private void Update()
    {
        foreach(GameObject bladeWeed in GameObject.FindGameObjectsWithTag("Blde"))
        {
            if (bladeWeed.transform.Find("Blades").gameObject.activeInHierarchy)
            {
                for (int i = 0; i < bladeWeed.transform.Find("Blades").childCount; i++)
                {
                    bladeWeed.transform.Find("Blades").GetChild(i).GetComponent<LeafBladeSystem>().greenThumbActive = true;
                }
            }
        }
    }
}
