using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AssetBudleScene : MonoBehaviour
{
    private AssetBundle assetBundle;

    public void LoadLevel(int i)
    {
        
        StartCoroutine(DownloadFiles(i));
    }
    
    private IEnumerator DownloadFiles(int i)
    {
        if (!assetBundle)
        {
            using (WWW www = new WWW(Info.StageURL[i]))
            {
                Debug.Log("I using www");
                yield return www;
                if (!string.IsNullOrEmpty(www.error))
                {
                    Debug.Log(www.error);
                    yield break;
                }
                assetBundle = www.assetBundle;
            }
        }

        string[] scenes = assetBundle.GetAllScenePaths();
        foreach(string s in scenes)
        {
            Debug.Log(s);
            Debug.Log(Path.GetFileNameWithoutExtension(s));
            Debug.Log("Task"+(i+1));
            if(Path.GetFileNameWithoutExtension(s) == "Task" + (i + 1))
            {
                Debug.Log("SceneNameInPath(foreach):: " + Path.GetFileNameWithoutExtension(s));
                LoadAssetBundleScene(Path.GetFileNameWithoutExtension(s));
            }
        }
    }
    private void LoadAssetBundleScene(string name)
    {
        SceneManager.LoadScene(name);
    }

}
