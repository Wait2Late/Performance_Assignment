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

        Entity enemyEntity = SystemAPI.GetSingletonEntity<EnemySpawnComponent>();
        EnemySpawnAspect enemyAspect = SystemAPI.GetAspect<EnemySpawnAspect>(enemyEntity);

        
        float maxRadius = enemyAspect.MaxRadius;
        float randomRadius = Random.Range(0, maxRadius);
        float randomAngle = Random.Range(0, 360);

        float countDownTimer = enemyAspect.CountDownTimer;
        double elapsedTime = SystemAPI.Time.ElapsedTime;
        
        if (elapsedTime <= countDownTimer)
        {
            new ProcessEnemySpawn()
            {
                Ecb = ecb,
                RandomAngle = randomAngle,
                RandomRadius = randomRadius,
                // DeltaTime = deltaTime,
                // Seconds = seconds,
                // ElapsedTime = elapsedTime
                
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
    // public float DeltaTime;
    // public float Seconds;
    // public double ElapsedTime;
    
    [BurstCompile]
    private void Execute([ChunkIndexInQuery] int indexKey, EnemySpawnAspect enemy)
    {
        Entity enemyEntity = Ecb.Instantiate(indexKey, enemy.EnemyPrefab);
        float3 randomSpawnPositions = enemy.SpawnRandomPosition(RandomAngle, RandomRadius);
        Ecb.SetComponent(indexKey, enemyEntity, LocalTransform.FromPosition(randomSpawnPositions));
    }
}

public partial struct EnemyCollisionJob : ICollisionEventsJob
{
    public void Execute(CollisionEvent collisionEvent)
    {
        Debug.Log($"A:  {collisionEvent.EntityA}, B: {collisionEvent.EntityB}");
    }
}

public struct Touch : IComponentData { }

public readonly struct Touched : IComponentData
{
    public readonly Entity Who;
    public readonly float3 Normal;

    public Touched(Entity who, float3 normal)
    {
        Who = who;
        Normal = normal;
    }
}

public partial struct TouchSystem : ISystem
{
    public void OnCreate(ref SystemState state)
    {
        state.RequireForUpdate<SimulationSingleton>();
        state.RequireForUpdate<EndSimulationEntityCommandBufferSystem.Singleton>();
    }
    
    public void OnDestroy(ref SystemState state) {}

    public void OnUpdate(ref SystemState state)
    {
        state.Dependency = new CollisionJob()
        {
            TouchLookup = SystemAPI.GetComponentLookup<Touch>(),
            PhysicsVelocityLookUp = SystemAPI.GetComponentLookup<PhysicsVelocity>(),
            Buffer = SystemAPI.GetSingleton<EndSimulationEntityCommandBufferSystem.Singleton>()
                .CreateCommandBuffer(state.WorldUnmanaged)
            
        }.Schedule(SystemAPI.GetSingleton<SimulationSingleton>(), state.Dependency);
    }

    private struct CollisionJob : ICollisionEventsJob
    {
        public ComponentLookup<Touch> TouchLookup;
        public ComponentLookup<PhysicsVelocity> PhysicsVelocityLookUp;

        public EntityCommandBuffer Buffer;

        private bool IsDynamic(Entity entity) => PhysicsVelocityLookUp.HasComponent(entity);
        
        private bool IsTouchable(Entity entity) => TouchLookup.HasComponent(entity);
        
        public void Execute(CollisionEvent collisionEvent)
        {
            var A = collisionEvent.EntityA;
            var B = collisionEvent.EntityB;

            if (IsTouchable(A) && IsDynamic(B))
            {
                Buffer.AddComponent(A, new Touched(B, collisionEvent.Normal));
            }
            else if (IsTouchable(B) && IsDynamic(A))
            {
                Buffer.AddComponent(B, new Touched(A, collisionEvent.Normal));
            }
        }
    }
}
