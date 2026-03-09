using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class ObstacleSpawner : MonoBehaviour
{
    [SerializeField] private ObstacleFactory _obstacleFactory;
    [SerializeField] private SpeedManager _speedTracker;

    [SerializeField] private Vector3[] _spawnPositions;

    [SerializeField] private float _spawnCooldown;
    
    [SerializeField] private bool _collectionCheck = true;

    [SerializeField] private int _defaultCapacity;
    [SerializeField] private int _maxSize;

    [SerializeField] private GameConfig _gameConfig;

    private IObjectPool<Obstacle> _objectPool;
    private List<Obstacle> _activeObstacles;

    private float _spawnTimer;

    private bool _isStarted = false;
    private void Awake()
    {
        _objectPool = new ObjectPool<Obstacle>(CreateObstacle, OnGetFromPool, OnReleaseToPool, OnDestroyPooledObject,
            _collectionCheck, _defaultCapacity, _maxSize);

        _activeObstacles = new List<Obstacle>();

        _spawnCooldown = _gameConfig.SpawnCooldown;
    }
    private void Update()
    {
        if (!_isStarted)
            return;

        _spawnTimer -= Time.deltaTime;
        if (_spawnTimer <= 0)
        {
            SpawnObstacle();

            _spawnTimer = _spawnCooldown * (_speedTracker.StartSpeed / _speedTracker.CurrentSpeed);
        }
    }
    private void SpawnObstacle()
    {
        Obstacle obstacle = _objectPool.Get();

        if (obstacle == null)
            return;

        _activeObstacles.Add(obstacle);

        Vector3 spawnPos = _spawnPositions[Random.Range(0, _spawnPositions.Length)];
        float obstacleSpeed = _speedTracker.CurrentSpeed;
        obstacle.PlaceObstacle(spawnPos, obstacleSpeed);
    }
    private Obstacle CreateObstacle()
    {
        EObstacleType randomType = (EObstacleType)Random.Range(0, (int)EObstacleType.Count);
        Obstacle obstacle = _obstacleFactory.CreateObstacle(randomType, transform);
        obstacle.SetObstaclePool(_objectPool);
        return obstacle;
    }
    private void OnGetFromPool(Obstacle pooledObstacle)
    {
        pooledObstacle.Show();
    }
    private void OnReleaseToPool(Obstacle pooledObstacle)
    {
        pooledObstacle.Hide();
    }
    private void OnDestroyPooledObject(Obstacle pooledObstacle)
    {
        Destroy(pooledObstacle.gameObject);
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        foreach(Vector3 vector in _spawnPositions)
        {
            Gizmos.DrawSphere(vector, 0.5f);
        }
    }
    public void StartGame()
    {
        _spawnTimer = _spawnCooldown;

        _isStarted = true;
    }
    public void EndGame()
    {
        _isStarted = false;

        foreach(var obstacle in _activeObstacles.ToArray())
        {
            Destroy(obstacle.gameObject);
        }

        _activeObstacles.Clear();

        _objectPool.Clear();
    }
}
