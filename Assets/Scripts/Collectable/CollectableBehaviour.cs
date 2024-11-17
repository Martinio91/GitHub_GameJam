using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CollectableBehaviour : MonoBehaviour
{
    [SerializeField] private bool disableOnCollect;
    [TextArea(3, 10)]
    [SerializeField] private string text = "Error - No text was added to this object";
    [SerializeField] private UnityEvent<string> onCollect;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerController>())
        {
            onCollect?.Invoke(text);
            if (disableOnCollect) gameObject.SetActive(false);
        }
    }
}
