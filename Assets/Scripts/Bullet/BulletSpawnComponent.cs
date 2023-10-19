using Unity.Entities;
using UnityEngine;

public struct BulletSpawnComponent : IComponentData
{
    public Entity Prefab;
}
