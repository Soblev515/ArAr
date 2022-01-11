using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseUI : MonoBehaviour
{
    [SerializeField]
    PopupUIController Controller;
    private void OnClick()
    {
        Controller.HideMenu();
    }

}
