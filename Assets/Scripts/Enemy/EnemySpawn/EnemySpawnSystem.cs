
using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;
using Random = UnityEngine.Random;

[BurstCompile]
public partial struct EnemySpawnSystem : ISystem
{
    [BurstCompile]
    public void OnCreate(ref SystemState state){}
    [BurstCompile]
    public void OnDestroy(ref SystemState state){}

    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        EntityCommandBuffer.ParallelWriter ecb = GetEntityCommandBuffer(ref state); //TODO This might need to be OnUpdate()
        float deltaTime = SystemAPI.Time.DeltaTime;
        float randomAngle = Random.Range(0, 360);
        float randomRadius = Random.Range(10, 25);
        if (Input.GetKey(KeyCode.Q))
        {
            new ProcessEnemySpawn()
            {
                Ecb = ecb,
                RandomAngle = randomAngle,
                RandomRadius = randomRadius,
                DeltaTime = deltaTime
            }.ScheduleParallel();
        }

    }
    private EntityCommandBuffer.ParallelWriter GetEntityCommandBuffer(ref SystemState state)
    {
        var ecbSingleton = SystemAPI.GetSingleton<BeginSimulationEntityCommandBufferSystem.Singleton>();
        var ecb = ecbSingleton.CreateCommandBuffer(state.WorldUnmanaged); 
        return ecb.AsParallelWriter();
    }
}

[BurstCompile]
public partial struct ProcessEnemySpawn : IJobEntity
{
    public EntityCommandBuffer.ParallelWriter Ecb;
    public float RandomAngle;
    public float RandomRadius;
    public float DeltaTime;
    
    [BurstCompile]
    private void Execute([ChunkIndexInQuery] int indexKey, EnemySpawnAspect enemy)
    {
        float3 randomSpawnPositions = enemy.SpawnRandomPosition(RandomAngle, RandomRadius);

        Entity enemyEntity = Ecb.Instantiate(indexKey, enemy.EnemyPrefab);
        Ecb.SetComponent(indexKey, enemyEntity, LocalTransform.FromPosition(randomSpawnPositions));
        
        
    }
}