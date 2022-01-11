using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupUIController : MonoBehaviour
{
    [SerializeField]
    public float moveSpeed;
    [SerializeField]
    private Vector2 startPos;
    [SerializeField]
    private Vector2 showPos;

    [SerializeField]
    private bool canTouch;

    private RectTransform tr;
    private Vector2 target;
    private Vector2 startTouch;
    void Start()
    {
        startPos = new Vector2(-1200, 0);
        showPos = new Vector2(0, 0);
        tr = transform as RectTransform;
        target = startPos;
    }

    void Update()
    {
        tr.anchoredPosition = Vector2.MoveTowards(tr.anchoredPosition,
                                                    target, moveSpeed * Time.deltaTime);
        if (Input.touchCount > 0 && canTouch)
        {
            Touch touch = Input.touches[0];

            switch (touch.phase)
            {
                case TouchPhase.Began: startTouch = touch.position; break;
                case TouchPhase.Moved:
                    //swipe horizontal?
                    if (touch.position.x - startTouch.x > 20)
                        ShowMenu();//show menu
                    if (touch.position.x - startTouch.x < -20)
                        HideMenu();//hide menu
                    break;
            }
        }
    }

    public void ShowMenu()
    {
        target = showPos;
        tr.anchoredPosition = Vector2.MoveTowards(tr.anchoredPosition,
                                                     target, moveSpeed * Time.deltaTime);
    }

    public void HideMenu()
    {
        target = startPos;
        tr.anchoredPosition = Vector2.MoveTowards(tr.anchoredPosition,
                                                     target, moveSpeed * Time.deltaTime);
    }
}