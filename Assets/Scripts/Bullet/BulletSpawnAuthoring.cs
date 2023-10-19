using System.Collections;
using System.Collections.Generic;using Unity.Entities;
using UnityEngine;

public class BulletSpawnAuthoring : MonoBehaviour
{
    public GameObject Prefab;
    public Transform spawnPosition;
    public float bulletSpeed;
}

public class BulletSpawnBaker : Baker<BulletSpawnAuthoring>
{
    public override void Bake(BulletSpawnAuthoring authoring)
    {
        var entity = GetEntity(TransformUsageFlags.Dynamic);
        
        AddComponent(entity, new BulletSpawnComponent()
        {
            Prefab = GetEntity(authoring.Prefab, TransformUsageFlags.Dynamic)
            
        });

        AddComponent(entity, new VelocityComponent()
        {
            bulletSpeed = authoring.bulletSpeed
        });
    }
}
