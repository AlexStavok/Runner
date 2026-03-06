using UnityEngine;

public class ObstacleFactory : MonoBehaviour
{
    [SerializeField] private Obstacle _obstacleJump;
    [SerializeField] private Obstacle _obstacleDash;
    [SerializeField] private Obstacle _obstacleSlide;
    public Obstacle CreateObstacle(EObstacleType obstacleType)
    {
        switch (obstacleType)
        {
            case EObstacleType.Jump:
                Obstacle jumpObstacle = Instantiate(_obstacleJump);
                return jumpObstacle;

            case EObstacleType.Dash:
                Obstacle dashObstacle = Instantiate(_obstacleDash);
                return dashObstacle;

            case EObstacleType.Slide:
                Obstacle slideObstacle = Instantiate(_obstacleSlide);
                return slideObstacle;
        }
        return null;
    }
}

public enum EObstacleType
{
    Jump = 0,
    Dash = 1,
    Slide = 2,
    Count = 3,
}
