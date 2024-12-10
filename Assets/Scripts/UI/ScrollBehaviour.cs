using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollBehaviour : MonoBehaviour
{
    [SerializeField] private ScrollTextBehaviour mainText;
    [SerializeField] private Animator animator;
    [SerializeField] private bool animationDisplaysText;

    private PauseBehaviour pause;
    private bool isUIShown;

    void Start()
    {
        if(mainText is null)
        {
            mainText = GetComponentInChildren<ScrollTextBehaviour>();
        }
        if(animator is null)
        {
            animator = gameObject.GetComponent<Animator>();
        }
        pause = GetComponentInParent<PauseBehaviour>();

    }
    void Update()
    {
        // Check if the user clicked the screen
        //if (Input.GetMouseButtonDown(0)) // 0 is the left mouse button or tap on mobile
        //{
        //    Debug.Log("Should run animation");
        //    SetTextAndOpenScroll("Example Text");
        //}
        if(isUIShown && Input.GetKeyDown(KeyCode.Return))
        {
            NextPageOrClose();
        }
    }

    public void SetTextAndOpenScroll(string text)
    {
        if (isUIShown)
        {
            return;
        }
        //Debug.Log(mainText);
        mainText.TMPText.pageToDisplay = 1;
        mainText.SetText(text);
        animator.enabled = true;
        animator.ResetTrigger("ScrollOpen");
        animator.SetTrigger("ScrollOpen");
        pause.PauseGameOnUI();
    }
    public void OnAnimationEnd()
    {
        //Debug.Log(mainText.TMPText.textInfo.pageCount);
        mainText.gameObject.SetActive(true);
        isUIShown = true;
    }

    public void NextPageOrClose()
    {
        var pages = mainText.TMPText;
        if(pages.pageToDisplay < pages.textInfo.pageCount)
        {
            pages.pageToDisplay++;
        }
        else
        {
            CloseScroll();
        }
    }

    private void CloseScroll()
    {
        animator.SetTrigger("ScrollClose");
        mainText.gameObject.SetActive(false);
        pause.ResumeGameOnUI();
        isUIShown = false;
    }
}
