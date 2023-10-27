
using Unity.Entities;
using Unity.Mathematics;

public readonly partial struct EnemySpawnAspect : IAspect
{
    public readonly Entity Entity;

    private readonly RefRW<EnemySpawnComponent> enemy;

    public Entity EnemyPrefab => enemy.ValueRO.Prefab;

    public float3 SpawnPosition
    {
        get => enemy.ValueRO.SpawnPosition;
        set => enemy.ValueRW.SpawnPosition = value;
    }

    public void SpawnRandomPosition()
    {
        
    }
}
