using UnityEngine;

public class GreenThumbTulipa : MonoBehaviour
{
    //General variables
    [Header("References")]
    public GameObject icon;

    private void Start()
    {
        icon.SetActive(true);
    }

    //Will check which bladeweeds have been cut to then activete di effect of this skill
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
