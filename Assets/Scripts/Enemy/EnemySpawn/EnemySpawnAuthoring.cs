using Unity.Entities;
using UnityEngine;

public class EnemySpawnAuthoring : MonoBehaviour
{
    public GameObject Prefab;
    public Transform spawnTransform;
    public float maxRadius;
    // public int maxEnemiesAmount;
    public float countDownTimer;
}

public class EnemySpawnBaker : Baker<EnemySpawnAuthoring>
{
    public override void Bake(EnemySpawnAuthoring authoring)
    {
        var entity = GetEntity(TransformUsageFlags.Dynamic);
        
        AddComponent(entity, new EnemySpawnComponent()
        {
            Prefab = GetEntity(authoring.Prefab, TransformUsageFlags.Dynamic),
            SpawnPosition = authoring.spawnTransform.position,
            MaxRadius = authoring.maxRadius,
            // MaxEnemiesAmount = authoring.maxEnemiesAmount,
            CountDownTimer = authoring.countDownTimer
        });
    }
}