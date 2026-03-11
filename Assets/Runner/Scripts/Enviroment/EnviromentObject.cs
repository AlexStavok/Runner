using UnityEngine;
using UnityEngine.Pool;

public class EnviromentObject : MonoBehaviour
{
    private IObjectPool<EnviromentObject> _enviromentPool;

    [SerializeField] private Rigidbody _rigidbody;
    public void PlaceObject(Vector3 position, float speed)
    {
        gameObject.transform.position = position;
        _rigidbody.linearVelocity = new Vector3(0, 0, -speed);
    }
    public void SetObstaclePool(IObjectPool<EnviromentObject> objectPool)
    {
        _enviromentPool = objectPool;
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

        _enviromentPool.Release(this);
    }
}
