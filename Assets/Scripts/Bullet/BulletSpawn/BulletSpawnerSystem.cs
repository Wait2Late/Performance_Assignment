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
    public void OnCreate(ref SystemState state) { }

    [BurstCompile]
    public void OnDestroy(ref SystemState state){ }
    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        // state.Dependency = new CollisionJob().Schedule(SystemAPI.GetSingleton<SimulationSingleton>(), state.Dependency);
        EntityCommandBuffer.ParallelWriter ecb = GetEntityCommandBuffer(ref state); //TODO This might need to be OnUpdate()
        float deltaTime = SystemAPI.Time.DeltaTime;

        if (Input.GetKey(KeyCode.Space))
        {
            new ProcessBulletSpawner
            {
                DeltaTime = deltaTime,
                Ecb = ecb
            }.ScheduleParallel();
        }

        // foreach (var bulletSpawn in SystemAPI.Query<RefRW<BulletSpawnComponent>>()) //TODO This one does work kinda?
        // {
        //     ShootBullet(ref state, bulletSpawn);
        // }

    }
    private EntityCommandBuffer.ParallelWriter GetEntityCommandBuffer(ref SystemState state)
    {
        var ecbSingleton = SystemAPI.GetSingleton<BeginSimulationEntityCommandBufferSystem.Singleton>();
        var ecb = ecbSingleton.CreateCommandBuffer(state.WorldUnmanaged); 
        return ecb.AsParallelWriter();
    }
    
}

[BurstCompile]
public partial struct ProcessBulletSpawner : IJobEntity
{
    
    public EntityCommandBuffer.ParallelWriter Ecb;
    public float DeltaTime;
    
    [BurstCompile]
    private void Execute([ChunkIndexInQuery] int chunkIndex, BulletAspect bulletAspect)
    {
        Entity bulletEntity = Ecb.Instantiate(chunkIndex, bulletAspect.BulletPrefab);        
        Ecb.SetComponent(chunkIndex, bulletEntity, LocalTransform.FromPosition(bulletAspect.SpawnPositon));
    }
}


