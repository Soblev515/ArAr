using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Firebase;
using Firebase.Storage;     

public class LoadFirebaseStorage : MonoBehaviour
{
    private FirebaseStorage Storage;
    void Start()
    {
        Storage = FirebaseStorage.DefaultInstance;
        
    }

    /* storagePath format: "{folder}/{folder}/.../{folder}"
     * fileName format: "{nameFile}
     * expansion format: ".{expansion}"*/
    public void StartUpload(string importPath, string storagePath, string fileName, string expansion)
    {
        StartCoroutine(UploadData(importPath, storagePath, fileName, expansion));
    }

    public void StartDownLoad(Image IMG, string storagePath, string fileName)
    {
        StartCoroutine(DownLoadData(IMG, storagePath, fileName));
    }

    private IEnumerator UploadData(string importaPath, string storagePath, string fileName, string expansion)
    {
        var storageRef = Storage.RootReference.Child(storagePath).Child(fileName + expansion);

        //Вот тут надо придумать и в file записать фотографию как текстуту
        var file = Resources.Load(importaPath) as Texture2D;
        var bytes = file.EncodeToPNG();
        var new_metadata = new MetadataChange();
        new_metadata.ContentType = "image/png";
        Debug.Log(storageRef);
        Debug.Log("Download/" + fileName);

        var uploadTask = storageRef.PutBytesAsync(bytes, new_metadata);
        yield return new WaitUntil(() => uploadTask.IsCompleted);
        Debug.Log(uploadTask.Exception);
        Debug.Log("Upload is completed");
    }

    public IEnumerator DownLoadData(Image IMG, string storagePath, string fileName)
    {
        Debug.Log("path: " + storagePath);
        Debug.Log("file name: " + fileName);
        var storageRef = Storage.RootReference.Child(storagePath).Child(fileName);
        var downloadTask = storageRef.GetBytesAsync(long.MaxValue);
        yield return new WaitUntil(() => downloadTask.IsCompleted);
        Texture2D file = new Texture2D(2, 2);
        file.LoadImage(downloadTask.Result);
        Sprite sprite = Sprite.Create(file, new Rect(0.0f, 0.0f, file.width, file.height),
                                            new Vector2(0.5f, 0.5f), 100.0f);
        IMG.GetComponent<Image>().overrideSprite = sprite;
    }
}
