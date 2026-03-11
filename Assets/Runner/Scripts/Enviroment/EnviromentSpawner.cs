using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Pool;

public class EnviromentSpawner : MonoBehaviour
{
    [SerializeField] private EnviromentObject[] _enviromentObjects;

    [SerializeField] private SpeedManager _speedTracker;

    [SerializeField] private Vector3[] _spawnPositions;

    [SerializeField] private float _spawnCooldown;

    [SerializeField] private bool _collectionCheck = true;

    [SerializeField] private int _defaultCapacity;
    [SerializeField] private int _maxSize;

    [SerializeField] private GameConfig _gameConfig;

    private IObjectPool<EnviromentObject> _objectPool;
    private List<EnviromentObject> _activeObjects;

    private float _spawnTimer;

    private bool _isStarted = false;
    private void Awake()
    {
        _objectPool = new ObjectPool<EnviromentObject>(CreateEnviromentObject, OnGetFromPool, OnReleaseToPool, OnDestroyPooledObject,
            _collectionCheck, _defaultCapacity, _maxSize);

        _activeObjects = new List<EnviromentObject>();

        _spawnCooldown = _gameConfig.SpawnEnviromentCooldown;
    }
    private void Update()
    {
        if (!_isStarted)
            return;

        _spawnTimer -= Time.deltaTime;
        if (_spawnTimer <= 0)
        {
            SpawnEnviromentObjects();

            _spawnTimer = _spawnCooldown * (_speedTracker.StartSpeed / _speedTracker.CurrentSpeed);
        }
    }
    private void SpawnEnviromentObjects()
    {
        foreach (var spawn in _spawnPositions)
        {
            EnviromentObject _enviromentObject = _objectPool.Get();
            if (_enviromentObject == null)
                return;
            _activeObjects.Add(_enviromentObject);
            float objectSpeed = _speedTracker.CurrentSpeed;
            _enviromentObject.PlaceObject(spawn, objectSpeed);
        }
    }
    private EnviromentObject CreateEnviromentObject()
    {
        EnviromentObject randomPrefab = _enviromentObjects[Random.Range(0, _enviromentObjects.Length)];
        EnviromentObject enviromentObject = Instantiate(randomPrefab, transform);
        enviromentObject.SetObstaclePool(_objectPool);
        return enviromentObject;
    }
    private void OnGetFromPool(EnviromentObject pooledEnviromentObject)
    {
        pooledEnviromentObject.Show();
    }
    private void OnReleaseToPool(EnviromentObject pooledEnviromentObject)
    {
        pooledEnviromentObject.Hide();
    }
    private void OnDestroyPooledObject(EnviromentObject pooledEnviromentObject)
    {
        Destroy(pooledEnviromentObject.gameObject);
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        foreach (Vector3 vector in _spawnPositions)
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

        foreach (var obstacle in _activeObjects.ToArray())
        {
            Destroy(obstacle.gameObject);
        }

        _activeObjects.Clear();

        _objectPool.Clear();
    }
}
