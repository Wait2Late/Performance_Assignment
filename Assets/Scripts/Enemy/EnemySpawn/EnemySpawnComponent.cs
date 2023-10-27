using Unity.Entities;
using Unity.Mathematics;

public partial struct EnemySpawnComponent : IComponentData
{
    public Entity Prefab;
    public float3 SpawnPosition;
    
}
