using UnityEngine;

public class SpeedManager : MonoBehaviour
{
    public float StartSpeed;
    public float CurrentSpeed { get; private set; }

    [SerializeField] private float _maxSpeed;

    [SerializeField] private float _speedIncrement;

    [SerializeField] private float _speedIncrementCooldown;

    [SerializeField] private GameConfig _gameConfig;

    private float _timer;

    private bool isStarted = false;
    private void Awake()
    {
        StartSpeed = _gameConfig.StartSpeed;
        _speedIncrement = _gameConfig.SpeedIncrement;
        _maxSpeed = _gameConfig.MaxSpeed;
        _speedIncrementCooldown = _gameConfig.SpeedIncrementCooldown;
    }
    private void Update()
    {
        if (!isStarted)
            return;

        if (CurrentSpeed >= _maxSpeed)
            return;

        _timer -= Time.deltaTime;
        if (_timer <= 0)
        {
            if (CurrentSpeed < _maxSpeed)
                CurrentSpeed += _speedIncrement;

            _timer = _speedIncrementCooldown;
        }
    }
    public void StartGame(bool isRestartingSpeed)
    {
        if (isRestartingSpeed)
            CurrentSpeed = StartSpeed;

        _timer = _speedIncrementCooldown;
        
        isStarted = true;
    }
    public void EndGame()
    {
        isStarted = false;
    }
}
