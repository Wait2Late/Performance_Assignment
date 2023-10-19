using Unity.Entities;
using UnityEngine;

public readonly partial struct BulletAspect : IAspect
{
    public readonly Entity entity;

    private readonly RefRW<VelocityComponent> velocityComponent;
    public float BulletSpeed => velocityComponent.ValueRO.bulletSpeed;
}
