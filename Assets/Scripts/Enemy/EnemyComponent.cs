using Unity.Entities;
using Unity.Mathematics;

public partial struct EnemyComponent : IComponentData
{
    public Entity Prefab;
    public float3 SpawnPosition;
    
}
