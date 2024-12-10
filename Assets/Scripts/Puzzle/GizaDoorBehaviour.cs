using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GizaDoorBehaviour : MonoBehaviour
{
    [TextArea(3, 10)]
    [SerializeField] private string text = "Error - No text was added to this object";
    [SerializeField] private PlayerController player;
    private string adviceText = "Press E to interact";
    private AdviceTextBehaviour adviceTextObject;
    public UnityEvent<string> onCollect;
    public UnityEvent onVictory;

    public bool isAdviceVisible;

    void Start()
    {
        if (adviceTextObject == null)
        {
            adviceTextObject = GameObject.FindGameObjectWithTag("AdviceText").GetComponent<AdviceTextBehaviour>();
        }
    }

    void Update()
    {
        if (isAdviceVisible && Input.GetKeyDown(KeyCode.E))
        {
            DisableAdvice();
            if (player.hasKey)
            {
                onVictory?.Invoke();
                isAdviceVisible = false;
                return;
            }
            onCollect?.Invoke(text);
            isAdviceVisible = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerController>())
        {
            EnableAdvice(adviceText);
            isAdviceVisible = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerController>())
        {
            DisableAdvice();
            isAdviceVisible = false;
        }
    }

    public void DisableAdvice()
    {
        adviceTextObject.DisableAdvice();
    }

    public void EnableAdvice(string adText = "")
    {
        adviceTextObject.ViewAdvice(adText);
    }

    public void MakeVisible()
    {
        gameObject.SetActive(true);
    }
}
