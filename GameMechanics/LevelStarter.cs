using UnityEngine;

public class LevelStarter : MonoBehaviour
{
    private bool wasSet = false;

    private void Update()
    {
        if(Time.timeScale != 0 && !wasSet)
        {
            Time.timeScale = 0f;
            gameObject.GetComponent<Animator>().SetTrigger("Start");
            wasSet = true;
        }
    }
}
