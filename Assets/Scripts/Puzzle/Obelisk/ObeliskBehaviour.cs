using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObeliskBehaviour : MonoBehaviour
{
    [SerializeField] private int obNum = 0;
    [SerializeField] private ObeliskPuzzle obelisk;
    private bool isMoving;
    private bool isAdviceVisible;
    private AdviceTextBehaviour adviceTextObject;

    private Transform player;

    void Awake()
    {
        if(obelisk is null)
        {
            obelisk = GetComponentInParent<ObeliskShadow>().gameObject.GetComponentInParent<ObeliskPuzzle>();
        }
        if (adviceTextObject == null)
        {
            adviceTextObject = GameObject.FindGameObjectWithTag("AdviceText").GetComponent<AdviceTextBehaviour>();
        }
    }

    void Update()
    {
        
        if (isMoving && Input.GetKeyDown(KeyCode.E))
        {
            var xPos = Mathf.RoundToInt(player.position.x);
            obelisk.MoveObelisk(obNum, xPos);
            isMoving = false;
            adviceTextObject.DisableAdvice();
            isAdviceVisible = false;
        }
        else if (isMoving && Input.GetKeyDown(KeyCode.Tab))
        {
            isMoving = false;
            adviceTextObject.DisableAdvice();
            isAdviceVisible = false;
        }
        if (isAdviceVisible && Input.GetKeyDown(KeyCode.E))
        {
            adviceTextObject.ViewAdvice("Press E to put Obeslik in your position, Press Tab to stop");
            isMoving = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerController>())
        {
            if(player is null)
            {
                player = collision.transform;
            }
            if (!isAdviceVisible)
            {
                adviceTextObject.ViewAdvice("Press E to move Obelisk");
                isAdviceVisible = true;

            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerController>())
        {
            if (isMoving == false)
            {
                adviceTextObject.DisableAdvice();
                isAdviceVisible = false;
            }
        }
    }
}
