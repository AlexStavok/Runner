using System;
using UnityEngine;

public abstract class Obstacle : MonoBehaviour
{
    public void PlaceObstacle(Vector3 newPos)
    {
        transform.position = newPos;
    }
    public void Show()
    {
        gameObject.SetActive(true);
    }
    public void Hide()
    {
        gameObject.SetActive(false);
    }
}