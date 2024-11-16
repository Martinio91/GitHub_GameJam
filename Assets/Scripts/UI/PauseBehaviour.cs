using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseBehaviour : MonoBehaviour
{
    private bool isPaused = false;

    void Update()
    {
        // Toggle pause when the user presses the Escape key
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }

    public void TogglePause()
    {
        if (isPaused)
        {
            ResumeGame();
        }
        else
        {
            PauseGame();
        }
    }

    public void PauseGame()
    {
        Time.timeScale = 0; // Freeze time
        isPaused = true;
        // pauseMenu.SetActive(true);
    }

    public void ResumeGame()
    {
        Time.timeScale = 1; // Resume time
        isPaused = false;
        // pauseMenu.SetActive(false);
    }

    public void PauseGameOnUI()
    {
        if (!isPaused)
        {
            PauseGame();
        }
    }
}
