using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Firebase;
using Firebase.Firestore;


public class LoadFirebaseFirestore : MonoBehaviour
{
    public LoadFirebaseStorage storage;
    public Button prefabButton;
    public Text NameButton;
    public Text ShortDescription;
    public Image Image;
    public Transform root;

    public static Dictionary<string, Quest> Quests = new Dictionary<string, Quest>();

    public void Awake()
    {
        var db = FirebaseFirestore.DefaultInstance;
        var docs = db.Collection("Quests");
        Lister(docs);
    }

    public Quest GetQuest(string name)
    {
        foreach (var quest in Quests)
        {
            Debug.Log(quest);
        }
        return Quests[name];
    }

    public List<UserDataStruct> GetUserInRoom(List<string> users)
    {
        List<UserDataStruct> result = new List<UserDataStruct>();
        foreach (var user in users)
        {
            var data = FirebaseFirestore.DefaultInstance.Collection("Users").Document(user);
            var lister = data.Listen(snapshot =>
            {
                var user = snapshot.ConvertTo<UserDataStruct>();
                Debug.Log(user.Name);
                result.Add(user);
                Debug.Log(result.Count);
            });
        }
        Debug.Log(result.Count);
        return result;
    }

    private void Lister(CollectionReference docs)
    {
        var lister = docs.Listen(snapshot =>
        {
            root.DetachChildren();
            foreach (var document in snapshot.Documents)
            {
                Debug.Log(document);
                var quest = document.ConvertTo<Quest>();
                Debug.Log(document.Id);
                if(!Quests.ContainsKey(document.Id))
                    Quests.Add(document.Id, quest);
                storage.StartDownLoad(Image, "Quests/" + document.Id, "preview.jpg");
                Debug.Log(quest.StageURL);
                Debug.Log(quest.StageURL[0]);

                NameButton.text = Quests[document.Id].Name;
                ShortDescription.text = Quests[document.Id].ShortDescription;

                Debug.Log("path: " + "Quests/" + document.Id);
                Debug.Log("file name: " + quest.IMG);
                
                var clone = Instantiate(prefabButton.gameObject) as GameObject;
                clone.name = document.Id;
                clone.SetActive(true);
                clone.transform.SetParent(root);
            }
        });
    }

    
}
