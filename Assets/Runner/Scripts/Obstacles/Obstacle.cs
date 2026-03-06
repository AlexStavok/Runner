using System;
using UnityEngine;
using UnityEngine.Pool;

public class Obstacle : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigidbody;

    private IObjectPool<Obstacle> _obstaclePool;

    public void SetObstaclePool(IObjectPool<Obstacle> objectPool)
    {
        _obstaclePool = objectPool;
    }
    public void PlaceObstacle(Vector3 newPos, float obstacleSpeed)
    {
        transform.position = newPos;
        _rigidbody.linearVelocity = new Vector3(0, 0, -obstacleSpeed);
    }
    public void Show()
    {
        gameObject.SetActive(true);
    }
    public void Hide()
    {
        gameObject.SetActive(false);
    }
    public void BackToPool()
    {
        _rigidbody.linearVelocity = new Vector3(0, 0, 0);

        _obstaclePool.Release(this);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent<Player>(out var player))
        {
            GameManager.Instance.EndGame();
        }
    }
}