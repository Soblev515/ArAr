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
    private List<string> usedAnswers = new List<string>(4);



    // Update is called once per frame
    /*public void CheckAnswers()
    {
        int count = 0;
        List<string> usedAnswers = new List<string>(4);

        for (int i = 0; i < 4; i++) 
        {
            if (inputField.text == answers[i] & !usedAnswers.Contains(inputField.text))
            {
                count += 1;
                usedAnswers.Add(inputField.text);
            }
            if (count == 4)
                SceneManager.LoadScene("New Task 4.2");
        }
    }*/

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
        }
        else
        {
            t.text = "Ответ неправильный";
        }
        if (count == 4)
        {
            SceneManager.LoadScene("New Task 4.2");
        }
    }

    private void ChangedValue()
    {
        t.text = "";
    }
}
