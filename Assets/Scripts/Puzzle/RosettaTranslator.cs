using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RosettaTranslator : MonoBehaviour
{
    public void MakeUIActive()
    {
        gameObject.SetActive(true);
    }
    public void MakeUIDisappear()
    {
        gameObject.SetActive(false);
    }
}
