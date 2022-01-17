using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{
    public InputField InputField;
    public string Answer;
    public Text Text;

    public void ClickButt()
    {
        if (InputField.text == Answer)
            Text.text = "You Win!!!";
    }
}
