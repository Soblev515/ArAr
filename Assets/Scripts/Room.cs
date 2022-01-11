using Firebase.Firestore;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Room
{
    [FirestoreProperty]
    public bool isStart { private set; get; }
    [FirestoreProperty]
    public string QuestName { get; }
    [FirestoreProperty]
    public string Name { get; private set; }
    [FirestoreProperty]
    public List<string> Users { get; set; }
    [FirestoreProperty]
    public int StagesCount { get; set; }
    [FirestoreProperty]
    public int CorrectStage { get; set; }


    public SceneMoving sceneMoving;

    public Room()
    {
        this.Name = "wait";
    }
    public Room(string Name, string QuestName, string User, int StageCount, int CorrectStage)
    {
        this.Name = Name;
        this.QuestName = QuestName;
        isStart = false;
        this.StagesCount = StageCount;
        this.CorrectStage = CorrectStage;
        Info.CorrectStage = CorrectStage;
        Users = new List<string>();
        Users.Add(User);
    }

    public Room(string Name, string QuestName, List<string> Users, int StageCount, int CorrectStage)
    {
        this.Name = Name;
        this.QuestName = QuestName;
        this.StagesCount = StageCount;
        this.CorrectStage = CorrectStage;
        this.Users = Users;
        isStart = false;
    }

    public Room Clone()
    {
        var roomClone = new Room(this.Name, this.QuestName, this.Users, this.StagesCount, this.CorrectStage);
        //тут копируем в новую сущность нужные свойства
        return roomClone;
    }

    public void StageFin(int index)
    {
        this.CorrectStage += 1;
    }

    public void AddUser(string User)
        => Users.Add(User);

    public void LoadStageQuest()
    {
        isStart = true;
        StageFin(-1);
    }
}
