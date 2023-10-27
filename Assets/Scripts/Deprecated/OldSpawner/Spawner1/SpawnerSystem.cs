// using Unity.Entities;
// using Unity.Transforms;
// using Unity.Burst;
// using UnityEngine;
//
// [BurstCompile]
// public partial struct SpawnerSystem : ISystem
// {
//     public void OnCreate(ref SystemState state) { }
//     public void OnDestroy(ref SystemState state) { }
//
//     [BurstCompile]
//     public void OnUpdate(ref SystemState state)
//     {
//         // Queries for all Spawner components. Uses RefRW because this system wants
//         // to read from and write to the component. If the system only needed read-only
//         // access, it would use RefRO instead.
//         foreach (RefRW<Spawner> spawner in SystemAPI.Query<RefRW<Spawner>>())
//         {
//             ProcessSpawner(ref state, spawner);
//         }
//     }
//
//     private void ProcessSpawner(ref SystemState state, RefRW<Spawner> spawner)
//     {
//         // If the next spawn time has passed.
//         if (spawner.ValueRO.NextSpawnTime < SystemAPI.Time.ElapsedTime)
//         {
//             // Spawns a new entity and positions it at the spawner.
//             Entity newEntity = state.EntityManager.Instantiate(spawner.ValueRO.Prefab);
//             // LocalPosition.FromPosition returns a Transform initialized with the given position.
//             state.EntityManager.SetComponentData(newEntity, 
//                 LocalTransform.FromPosition(spawner.ValueRO.SpawnPosition));
//
//             // Resets the next spawn time.
//             spawner.ValueRW.NextSpawnTime = (float)SystemAPI.Time.ElapsedTime + spawner.ValueRO.SpawnRate;
//         }
//     }
// }




using System;
using System.Security.Cryptography;
using Unity.Collections;
using Unity.Entities;
using Unity.Transforms;
using Unity.Burst;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

[BurstCompile]
public partial struct OptimizedSpawnerSystem : ISystem
{
    public void OnCreate(ref SystemState state) { }

    public void OnDestroy(ref SystemState state) { }

    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        EntityCommandBuffer.ParallelWriter ecb = GetEntityCommandBuffer(ref state);

        // Creates a new instance of the job, assigns the necessary data, and schedules the job in parallel.
        new ProcessSpawnerJob
        {
            ElapsedTime = SystemAPI.Time.ElapsedTime,
            Ecb = ecb
        }.ScheduleParallel();
    }

    private EntityCommandBuffer.ParallelWriter GetEntityCommandBuffer(ref SystemState state)
    {
        var ecbSingleton = SystemAPI.GetSingleton<BeginSimulationEntityCommandBufferSystem.Singleton>();
        var ecb = ecbSingleton.CreateCommandBuffer(state.WorldUnmanaged); 
        return ecb.AsParallelWriter();
    }
}

[BurstCompile]
public partial struct ProcessSpawnerJob : IJobEntity
{
    public EntityCommandBuffer.ParallelWriter Ecb;
    public double ElapsedTime;

    // IJobEntity generates a component data query based on the parameters of its `Execute` method.
    // This example queries for all Spawner components and uses `ref` to specify that the operation
    // requires read and write access. Unity processes `Execute` for each entity that matches the
    // component data query.
    private void Execute([ChunkIndexInQuery] int chunkIndex, ref Spawner spawner)
    {
        // Debug.Log("ElapsedTime: " + ElapsedTime);
        // If the next spawn time has passed.
        if (spawner.NextSpawnTime < ElapsedTime)
        {
            // Spawns a new entity and positions it at the spawner.
            Entity newEntity = Ecb.Instantiate(chunkIndex, spawner.Prefab);

            // LocalTransform centerPosition = GetRandomLocations(spawner.SpawnPosition);
            // Ecb.SetComponent(chunkIndex, newEntity, LocalTransform.FromPosition(spawner.SpawnPosition));
            Ecb.SetComponent(chunkIndex, newEntity, LocalTransform.FromPosition(spawner.SpawnPosition));
            
            
            // Resets the next spawn time.
            spawner.NextSpawnTime = (float)ElapsedTime + spawner.SpawnRate;
            
        }
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