using System.Collections.Generic;
using UnityEngine;

public class UIList : MonoBehaviour
{
    [SerializeField]
    public List<GameObject> UIs = new List<GameObject>();

    public List<GameObject> GetList()
    {
        return UIs;
    }

    public GameObject GetUI(string name)
    {
        foreach (var UI in UIs)
        {
            if (UI.name.Equals(name))
            {
                return UI;
            }
        }

        return UIs[0];
    }

    public GameObject GetActivUI()
    {
        foreach (var UI in UIs)
        {
            if (UI.activeInHierarchy)
            {
                return UI;
            }
        }

        return UIs[0];
    }

    public void Add(GameObject gameObject)
    {
        UIs.Add(gameObject);
    }
}