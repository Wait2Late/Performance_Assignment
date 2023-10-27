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

    public float3 SpawnRandomPosition(float randomAngle, float randomRadius)
    {
        // float randomAngle = Random.Range(0f, 360f);
        float3 spawnDirection = Quaternion.Euler(0, randomAngle, 0) * math.forward();
        // float radius = 25.0f;
    
        float3 randomLocations = randomRadius * spawnDirection;
        SpawnPosition = randomLocations; //TODO It spawns I think in the center. It needs to be fixed
    
        return SpawnPosition;
    }
}
