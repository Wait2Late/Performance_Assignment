using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Physics;
using Unity.Physics.Systems;
using UnityEngine;

[BurstCompile]
public partial struct BulletShootSystem : ISystem
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
        float deltaTime = SystemAPI.Time.DeltaTime;
        // var ecbSingleton = SystemAPI.GetSingleton<EndSimulationEntityCommandBufferSystem.Singleton>();
        
        new ProcessProjectile()
        {
            DeltaTime = deltaTime,
        }.ScheduleParallel();
        


    }
}
[BurstCompile]
public partial struct ProcessProjectile : IJobEntity
{
    public float DeltaTime;

    [BurstCompile]
    private void Execute(BulletShootAspect bullet)
    {
        bullet.ShootProjectile(DeltaTime);
        
    }
}
