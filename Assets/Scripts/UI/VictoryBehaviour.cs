using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VictoryBehaviour : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ActivatePanel()
    {
        Time.timeScale = 0; // Freeze time
        gameObject.SetActive(true);
    }
    public void Retry()
    {
        //Restarts current level
        Time.timeScale = 1; // Freeze time
        gameObject.SetActive(false);
        SceneManager.LoadScene(0);
    }
    public void Quit()
    {
        Application.Quit();
    }
}
