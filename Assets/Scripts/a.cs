using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class a : MonoBehaviour
{
    public FirebaseManagerMain ManagerMain;
    public void Start()
    {
        ManagerMain.SetProfile();
    }
}
