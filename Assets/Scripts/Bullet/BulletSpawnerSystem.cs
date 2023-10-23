using Unity.Entities;
using Unity.Transforms;
using Unity.Burst;
using Unity.Mathematics;
using Unity.Physics;
using UnityEngine;
using Random = UnityEngine.Random;

[BurstCompile]
public partial struct BulletSpawnerSystem : ISystem
{
    public void OnCreate(ref SystemState state) { }
    
    public void OnDestroy(ref SystemState state){ }
    
    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        EntityCommandBuffer.ParallelWriter ecb = GetEntityCommandBuffer(ref state); //TODO This might need to be OnUpdate()

        foreach (var bullet in SystemAPI.Query<BulletAspect>())
        {
            if (Input.GetKey(KeyCode.Space))
            {
                // bullet.ShootBullet();
                new ProcessBulletSpawner
                {
                    Ecb = ecb
                }.ScheduleParallel();
            }
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
public partial struct ProcessBulletSpawner : IJobEntity
{
    public EntityCommandBuffer.ParallelWriter Ecb;
    private PhysicsVelocity velocity;

    private ChunkIndexInQuery _chunkIndexInQuery;

    [BurstCompile]
    private void Execute(/*[ChunkIndexInQuery] int chunkIndex,*/ BulletSpawnComponent bulletSpawn, BulletAspect bulletAspect)
    {
        // bulletAspect.ShootBullet(/*chunkIndex,*/ ref bulletSpawn, Ecb);

        // if (Input.GetKey("space"))
        // {
        //     Entity newEntity = Ecb.Instantiate(chunkIndex, bulletSpawn.Prefab);
        //     Ecb.SetComponent(chunkIndex, newEntity, LocalTransform.FromPosition(bulletSpawn.SpawnPosition));
        //     
        //     // Calculate initial velocity for the bullets
        //     float3 direction = math.normalize(LocalTransform.FromPosition(bulletSpawn.SpawnPosition).Position);
        //     velocity.Linear = direction * bulletSpawn.BulletSpeed;
        // }
    }
}
