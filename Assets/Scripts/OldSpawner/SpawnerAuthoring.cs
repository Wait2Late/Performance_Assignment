using System;
using UnityEngine;
using Random = UnityEngine.Random;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

public class SpawnerAuthoring : MonoBehaviour
{
    public GameObject Prefab;
    public Transform SpawnPosition;
    public float NextSpawnTime;
    public float SpawnRate;

    private void Awake()
    {
        
    }

    private LocalTransform GetRandomLocations(float3 centerPosition)
    {
        
        float randomAngle = Random.Range(0f, 360f);
        float3 spawnDirection = Quaternion.Euler(0, randomAngle, 0) * Vector3.forward;
        float radius = 25.0f;
        
        float3 randomLocation = centerPosition + radius * spawnDirection;
        
        LocalTransform SpawnPosition = LocalTransform.FromPosition(randomLocation);
        
        return SpawnPosition;

    }
}

class SpawnerBaker : Baker<SpawnerAuthoring>
{
    public override void Bake(SpawnerAuthoring authoring)
    {
        var entity = GetEntity(TransformUsageFlags.None);
        AddComponent(entity, new Spawner
        {
            // By default, each authoring GameObject turns into an Entity.
            // Given a GameObject (or authoring component), GetEntity looks up the resulting Entity.
            Prefab = GetEntity(authoring.Prefab, TransformUsageFlags.Dynamic),
            // SpawnPosition = authoring.transform.position,
            SpawnPosition = authoring.SpawnPosition.position,
            // NextSpawnTime = 0.0f,
            NextSpawnTime = authoring.NextSpawnTime,
            SpawnRate = authoring.SpawnRate
        });
    }
}
