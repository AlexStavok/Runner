using System;
using UnityEngine;

[CreateAssetMenu(fileName = "GameConfig", menuName = "Scriptable Objects/GameConfig")]
public class GameConfig : ScriptableObject
{
    [Header("Player")]
    public float ChangeLineSpeed;

    [Header("ObstacleSpawner")]
    public float SpawnCooldown;

    [Header("SpeedManager")]
    public float StartSpeed;
    public float SpeedIncrement;
    public float MaxSpeed;

    [Space(5)]
    public float SpeedIncrementCooldown;
}
