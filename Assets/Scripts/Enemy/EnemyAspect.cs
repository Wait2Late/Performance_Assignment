using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEditor.Search;
using Random = UnityEngine.Random;

public readonly partial struct EnemyAspect : IAspect
{
    public readonly Entity Entity;

    private readonly RefRW<EnemyComponent> enemy;

    public readonly RefRW<LocalTransform> transform;

    public float3 MovePosition
    {
        get => transform.ValueRO.Position;
        set => transform.ValueRW.Position = value;
    }

    public float EnemySpeed => enemy.ValueRO.speedValue;

    public void MoveRandomDirections(float deltaTime, float3 onUnitSphere)
    {
        float3 randomDirection = onUnitSphere * 100;
        randomDirection.y = 0f;
        MovePosition += randomDirection * EnemySpeed * deltaTime;
    }
    
}
