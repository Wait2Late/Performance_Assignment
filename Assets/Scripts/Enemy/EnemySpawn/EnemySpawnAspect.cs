using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;
using Random = UnityEngine.Random;

public readonly partial struct EnemySpawnAspect : IAspect
{
    public readonly Entity Entity;

    private readonly RefRW<EnemySpawnComponent> enemy;

    public Entity EnemyPrefab => enemy.ValueRO.Prefab;

    public float3 SpawnPosition
    {
        get => enemy.ValueRO.SpawnPosition;
        set => enemy.ValueRW.SpawnPosition = value;
    }

    public float MaxRadius => enemy.ValueRO.MaxRadius;
    
    public float CountDownTimer => enemy.ValueRO.CountDownTimer;

    public int MaxEnemiesAmount => enemy.ValueRO.MaxEnemiesAmount;


    public float3 SpawnRandomPosition(float randomAngle, float randomRadius)
    {
        float3 spawnDirection = Quaternion.Euler(0, randomAngle, 0) * math.forward();
        float3 randomLocations = SpawnPosition + randomRadius * spawnDirection;
        return randomLocations;
    }

    public float3 SpawnOnThisPosition()
    {
        return SpawnPosition;
    }
}
