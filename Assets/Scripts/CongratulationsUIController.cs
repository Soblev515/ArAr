using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CongratulationsUIController : MonoBehaviour
{
    public void ToMain()
    {
        SceneManager.LoadScene("Main");
    }
}
