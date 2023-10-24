using Unity.Entities;
using Unity.Transforms;
using Unity.Burst;
using Unity.Mathematics;
using Unity.Physics;
using UnityEngine;
using Random = UnityEngine.Random;

[BurstCompile]
[UpdateInGroup(typeof(InitializationSystemGroup))]
public partial struct BulletSpawnerSystem : ISystem
{
    [BurstCompile]

    public void OnCreate(ref SystemState state)
    {
        
    }

    [BurstCompile]
    
    public void OnDestroy(ref SystemState state){ }
    
    public void OnUpdate(ref SystemState state)
    {
        // EntityCommandBuffer.ParallelWriter ecb = GetEntityCommandBuffer(ref state); //TODO This might need to be OnUpdate()

        // foreach (var bullet in SystemAPI.Query<BulletAspect>())
        // {
        //     if (Input.GetKey(KeyCode.Space))
        //     {
        //         // bullet.ShootBullet();
        //         new ProcessBulletSpawner
        //         {
        //             Ecb = ecb
        //         }.ScheduleParallel();
        //
        //         state.Enabled = false;
        //     }
        // }
        // foreach (var bulletAspect in SystemAPI.Query<BulletAspect>().WithAll<BulletTag>())
        // {
        //     ShootBulletAspect(ref state, bulletAspect);
        // }

        foreach (var bulletSpawn in SystemAPI.Query<RefRW<BulletSpawnComponent>>())
        {
            ShootBullet(ref state, bulletSpawn);
        }
        
        
    }
    
    private void ShootBullet(ref SystemState state, RefRW<BulletSpawnComponent> bulletSpawn)
    {
        
        if (Input.GetKey(KeyCode.Space))
        {
            Entity newEntity = state.EntityManager.Instantiate(bulletSpawn.ValueRO.Prefab);

            // quaternion rotation = state.EntityManager.GetComponentData<LocalToWorld>(newEntity).Rotation;
            //
            // float3 forwardDirection = math.forward(rotation);
            // float3 spawnPosition = bulletSpawn.ValueRO.SpawnPosition;
            // float3 initialVelocity = forwardDirection * bulletSpawn.ValueRO.BulletSpeed;
            //
            //
            // state.EntityManager.SetComponentData(newEntity, new LocalToWorld
            // {
            //     Value = new float4x4(
            //         rotation, new float3
            //         (
            //         spawnPosition.x, 
            //         spawnPosition.y, 
            //         spawnPosition.z)
            //         )
            // });
            //
            // state.EntityManager.AddComponentData(newEntity, new PhysicsVelocity { Linear = initialVelocity });
            
            state.EntityManager.SetComponentData(
                newEntity, LocalTransform.FromPosition(bulletSpawn.ValueRO.SpawnPosition)); //TODO Keep this as it spawns on the original position
            
            // state.EntityManager.DestroyEntity(newEntity);
            
        }
    }
    
    // private void ShootBulletAspect(ref SystemState state, BulletAspect bulletAspect)
    // {
    //     if (Input.GetKey(KeyCode.Space))
    //     {
    //         PhysicsVelocity velocity;
    //         Entity newEntity = state.EntityManager.Instantiate(bulletAspect.entity);
    //         
    //         state.EntityManager.SetComponentData(newEntity, 
    //             LocalTransform.FromPosition(bulletAspect.SpawnPositon));
    //         
    //         float3 direction = math.normalize(LocalTransform.FromPosition(bulletAspect.SpawnPositon).Position);
    //         velocity.Linear = direction * bulletAspect.BulletSpeed;
    //     }
    // }
    
    // private EntityCommandBuffer.ParallelWriter GetEntityCommandBuffer(ref SystemState state)
    // {
    //     var ecbSingleton = SystemAPI.GetSingleton<BeginSimulationEntityCommandBufferSystem.Singleton>();
    //     var ecb = ecbSingleton.CreateCommandBuffer(state.WorldUnmanaged); 
    //     return ecb.AsParallelWriter();
    // }
    
    
    
}


// [BurstCompile]
// public partial struct ProcessBulletSpawner : IJobEntity
// {
//     public EntityCommandBuffer.ParallelWriter Ecb;
//     private PhysicsVelocity velocity;
//
//     [BurstCompile]
//     private void Execute([ChunkIndexInQuery] int chunkIndex, BulletSpawnComponent bulletSpawn, BulletAspect bulletAspect)
//     {
//         // bulletAspect.ShootBullet(/*chunkIndex,*/ ref bulletSpawn, Ecb);
//
//         // if (Input.GetKey("space"))
//         // {
//         //     Entity newEntity = Ecb.Instantiate(chunkIndex, bulletSpawn.Prefab);
//         //     Ecb.SetComponent(chunkIndex, newEntity, LocalTransform.FromPosition(bulletSpawn.SpawnPosition));
//         //     
//         //     // Calculate initial velocity for the bullets
//         //     float3 direction = math.normalize(LocalTransform.FromPosition(bulletSpawn.SpawnPosition).Position);
//         //     velocity.Linear = direction * bulletSpawn.BulletSpeed;
//         // }
//     }
// }
