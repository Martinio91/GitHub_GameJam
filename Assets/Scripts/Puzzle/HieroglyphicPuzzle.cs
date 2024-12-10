using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HieroglyphicPuzzle : MonoBehaviour
{
    [SerializeField] private string correctSequence; // Correct order
    [SerializeField] private TextMeshProUGUI feedbackText; 
    [SerializeField] private GameObject playerInputTMP;
    [SerializeField] private DoorBehaviour door; // The door to unlock
    [SerializeField] private PauseBehaviour pause;

    private TMP_InputField playerInput; // Player's entered sequence
    private bool isUIOpened;

    private void Awake()
    {
        if (pause == null)
        {
            pause = GetComponentInParent<PauseBehaviour>();
        }
    }

    public void OpenScreen()
    {
        playerInput = playerInputTMP.GetComponent<TMP_InputField>();
        gameObject.SetActive(true);
        isUIOpened = true;
        pause.PauseGameOnUI();
    }

    public void CloseScreen()
    {
        ResetInput("");
        gameObject.SetActive(false);
        isUIOpened = false;
        pause.ResumeGameOnUI();
    }

    void Update()
    {
        if(isUIOpened && Input.GetKeyDown(KeyCode.Return))
        {
            OnSubmit();
        }
        else if (isUIOpened && Input.GetKeyDown(KeyCode.Tab))
        {
            CloseScreen();
        }
    }

    // Called when the player presses "Submit"
    private void OnSubmit()
    {
        if (CheckSequence())
        {
            feedbackText.text = "Correct sequence! The door opens.";
            door.OpenDoor();
        }
        else
        {
            ResetInput("Incorrect sequence. Try again.");
        }
    }

    // Check if the entered sequence matches the correct sequence
    private bool CheckSequence()
    {
        if(playerInput.text.ToUpper() == correctSequence)
        {
            return true;
        }
        Debug.Log("IsHere");
        return false;
    }

    // Reset the player's input
    private void ResetInput(string feedback)
    {
        playerInput.text = "";
        feedbackText.text = feedback; // Clear feedback text
    }
}
