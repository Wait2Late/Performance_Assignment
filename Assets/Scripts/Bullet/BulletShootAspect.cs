using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

public readonly partial struct BulletShootAspect : IAspect
{
    public readonly Entity Entity;

    private readonly RefRW<BulletComponent> bullet;

    public readonly RefRW<LocalTransform> transform;

    public float BulletSpeed => bullet.ValueRO.Value;
    public float3 MovePosition
    {
        get => transform.ValueRO.Position;
        set => transform.ValueRW.Position = value;
    }
    
    public void ShootProjectile(float deltaTime)
    {
        MovePosition += math.forward() * BulletSpeed * deltaTime;
    }
    
}
