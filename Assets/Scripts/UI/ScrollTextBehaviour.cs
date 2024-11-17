using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScrollTextBehaviour : MonoBehaviour
{
    public TextMeshProUGUI TMPText;

    public void Start()
    {
        if(TMPText is null)
        {
            TMPText = GetComponent<TextMeshProUGUI>();
        }
        gameObject.SetActive(false);
    }

    public void SetText(string text)
    {
        TMPText.text = text;
    }

    public string GetText()
    {
        return TMPText.text;
    }
}
