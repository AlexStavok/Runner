using UnityEngine;

public class ObstacleHider : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        other.TryGetComponent<Obstacle>(out var obstacle);
        {
            obstacle.BackToPool();
        }
    }
}
