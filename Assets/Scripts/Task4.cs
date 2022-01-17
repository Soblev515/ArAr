using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class Task4 : MonoBehaviour
{
    public InputField inputField;
    public Text t;
    public Button btn;
    [SerializeField]
    List<string> answers;
    private int count = 0;
    private List<string> usedAnswers = new List<string>(3);
    public FirebaseManagerMain ManagerMain;
    public Text score;

    private void Start()
    {
        btn.onClick.AddListener(InputOnClick);
    }

    public void InputOnClick()
    {
        Debug.Log("Log Input" + inputField.text);

        if (!usedAnswers.Contains(inputField.text) & answers.Contains(inputField.text))
        {
            count += 1;
            Debug.Log(count);
            usedAnswers.Add(inputField.text);
            t.text = "Ответ правильный";
            score.text = count + "/3";
        }
        else
        {
            t.text = "Ответ неправильный";
        }
        if (count == 3)
        {
            ChangedValue();
            Info.CorrectStage += 1;
            ManagerMain.LoadLevel();
        }
    }

    private void ChangedValue()
    {
        t.text = "";
    }
}
