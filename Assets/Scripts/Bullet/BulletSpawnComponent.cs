using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

public struct BulletSpawnComponent : IComponentData
{
    public Entity Prefab;
    public float3 SpawnPosition;
    public float BulletSpeed;

    public void Shoot()
    {
        Debug.Log("Shoots");
    }
}
