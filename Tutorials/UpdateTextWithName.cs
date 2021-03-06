using UnityEngine;
using TMPro;

public class UpdateTextWithName : MonoBehaviour
{
    public TMP_InputField name;

    private void Update()
    {
        gameObject.GetComponent<TextMeshProUGUI>().text = "Are you sure you want to use \"" + name.text + "\" as your name?";
    }
}
