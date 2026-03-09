using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int ScoreRecord {  get; private set; }

    [SerializeField] private Player _player;
    [SerializeField] private PlayerDashes _playerDashes;
    [SerializeField] private SpeedManager _speedTracker;
    [SerializeField] private ObstacleSpawner _obstacleSpawner;

    [SerializeField] private GameOverUI _gameOverUI;

    public event EventHandler OnRecordChanged;

    public float CurrentScore { get; private set; }
    private bool _isPlaying = false;
    private bool _restartedAlready = false;

    private void Awake()
    {
        if(Instance == null)
            Instance = this;
    }
    private void Start()
    {
        FirebaseService.Instance.RequestPlayerData();
        FirebaseService.Instance.OnPlayerDataLoaded += FirebaseService_OnPlayerDataLoaded;
    }

    private void FirebaseService_OnPlayerDataLoaded(object sender, PlayerData e)
    {
        ScoreRecord = e.RecordScore;
        OnRecordChanged?.Invoke(this, EventArgs.Empty);
    }

    private void Update()
    {
        if(!_isPlaying)
            return;

        CurrentScore += _speedTracker.CurrentSpeed * Time.deltaTime;
    }
    public void RestartedAlreadyReset()
    {
        _restartedAlready = false;
    }
    public void StartGame(bool isRestartingScore)
    {
        if (isRestartingScore)
        {
            CurrentScore = 0;
        }

        _player.StartGame();
        _playerDashes.StartGame();
        _speedTracker.StartGame(isRestartingScore);
        _obstacleSpawner.StartGame();
        _isPlaying = true;
    }
    public void EndGame()
    {
        ResetGameObjects();

        ProcessRecordScore();

        _gameOverUI.Show((int)CurrentScore, _restartedAlready);
        _restartedAlready = true;
    }
    public void ResetGameObjects()
    {
        _isPlaying = false;
        _obstacleSpawner.EndGame();
        _speedTracker.EndGame();
        _playerDashes.EndGame();
        _player.EndGame();
    }
    private void ProcessRecordScore()
    {
        if ((int)CurrentScore > ScoreRecord)
        {
            ScoreRecord = (int)CurrentScore;
            FirebaseService.Instance.UpdatePlayerRecordScore(ScoreRecord);
            OnRecordChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
