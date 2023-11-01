using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Physics;
using UnityEngine;

[BurstCompile]
public partial struct BulletShootSystem : ISystem
{

    [BurstCompile]
    public void OnCreate(ref SystemState state)
    {
        // state.RequireForUpdate<TriggerGravityFactor>();
        // state.RequireForUpdate<SimulationSingleton>();
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

[BurstCompile]
struct TriggerGravityFactorJob : ITriggerEventsJob
{
    [ReadOnly] public ComponentLookup<TriggerGravityFactor> TriggerGravityFactorGroup;
    public ComponentLookup<PhysicsGravityFactor> PhysicsGravityFactorGroup;
    public ComponentLookup<PhysicsVelocity> PhysicsVelocityGroup;

    public void Execute(TriggerEvent triggerEvent)
    {
        Entity entityA = triggerEvent.EntityA;
        Entity entityB = triggerEvent.EntityB;

        bool isBodyATrigger = TriggerGravityFactorGroup.HasComponent(entityA);
        bool isBodyBTrigger = TriggerGravityFactorGroup.HasComponent(entityB);

        // Ignoring Triggers overlapping other Triggers
        if (isBodyATrigger && isBodyBTrigger)
            return;

        bool isBodyADynamic = PhysicsVelocityGroup.HasComponent(entityA);
        bool isBodyBDynamic = PhysicsVelocityGroup.HasComponent(entityB);

        // Ignoring overlapping static bodies
        if ((isBodyATrigger && !isBodyBDynamic) ||
            (isBodyBTrigger && !isBodyADynamic))
            return;

        var triggerEntity = isBodyATrigger ? entityA : entityB;
        var dynamicEntity = isBodyATrigger ? entityB : entityA;

        var triggerGravityComponent = TriggerGravityFactorGroup[triggerEntity];
        // tweak PhysicsGravityFactor
        {
            var component = PhysicsGravityFactorGroup[dynamicEntity];
            component.Value = triggerGravityComponent.GravityFactor;
            PhysicsGravityFactorGroup[dynamicEntity] = component;
        }
        // damp velocity
        {
            var component = PhysicsVelocityGroup[dynamicEntity];
            component.Linear *= triggerGravityComponent.DampingFactor;
            PhysicsVelocityGroup[dynamicEntity] = component;
        }
    }
}