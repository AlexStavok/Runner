using Firebase;
using Firebase.Auth;
using Firebase.Extensions;
using Firebase.Firestore;
using System;
using System.Collections;
using UnityEngine;
using System.Collections.Generic;

public class FirebaseService : MonoBehaviour
{
    public static FirebaseService Instance;


    // Auth
    private Coroutine _registrationCoroutine;
    private Coroutine _loginCoroutine;

    public event EventHandler OnUserRegistrationFailed;
    public event EventHandler OnUserRegistrationSucceeded;

    public event EventHandler OnUserLoginFailed;
    public event EventHandler OnUserLoginSucceeded;


    // Firestore database

    private readonly string _playerPath = "Players/";

    public event EventHandler<PlayerData> OnPlayerDataLoaded;
    public event EventHandler<List<PlayerData>> OnLeaderboardLoaded;
    public event EventHandler OnPlayerDataUpdated;
    public event EventHandler<int> OnPlayerRankLoaded;
    public event EventHandler OnFirebaseLoaded;

    async private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        DontDestroyOnLoad(this);

        var dependencyStatus = await FirebaseApp.CheckDependenciesAsync();
        if (dependencyStatus == DependencyStatus.Available)
            OnFirebaseLoaded?.Invoke(this, EventArgs.Empty);
    }
    public bool IsLogged()
    {
        return FirebaseAuth.DefaultInstance.CurrentUser != null;
    }
    public void Logout()
    {
        FirebaseAuth.DefaultInstance.SignOut();
    }
    public void RegisterUser(string email, string password)
    {
        if (email == null || password == null)
            return;

        _registrationCoroutine = StartCoroutine(RegisterUserCoroutine(email, password));
    }
    public void LoginUser(string email, string password)
    {
        if (email == null || password == null)
            return;

        _loginCoroutine = StartCoroutine(LoginUserCoroutine(email, password));
    }
    private IEnumerator RegisterUserCoroutine(string email, string password)
    {
        var auth = FirebaseAuth.DefaultInstance;
        var registerTask = auth.CreateUserWithEmailAndPasswordAsync(email, password);
        yield return new WaitUntil(() => registerTask.IsCompleted);

        if(registerTask.Exception != null)
        {
            Debug.LogWarning($"Failed to register task with  {registerTask.Exception}");
            OnUserRegistrationFailed?.Invoke(this, EventArgs.Empty);
        }
        else
        {
            Debug.Log($"Successfully registered user {registerTask.Result.User.Email}");
            OnUserRegistrationSucceeded?.Invoke(this, EventArgs.Empty);
        }

        _registrationCoroutine = null;
    }
    private IEnumerator LoginUserCoroutine(string email, string password)
    {
        var auth = FirebaseAuth.DefaultInstance;
        var loginTask = auth.SignInWithEmailAndPasswordAsync(email, password);
        yield return new WaitUntil(() => loginTask.IsCompleted);

        if (loginTask.Exception != null)
        {
            Debug.LogWarning($"Login failed with  {loginTask.Exception}");
            OnUserLoginFailed?.Invoke(this, EventArgs.Empty);
        }
        else
        {
            Debug.Log($"Login succeeded with {loginTask.Result.User.Email}");
            OnUserLoginSucceeded?.Invoke(this, EventArgs.Empty);
        }

        _loginCoroutine = null;
    }
    public void RegisterUserWithFirestore(string email, string password, string nickname)
    {
        if (email == null || password == null || nickname == null)
            return;

        _registrationCoroutine = StartCoroutine(RegisterUserWithFirestoreCoroutine(email, password, nickname));
    }
    private IEnumerator RegisterUserWithFirestoreCoroutine(string email, string password, string nickname)
    {
        var auth = FirebaseAuth.DefaultInstance;
        var registerTask = auth.CreateUserWithEmailAndPasswordAsync(email, password);
        yield return new WaitUntil(() => registerTask.IsCompleted);

        if (registerTask.Exception != null)
        {
            Debug.LogWarning($"Failed to register task with  {registerTask.Exception}");
            OnUserRegistrationFailed?.Invoke(this, EventArgs.Empty);
        }
        else
        {
            Debug.Log($"Successfully registered user {registerTask.Result.User.Email}");
            OnUserRegistrationSucceeded?.Invoke(this, EventArgs.Empty);
            SetStartPlayerData(nickname);
        }

        _registrationCoroutine = null;
    }
    private void SetStartPlayerData(string nickname)
    {
        var playerData = new PlayerData()
        {
            Nickname = nickname,
            RecordScore = 0,
        };

        var firestore = FirebaseFirestore.DefaultInstance;
        firestore.Document(_playerPath + FirebaseAuth.DefaultInstance.CurrentUser.UserId).SetAsync(playerData);
    }
    public void RequestPlayerData()
    {
        var firestore = FirebaseFirestore.DefaultInstance;

        firestore.Document(_playerPath + FirebaseAuth.DefaultInstance.CurrentUser.UserId)
            .GetSnapshotAsync().ContinueWithOnMainThread(task =>
            {
                if (task.Exception != null)
                {
                    Debug.LogError(task.Exception);
                    return;
                }

                var data = task.Result.ConvertTo<PlayerData>();

                Debug.Log("Data loaded: " + data.Nickname);

                OnPlayerDataLoaded?.Invoke(this, data);
            });
    }
    public void UpdatePlayerRecordScore(int newScore)
    {
        var firestore = FirebaseFirestore.DefaultInstance;

        firestore.Document(_playerPath + FirebaseAuth.DefaultInstance.CurrentUser.UserId)
            .GetSnapshotAsync().ContinueWithOnMainThread(task =>
            {
                if (task.Exception != null)
                {
                    Debug.LogError(task.Exception);
                    return;
                }

                PlayerData playerData = task.Result.ConvertTo<PlayerData>();

                if(newScore > playerData.RecordScore)
                {
                    firestore.Document(_playerPath + FirebaseAuth.DefaultInstance.CurrentUser.UserId)
                        .UpdateAsync("RecordScore", newScore);

                    Debug.Log("Record updated");

                    OnPlayerDataUpdated?.Invoke(this, EventArgs.Empty);
                }
            });
    }
    public void RequestLeaderboard(int maxPlayers)
    {
        var firestore = FirebaseFirestore.DefaultInstance;

        firestore.Collection("Players")
            .OrderByDescending("RecordScore")
            .Limit(maxPlayers)
            .GetSnapshotAsync()
            .ContinueWithOnMainThread(task =>
            {
                if (task.Exception != null)
                {
                    Debug.LogError(task.Exception);
                    return;
                }

                List<PlayerData> leaderboard = new List<PlayerData>();

                foreach(var document in task.Result.Documents)
                {
                    PlayerData playerData = document.ConvertTo<PlayerData>();
                    
                    leaderboard.Add(playerData);
                }

                OnLeaderboardLoaded?.Invoke(this, leaderboard);
            });
    }
    public void RequestPlayerPosition()
    {
        var firestore = FirebaseFirestore.DefaultInstance;

        firestore.Document(_playerPath + FirebaseAuth.DefaultInstance.CurrentUser.UserId)
            .GetSnapshotAsync()
            .ContinueWithOnMainThread(task =>
            {
                if (task.Exception != null)
                {
                    Debug.LogError(task.Exception);
                    return;
                }

                PlayerData playerData = task.Result.ConvertTo<PlayerData>();

                int playerScore = playerData.RecordScore;

                firestore.Collection("Players")
                .WhereGreaterThan("RecordScore", playerScore)
                .GetSnapshotAsync()
                .ContinueWithOnMainThread(rankTask =>
                {
                    if (task.Exception != null)
                    {
                        Debug.LogError(task.Exception);
                        return;
                    }

                    int playersAbove = rankTask.Result.Count;

                    int rank = playersAbove + 1;

                    OnPlayerRankLoaded?.Invoke(this, rank);
                });
            });
    }
}