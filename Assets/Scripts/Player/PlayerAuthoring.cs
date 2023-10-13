using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public class PlayerAuthoring : MonoBehaviour
{
    public GameObject Prefab;
}

public class PlayerBaker : Baker<PlayerAuthoring>
{
    public override void Bake(PlayerAuthoring authoring)
    {
        AddComponent(new PlayerComponent()
        {
            Prefab = GetEntity(authoring.Prefab)
        });
    }
}