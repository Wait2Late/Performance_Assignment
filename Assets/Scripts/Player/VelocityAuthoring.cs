using Unity.Entities;
using Unity.Mathematics;
using UnityEditor.Search;
using UnityEngine;


public class VelocityAuthoring : MonoBehaviour
{
    public float3 moveValue;
    public float3 turnValue;
}

public class VelocityBaker : Baker<VelocityAuthoring>
{
    public override void Bake(VelocityAuthoring authoring)
    {

        AddComponent(new VelocityComponent()
        {
            moveValue = authoring.moveValue,
            turnValue = authoring.turnValue
        });
    }
}
