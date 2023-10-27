using Unity.Entities;
using Unity.Mathematics;
using Unity.Physics;
using Unity.Transforms;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public readonly partial struct BulletAspect : IAspect
{

    public readonly Entity Entity;

    private readonly RefRW<BulletSpawnComponent> bullet;
    public Entity BulletPrefab => bullet.ValueRO.Prefab;

    public readonly RefRW<LocalTransform> transform;

    public float3 SpawnPositon
    {
        get => bullet.ValueRO.SpawnPosition;
        set => bullet.ValueRW.SpawnPosition = value;
    }

    
}
