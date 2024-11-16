using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CollectableBehaviour : MonoBehaviour
{
    [SerializeField] private bool disableOnCollect;
    public UnityEvent<string> onCollect;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        string text = "Error - No text was added to this object";
        if (collision.gameObject.GetComponent<PlayerController>())
        {
            onCollect?.Invoke(text);
            if (disableOnCollect) gameObject.SetActive(false);
        }
    }
}
