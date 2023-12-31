﻿using Unity.Entities;
using UnityEngine;

public class BulletTagAuthoring : MonoBehaviour
{
    
}

public class BulletTagBaker : Baker<BulletTagAuthoring>
{
    public override void Bake(BulletTagAuthoring authoring)
    {
        var entity = GetEntity(TransformUsageFlags.None);
        AddComponent(entity, new BulletTag());
    }
}
