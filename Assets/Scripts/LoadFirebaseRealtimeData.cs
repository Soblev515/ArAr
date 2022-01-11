using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Database;
using Newtonsoft.Json;
using System;
using UnityEngine.UI;
using Random = System.Random;
using Firebase.Extensions;

public class LoadFirebaseRealtimeData : MonoBehaviour
{
    public DatabaseReference reference;
    public Room Room;
    private string Error = "OK";
    public FirebaseManagerMain ManagerMain;
    private bool isJoin;
    public SceneMoving sceneMoving;
    public AssetBudleScene AssetBudleScene;

    public void Awake()
    {
        reference = FirebaseDatabase.DefaultInstance.RootReference.Child("rooms");
    }

    public Room GetRoom()
    {
        return Room;
    }

    public void JoinRoom(string keyRoom, string uid)
    {
        isJoin = true;
        GetDataRoom(keyRoom);
    }

    public void StartLevel(int i)
    {
        AssetBudleScene.LoadLevel(i);
    }

    public bool isHaveRoom(string keyRoom)
    {
        reference.Child(keyRoom).ValueChanged += FingRoom;
        if (Error == "OK")
            return true;
        else
            return false;
    }

    public void AddUser(string keyRoom, string uid)
    {
        Debug.Log("=+" + keyRoom);
        Debug.Log("=+" + uid);
        Room.AddUser(uid);
        reference.Child(keyRoom).Child("Users").SetValueAsync(Room.Users);
    }

    private void GetDataRoom(string keyRoom)
    {
        //StartCoroutine(GetRoom(keyRoom));
        reference.Child(keyRoom).ValueChanged += Lister;
        reference.Child(keyRoom).Child("Name").SetValueAsync(keyRoom);
        reference.Child(keyRoom).ValueChanged -= Lister;
    }
    private void FingRoom(object sender, ValueChangedEventArgs e)
    {
        if (e.DatabaseError != null)
        {
            Error = e.DatabaseError.Message;
        }
        else
            Error = "OK";
    }

    private void Lister(object sender, ValueChangedEventArgs e)
    {
        Debug.Log("I start Lister");
        if (e.DatabaseError != null)
        {
            Debug.LogError(e.DatabaseError.Message);
            return;
        }

        var Name = e.Snapshot.Child("Name").GetValue(true).ToString();
        var QuestName = e.Snapshot.Child("QuestName").GetValue(true).ToString();
        var StageCount = (int)e.Snapshot.Child("StageCount").GetValue(true);
        var CorrectStage = (int)e.Snapshot.Child("CorrectStage").GetValue(true);
        var Users = GetUsersArray(e.Snapshot.Child("Users"));

        Debug.Log("=" + Name);
        Debug.Log("="+ QuestName);
        Debug.Log("=" + StageCount);
        Debug.Log("=" + Users[0]);

        Room = new Room(Name, QuestName, Users, StageCount, CorrectStage);
        if (!Users.Contains(Info.Uid) && isJoin)
        {
            Room.AddUser(Info.Uid);
            reference.Child(Name).Child("Users").SetValueAsync(Room.Users);
        }
        ManagerMain.SetRoom();
    }

    internal void ExitRoom()
    {
        Room.Users.Remove(Info.Uid);
        reference.Child(Room.Name).ValueChanged -= Lister;
        isJoin = false;
        reference.Child(Room.Name).Child("Users").SetValueAsync(Room.Users);
    }

    public void CreateRoom(Quest quest)
    {
        var random = new Random();
        var name = GenerateNameRoom(random);
        Debug.Log(name.ToUpper());
        Room = new Room(name, quest.Name, Info.Uid, quest.StageCount, -1);
        reference.Child(name).Child("Name").SetValueAsync(Room.Name);
        reference.Child(name).Child("QuestName").SetValueAsync(Room.QuestName);
        reference.Child(name).Child("StagesCount").SetValueAsync(Room.StagesCount);
        reference.Child(name).Child("CorrectStage").SetValueAsync(-1);
        reference.Child(name).Child("Users").SetValueAsync(Room.Users);

        reference.Child(name).Child("CorrectStage").ChildChanged += ChangedStages;
        reference.Child(name).Child("Users").ChildChanged += ChangedUsers;
        Debug.Log("room create");
        ManagerMain.SetRoom();
    }

    private void ChangedUsers(object sender, ChildChangedEventArgs e)
    {
        Room.Users = GetUsersArray(e.Snapshot);

    }

    private void ChangedStages(object sender, ChildChangedEventArgs e)
    {
        var CorrectStage = (int)e.Snapshot.Child("CorrectStage").GetValue(true);
        if (Room.CorrectStage < CorrectStage)
        {
            Room.StageFin(Room.CorrectStage);
            AssetBudleScene.LoadLevel(Room.CorrectStage);
        }
    }

    public void ListenStages()
        => reference.Child(Room.Name).ValueChanged += ListerStages;

    

    private void ListerStages(object sender, ValueChangedEventArgs e)
    {
        var CorrectStage = (int)e.Snapshot.Child("CorrectStage").GetValue(true);
        if (Room.CorrectStage < CorrectStage)
        {
            Room.StageFin(Room.CorrectStage);
            AssetBudleScene.LoadLevel(Room.CorrectStage);
        }
    }
    public void StageFin(int index)
    {
        Room.StageFin(index);
        reference.Child(Room.Name).Child("CorrectStage").SetValueAsync(Room.CorrectStage);
    }

    public void SetAnswer(string link)
    {
        Debug.Log("link:" + link);
        reference.Child(Room.Name).Child("Answers").Child("0").SetValueAsync(link);
    }

    private List<string> GetUsersArray(DataSnapshot snapshot)
    {
        List<string> result = new List<string>();
        foreach (var child in snapshot.Children)
            result.Add(child.GetValue(true).ToString());
        return result;
    }

    private List<bool> GetStageArray(DataSnapshot snapshot)
    {
        List<bool> result = new List<bool>();
        foreach (var child in snapshot.Children)
        {
            result.Add((bool)child.GetValue(true));
            Debug.Log((bool)child.GetValue(true));
        }
        return result;
    }
    private static string GenerateNameRoom(Random random)
    {
        var name = "";
        for (int i = 0; i < 5; i++)
            name += (char)random.Next(65, 72);
        return name;
    }
}
