using Unity.Entities;
using UnityEditor.Search;

public readonly partial struct EnemyAspect : IAspect
{
    public readonly Entity Entity;

    private readonly RefRW<EnemyComponent> enemy;

    public float EnemySpeed => enemy.ValueRO.speedValue;

}
