using UnityEngine;

public class SkipTutorialSystem : MonoBehaviour
{
    private GameMaster gameMaster;

    private void Awake()
    {
        gameMaster = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
    }
    
    //Small function that if used mark all tutorials as completed unlocking the otherwise unavailable hub functions
    public void SkipTutorial()
    {
        for (int i = 0; i < gameMaster.tutorial.Length; i++)
        {
            gameMaster.tutorial[i] = true;
        }

        gameObject.GetComponent<LockUnavaibleFunctions>().Unlock();
    }
}
