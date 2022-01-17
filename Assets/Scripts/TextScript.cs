using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class TextScript: MonoBehaviour
{
    private Button Button;
    private bool riddle;
    public GameObject RiddleText;

    // Start is called before the first frame update
    void Start()
    {
        Button = GetComponent<Button>();
        Button.onClick.AddListener(RotationFunction);

        RiddleText.SetActive(false);
    }

    // Update is called once per frame
    void RotationFunction()
    {
        if (riddle)
        {
            riddle = false;
            GetComponent<Image>().color = Color.red;
            RiddleText.SetActive(false);
        }
        else
        {
            riddle = true;
            GetComponent<Image>().color = Color.green;
            RiddleText.SetActive(true);
        }
    }

}