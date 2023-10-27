using Unity.Entities;
using UnityEngine;

public class EnemySpawnAuthoring : MonoBehaviour
{
    public GameObject Prefab;
    public Transform spawnTransform;
}

public class EnemySpawnBaker : Baker<EnemySpawnAuthoring>
{
    public override void Bake(EnemySpawnAuthoring authoring)
    {
        var entity = GetEntity(TransformUsageFlags.Dynamic);
        
        AddComponent(entity, new EnemySpawnComponent()
        {
            Prefab = GetEntity(authoring.Prefab, TransformUsageFlags.Dynamic),
            SpawnPosition = authoring.spawnTransform.position
        });
    }
}