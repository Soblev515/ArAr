using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class QuestManager : MonoBehaviour
{
    public InputField inputField;
    public LoadFirebaseRealtimeData RealtimeData;

    [SerializeField]
    string answer;

    // Update is called once per frame
    public void CheckAnswer()
    {
        if (inputField.text == answer)
        {
            RealtimeData.StageFin(2);
            SceneManager.LoadScene("Congratulations");
        }
    }
}
