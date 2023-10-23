using Unity.Entities;
using Unity.Mathematics;
using Unity.Physics;
using Unity.Transforms;
using UnityEngine;

public readonly partial struct BulletAspect : IAspect
{
    public readonly Entity entity;

    private readonly RefRW<BulletSpawnComponent> bullet;
    public float BulletSpeed => bullet.ValueRO.BulletSpeed;

    // public void ShootBullet(/*[ChunkIndexInQuery] int ChunkIndex,*/ ref BulletSpawnComponent BulletSpawn, EntityCommandBuffer.ParallelWriter ecb)
    // {
    //     PhysicsVelocity velocity;
    //     Entity newEntity = ecb.Instantiate(ChunkIndex, BulletSpawn.Prefab);
    //     ecb.SetComponent(ChunkIndex, newEntity, LocalTransform.FromPosition(BulletSpawn.SpawnPosition));
    //             
    //     // Calculate initial velocity for the bullets
    //     float3 direction = math.normalize(LocalTransform.FromPosition(BulletSpawn.SpawnPosition).Position);
    //     velocity.Linear = direction * BulletSpawn.BulletSpeed;
    // }
}
