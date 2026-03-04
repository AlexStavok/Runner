using Firebase.Firestore;

[FirestoreData]
public struct PlayerData
{
    [FirestoreProperty]
    public string Nickname { get; set; }

    [FirestoreProperty]
    public int RecordScore { get; set; }
}
