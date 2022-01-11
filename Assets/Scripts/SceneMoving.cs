using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneMoving : MonoBehaviour
{
    [SerializeField]
    string sceneName;

    public void ToScene() 
    {
        SceneManager.LoadScene(sceneName);
    }

    public void LoadScene(string nameScene) 
    {
        SceneManager.LoadScene(nameScene);
    }
}
