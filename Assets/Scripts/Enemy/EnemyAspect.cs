
using Unity.Entities;
using Unity.Mathematics;

public struct EnemyAspect
{
    public readonly Entity Entity;

    private readonly RefRW<EnemyComponent> enemy;

    public Entity EnemyPrefab => enemy.ValueRO.Prefab;

    public float3 SpawnPosition
    {
        get => enemy.ValueRO.SpawnPosition;
        set => enemy.ValueRW.SpawnPosition = value;
    }
    
    
}
