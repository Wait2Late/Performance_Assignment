using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

public readonly partial struct PlayerAspect : IAspect
{
    public readonly Entity entity;

    private readonly RefRW<VelocityComponent> velocityComponent;

    public float3 MoveValue
    {
        get => velocityComponent.ValueRO.moveValue;
        set => velocityComponent.ValueRW.moveValue = value;
    }

    public float3 TurnValue
    {
        get => velocityComponent.ValueRO.turnValue;
        set => velocityComponent.ValueRW.turnValue = value;
    }

    public float MoveSpeed => velocityComponent.ValueRO.moveSpeed;

    public readonly RefRW<LocalTransform> MoveTransform;

}
