using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public class PlayerAuthoring : MonoBehaviour
{
    public GameObject Prefab;
    private Transform _transform;
    public float moveSpeed;
}

public class PlayerBaker : Baker<PlayerAuthoring>
{
    public override void Bake(PlayerAuthoring authoring)
    {
        var entity = GetEntity(TransformUsageFlags.Dynamic);
        
        AddComponent(entity, new PlayerComponent()
        {
            Prefab = GetEntity(authoring.Prefab, TransformUsageFlags.Dynamic)
        });
        
        AddComponent(entity, new PlayerTag());
        
        AddComponent(entity, new VelocityComponent()
        {
            moveSpeed = authoring.moveSpeed
        });
    }
}