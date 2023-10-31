using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Physics;
using Unity.Transforms;
using UnityEngine;
using Random = UnityEngine.Random;


[BurstCompile]
public partial struct EnemySpawnSystem : ISystem
{
    [BurstCompile]
    public void OnCreate(ref SystemState state)
    {
        state.RequireForUpdate<SimulationSingleton>();
    }
    [BurstCompile]
    public void OnDestroy(ref SystemState state){}

    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        EntityCommandBuffer.ParallelWriter ecb = GetEntityCommandBuffer(ref state); //TODO This might need to be OnUpdate()
        float deltaTime = SystemAPI.Time.DeltaTime;

        state.Dependency = new EnemyCollisionJob().Schedule(SystemAPI.GetSingleton<SimulationSingleton>()
            ,state.Dependency);

        float seconds = 2.0f;

        Entity enemyEntity = SystemAPI.GetSingletonEntity<EnemySpawnComponent>();
        EnemySpawnAspect enemyAspect = SystemAPI.GetAspect<EnemySpawnAspect>(enemyEntity);

        
        float maxRadius = enemyAspect.MaxRadius;
        float randomRadius = Random.Range(0, maxRadius);
        float randomAngle = Random.Range(0, 360);

        float countDownTimer = enemyAspect.CountDownTimer;
        var elapsedTime = SystemAPI.Time.ElapsedTime;
        
        Debug.Log("ElapsedTime: " + elapsedTime);
        if (elapsedTime <= countDownTimer)
        {
            new ProcessEnemySpawn()
            {
                Ecb = ecb,
                RandomAngle = randomAngle,
                RandomRadius = randomRadius,
                DeltaTime = deltaTime,
                Seconds = seconds,
                ElapsedTime = elapsedTime
                
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
    public float Seconds;
    public double ElapsedTime;
    // public int EnemyCount;
    // public EntityQuery EnemyQuery;
    
    [BurstCompile]
    private void Execute([ChunkIndexInQuery] int indexKey, EnemySpawnAspect enemy)
    {
        // Seconds -= DeltaTime;
        // Debug.Log("Seconds: " + Seconds);
        // if (0 <= Seconds)
        // {
            
        // if (EnemyCount < enemy.MaxEnemiesAmount)
        // {

            
            Entity enemyEntity = Ecb.Instantiate(indexKey, enemy.EnemyPrefab);
            float3 randomSpawnPositions = enemy.SpawnRandomPosition(RandomAngle, RandomRadius);
            Ecb.SetComponent(indexKey, enemyEntity, LocalTransform.FromPosition(randomSpawnPositions));
            
            // enemy.CountDownTimer = ElapsedTime
        // }
        // }
        
        
    }
}

public partial struct EnemyCollisionJob : ICollisionEventsJob
{
    public void Execute(CollisionEvent collisionEvent)
    {
        Debug.Log($"A:  {collisionEvent.EntityA}, B: {collisionEvent.EntityB}");
    }
}

