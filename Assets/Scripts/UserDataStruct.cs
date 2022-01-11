using Firebase.Firestore;

[FirestoreData]
public struct UserDataStruct
{
    [FirestoreProperty]
    public string Name { get; set; }

    [FirestoreProperty]
    public string IMG { get; set; }

    [FirestoreProperty]
    private string compitedQuest { get; set; }
}
