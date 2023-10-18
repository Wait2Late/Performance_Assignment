using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

public struct VelocityComponent : IComponentData
{
    public float3 moveValue;
    public float3 turnValue;
    public float moveSpeed;
}
