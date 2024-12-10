using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class CollectableBehaviour : MonoBehaviour
{
    [SerializeField] private bool disableOnCollect;
    [TextArea(3, 10)]
    [SerializeField] private string text = "Error - No text was added to this object";
    [SerializeField] private AdviceTextBehaviour adviceTextObject;
    private string adviceText = "Press E to interact";
    public UnityEvent<string> onCollect;

    public bool isAdviceVisible;

    void Awake()
    {
        if (adviceTextObject is null)
        {
            adviceTextObject = GameObject.FindGameObjectWithTag("AdviceText").GetComponent<AdviceTextBehaviour>();
        }
    }

    void Update()
    {
        if(isAdviceVisible && Input.GetKeyDown(KeyCode.E))
        {
            DisableAdvice();
            onCollect?.Invoke(text);
            isAdviceVisible = false;
            if (disableOnCollect) gameObject.SetActive(false);
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

    public void EnableAdvice(string adText)
    {
        adviceTextObject.ViewAdvice(adText);
    }

    public void MakeVisible()
    {
        gameObject.SetActive(true);
    }
}

