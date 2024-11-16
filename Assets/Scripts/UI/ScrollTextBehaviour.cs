using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScrollTextBehaviour : MonoBehaviour
{
    public TextMeshProUGUI mainText;

    public void Start()
    {
        if(mainText is null)
        {
            mainText = GetComponent<TextMeshProUGUI>();
        }
        //mainText.text = "Example text";
    }

    public void SetText(string text)
    {
        mainText.text = text;
    }

    public string GetText()
    {
        return mainText.text;
    }
}
