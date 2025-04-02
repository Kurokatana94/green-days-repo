using UnityEngine;

public class LinkURL : MonoBehaviour
{
    public void OpenTwitter()
    {
        Application.OpenURL("https://twitter.com/PixelKnight10");
    }

    public void OpenSpecificLink(string link)
    {
        Application.OpenURL(link);
    }

    public void OpenEmail()
    {

    }
}
