using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Firebase;
using Firebase.Firestore;
using UnityEngine.SceneManagement;

public class FirebaseManagerMain : MonoBehaviour
{
    public LoadFirebaseFirestore ManagerFirestore;
    public LoadFirebaseRealtimeData ManagerRealtime;
    public LoadFirebaseStorage ManagerStorage;

    public Quest activQuest;

    [Header("Profile")]
    public InputField UserName;
    public Image Avatar;
    public Button EditButton;
    public Button SaveButton;

    [Header("Quest")]
    public Text QuestName;
    public Text Description;
    public Image QuestImage;

    [Header("JoinRoom")]
    public InputField InputFieldKey;
    public Text ErrorText;

    [Header("Room")]
    public Text QuestNameRoom;
    public Text Key;
    public GameObject UserInfo;
    public Text RoomUserName;
    public Image RoomUserIcon;
    public Transform root;

    public void SetProfile()
    {
        UserName.text = Info.user.Name != null ? Info.user.Name : "TEST";
        //Подключить хранилище Firebase и обращаться к объекту и скачивать
        //avatar.sprite = Info.user.Img;
        Avatar.GetComponent<Image>().overrideSprite = Resources.Load("Assets/StreamingAssets/test.png") as Sprite;
    }

    public void SaveProfile()
    {
        var db = FirebaseFirestore.DefaultInstance;
        var userDataStruct = new UserDataStruct
        {
            Name = UserName.text,
            
            IMG = "Assets/Cjrrt7xGW8Y.jpg"
        };

        db.Collection("Users").Document(Info.Uid).SetAsync(userDataStruct);
        
        SaveButton.enabled = false;
        EditButton.enabled = true;
        EditButton.image.enabled = true;
        UserName.interactable = false; 
        SaveButton.image.enabled = false;
    }

    public void ToEdit()
    {
        UserName.interactable = true;
        EditButton.enabled = false;
        EditButton.image.enabled = false;
        SaveButton.enabled = true;
        SaveButton.image.enabled = true;
    }

    public void OpenQuest(GameObject gameObject)
    {
        Debug.Log(gameObject.name);
        activQuest = ManagerFirestore.GetQuest(gameObject.name);
        ManagerStorage.StartDownLoad(QuestImage, "Quests/" + gameObject.name, activQuest.IMG);
        QuestName.text = activQuest.Name;
        Description.text = activQuest.Description;
        Info.StageURL = activQuest.StageURL;
    }

    public void ExitRoom()
    {
        ManagerRealtime.ExitRoom();

    }

    public void JoinRoom()
    {
        if(InputFieldKey.text != "")
        {
            Debug.Log("I saw Key");
            if (ManagerRealtime.isHaveRoom(InputFieldKey.text))
            {
                ManagerRealtime.JoinRoom(InputFieldKey.text, Info.Uid);
            }
            else
                ErrorText.text = "Key is wrong. Room with it's key don't create now";
        }
        else
            ErrorText.text = "Key is null";
    }

    public void CreateRoom()
    {
        ManagerRealtime.CreateRoom(activQuest);
    }

    public void SetRoom()
    {
        Debug.Log("Set Room");
        Debug.Log(ManagerRealtime.Room.Users.Count);
        try
        {
            Debug.Log("@1");
            QuestNameRoom.text = ManagerRealtime.Room.QuestName;
            Debug.Log("@2");
            Key.text = ManagerRealtime.Room.Name;
            Debug.Log("@3");
            SetUserInRoom();
            Debug.Log("@4");
        }
        catch
        {
            Debug.Log("Error");
        }
    }

    private void SetUserInRoom()
    {
        var users = ManagerRealtime.Room.Users;
        root.DetachChildren();
        foreach (var auser in users)
        {
            Debug.Log(auser);
            var data = FirebaseFirestore.DefaultInstance.Collection("Users").Document(auser);
            var lister = data.GetSnapshotAsync().ContinueWith(snapshot =>
            {
                var user = snapshot.Result.ConvertTo<UserDataStruct>();
                UserName.text = user.Name;
                //Загрузка из Storage изображения
                Debug.Log(user.Name);
                var clone = Instantiate(UserInfo.gameObject) as GameObject;
                clone.name = user.Name;
                clone.SetActive(true);
                clone.transform.SetParent(root);
                Debug.Log("Clone Create");
            });
        }
    }

    public void LoadLevel()
    {
        if (Info.CorrectStage == Info.StageURL.Length)
            SceneManager.LoadScene("Congratulations");
        else
            ManagerRealtime.StartLevel(Info.CorrectStage);
    }
}
