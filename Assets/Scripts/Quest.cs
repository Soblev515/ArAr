using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Firestore;

[FirestoreData]
public class Quest
{
    [FirestoreProperty]
    public string Name { get; set; }

    [FirestoreProperty]
    public string ShortDescription { get; set; }

    [FirestoreProperty]
    public string IMG { get; set; }

    [FirestoreProperty]
    public int StageCount { get; set; }

    [FirestoreProperty]
    public string Description { set; get; }

    [FirestoreProperty]
    public string[] StageURL { set; get; }

}