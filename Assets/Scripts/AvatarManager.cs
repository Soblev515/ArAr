using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Linq;

public class AvatarManager : MonoBehaviour
{
    public GameObject filesListPan, filesContent, filePrefab;
    public RawImage avatarImg;

    private DirectoryInfo dirInfo = new DirectoryInfo("/mnt/Phone/");
    private FileInfo[] files;

    void Start()
    {
        
    }

    public void LoadAvatarList() 
    {
        filesListPan.SetActive(true); avatarImg.gameObject.SetActive(false);
        files = dirInfo.GetFiles("*.jpg", SearchOption.AllDirectories);
        foreach (FileInfo f in files) 
        {
            Instantiate(filePrefab, filesContent.transform);
        }
    }
}
