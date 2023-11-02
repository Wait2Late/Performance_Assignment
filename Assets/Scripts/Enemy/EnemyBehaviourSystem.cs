
// using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

// [BurstCompile]
public partial struct EnemyBehaviourSystem : ISystem
{
    // [BurstCompile]
    public void OnCreate(ref SystemState state) { }
    // [BurstCompile]
    public void OnDestroy(ref SystemState state){}

    
    // [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        float deltaTime = SystemAPI.Time.DeltaTime;
        float3 onUnitSphere = Random.onUnitSphere;
        new ProcessEnemyMovement()
        {
            DeltaTime = deltaTime,
            OnUnitSphere = onUnitSphere,
        }.ScheduleParallel();

    }

    // [BurstCompile]
    public partial struct ProcessEnemyMovement : IJobEntity
    {
        public float DeltaTime;
        public float3 OnUnitSphere;

        // [BurstCompile]
        private void Execute(EnemyAspect enemy)
        {
            enemy.MoveRandomDirections(DeltaTime, OnUnitSphere);
        }
    }
    
}
