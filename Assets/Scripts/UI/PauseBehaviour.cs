using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseBehaviour : MonoBehaviour
{
    private bool isPaused = false;
    private bool isShowingUI;

    [SerializeField] private GameObject pauseUI;

    void Update()
    {
        // Toggle pause when the user presses the Escape key
        if (!isShowingUI && Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }

    public void TogglePause()
    {
        if (isPaused)
        {
            ResumeGame();
            pauseUI.SetActive(false);
        }
        else
        {
            PauseGame();
            pauseUI.SetActive(true);
        }
    }

    public void PauseGame()
    {
        Time.timeScale = 0; // Freeze time
        isPaused = true;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1; // Resume time
        isPaused = false;
    }

    public void PauseGameOnUI()
    {
        if (!isPaused)
        {
            PauseGame();
            isShowingUI = true;
        }
    }
    public void ResumeGameOnUI()
    {
        if (isPaused)
        {
            ResumeGame();
            isShowingUI = false;
        }
    }
}
