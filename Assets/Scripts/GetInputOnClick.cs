using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GetInputOnClick : MonoBehaviour
{
    public Button btn;
    public string answer;
    public InputField input;
    public Text t;
    public FirebaseManagerMain ManagerMain;
    private void Start()
    {
        btn.onClick.AddListener(InputOnClick);
    }

    public void InputOnClick() 
    {
        Debug.Log("Log Input " + input.text);
        if (input.text.ToLower() == answer.ToLower())
        {
            ChangedValue();
            Info.CorrectStage += 1;
            ManagerMain.LoadLevel();
        }
        else 
        {
            t.text = "Ответ неправильный";
        }
    }

    private void ChangedValue() 
    {
        t.text = "";
    }
}
