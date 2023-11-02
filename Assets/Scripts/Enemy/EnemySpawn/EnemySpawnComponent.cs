using Unity.Entities;
using Unity.Mathematics;

public partial struct EnemySpawnComponent : IComponentData
{
    public Entity Prefab;
    public float3 SpawnPosition;
    public float MaxRadius;
    // public int MaxEnemiesAmount;
    public float CountDownTimer;
}
