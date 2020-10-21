using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    private static bool isPaused = false;
    public GameOverSystem gameOverSystem;
    public GameObject pauseMenu;

    private void Update()
    {
        if (gameOverSystem.gameOver == false)
        if (Input.GetButtonDown("Pause"))
        {
            if(isPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    private void Pause()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }


    public void Resume()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    public void Quit()
    {
        Application.Quit();
    }
}
