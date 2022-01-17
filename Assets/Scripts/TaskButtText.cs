using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TaskButtText : MonoBehaviour
{
    public Text Text;
    public Image Image;
    bool isHide;

    public void Start()
    {
        isHide = true;
        OnClick();
    }

    public void OnClick()
    {
        isHide = !isHide;
        Text.enabled = isHide;
        Image.enabled = isHide;
    }
}
