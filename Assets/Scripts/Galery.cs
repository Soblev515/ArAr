using Firebase.Storage;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Galery : MonoBehaviour
{
	public LoadFirebaseRealtimeData RealTime;
	public void OnClick()
	{
		if (NativeGallery.IsMediaPickerBusy())
			return;
		else
			PickImage(100000);
	}

	private FirebaseStorage Storage;
	void Start()
	{
		Storage = FirebaseStorage.DefaultInstance;

	}

	/* storagePath format: "{folder}/{folder}/.../{folder}"
     * fileName format: "{nameFile}
     * expansion format: ".{expansion}"*/
	public void StartUpload(Texture2D img, string storagePath, string fileName, string expansion)
	{
		StartCoroutine(UploadData(img, storagePath, fileName, expansion));
	}

	/*public void StartDownLoad(Image IMG, string storagePath, string fileName, string expansion)
	{
		StartCoroutine(DownLoadData(IMG, storagePath, fileName, expansion));
	}*/

	private IEnumerator UploadData(Texture2D img, string storagePath, string fileName, string expansion)
	{
		var storageRef = Storage.RootReference.Child(storagePath).Child(fileName);

		//¬от тут надо придумать и в file записать фотографию как текстуту
		//var file = Resources.Load(importaPath) as Texture2D;
		var bytes = img.EncodeToPNG();
		var new_metadata = new MetadataChange();
		new_metadata.ContentType = "image/png";
		Debug.Log(storageRef);
		Debug.Log("Download/" + fileName);

		var uploadTask = storageRef.PutBytesAsync(bytes, new_metadata);
		yield return new WaitUntil(() => uploadTask.IsCompleted);
		Debug.Log(uploadTask.Exception);
		Debug.Log("Upload is completed");
		RealTime.SetAnswer(storageRef.ToString());
	}


	/*public void StartUpload(Texture2D pict) 
	{
		StartCoroutine(UploadCoroutine(pict));
	}

	private IEnumerator UploadCoroutine(Texture2D pict) 
	{
		var storage = FirebaseStorage.DefaultInstance;
		Debug.Log(pict);
		var pictReference = storage.GetReference($"/Quest/{System.Guid.NewGuid()}.png"); 
		//ссылка на фото
		var bytes = pict.EncodeToPNG();
		var uploadTask = pictReference.PutBytesAsync(bytes);
		yield return new WaitUntil(() => uploadTask.IsCompleted);

		if (uploadTask.Exception != null) 
		{
			Debug.LogError($"Failed to upload because {uploadTask.Exception }");
			yield break;
		}

		var getUrlTask = pictReference.GetDownloadUrlAsync();
		yield return new WaitUntil(() => getUrlTask.IsCompleted);

		if (getUrlTask.Exception != null) 
		{
			Debug.LogError($"Failed to get a download url with {getUrlTask.Exception}");
			yield break;
		}

		Debug.Log($"Download from {getUrlTask.Result}");
	}*/

	private void PickImage(int maxSize)
	{
		NativeGallery.Permission permission = NativeGallery.GetImageFromGallery((path) =>
		{
			Debug.Log("Image path: " + path);
			if (path != null)
			{
				// Create Texture from selected image
				Texture2D texture = NativeGallery.LoadImageAtPath(path, maxSize, false, true);
				StartUpload(texture, "ActiveQuest/001"/*Storage.RootReference.Child("Resources").ToString()*/, Path.GetFileNameWithoutExtension(path), Path.GetExtension(path));
				if (texture == null)
				{
					Debug.Log("Couldn't load texture from " + path);
					return;
				}
			}
		});

		Debug.Log("Permission result: " + permission);
	}
}
