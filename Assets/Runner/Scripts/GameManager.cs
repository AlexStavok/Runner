using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int ScoreRecord {  get; private set; }

    [SerializeField] private Player _player;
    [SerializeField] private PlayerDashes _playerDashes;
    [SerializeField] private SpeedManager _speedTracker;
    [SerializeField] private ObstacleSpawner _obstacleSpawner;

    private float _score = 0;
    private bool _isPlaying = false;
    private void Awake()
    {
        if(Instance == null)
            Instance = this;
    }
    private void Update()
    {
        if(!_isPlaying)
            return;

        _score += _speedTracker.CurrentSpeed * Time.deltaTime;
    }
    public void StartGame(bool isRestartingScore)
    {
        if (isRestartingScore)
        {
            _score = 0;
        }

        _player.StartGame();
        _playerDashes.StartGame();
        _speedTracker.StartGame(isRestartingScore);
        _obstacleSpawner.StartGame();
        _isPlaying = true;
    }
    public void EndGame()
    {
        _isPlaying = false;
        _obstacleSpawner.EndGame();
        _speedTracker.EndGame();
        _playerDashes.EndGame();
        _player.EndGame();

        if((int)_score > ScoreRecord)
        {
            ScoreRecord = (int)_score;
            FirebaseService.Instance.UpdatePlayerRecordScore(ScoreRecord);
        }
    }
}
