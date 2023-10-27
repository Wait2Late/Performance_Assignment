
using Unity.Burst;
using Unity.Entities;
using Unity.Transforms;
using UnityEngine;

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

        if (Input.GetKey(KeyCode.Q))
        {
            new ProcessEnemySpawn()
            {
                Ecb = ecb,
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
    public float DeltaTime;
    
    [BurstCompile]
    private void Execute([ChunkIndexInQuery] int indexKey, EnemySpawnAspect enemy)
    {
        enemy.SpawnRandomPosition();

        Entity enemyEntity = Ecb.Instantiate(indexKey, enemy.EnemyPrefab);
        Ecb.SetComponent(indexKey, enemyEntity, LocalTransform.FromPosition(enemy.SpawnPosition));
        
        
    }
}