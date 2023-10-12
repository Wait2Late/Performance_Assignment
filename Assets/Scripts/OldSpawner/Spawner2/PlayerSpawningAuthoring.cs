using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public class PlayerSpawningAuthoring : MonoBehaviour
{
    public GameObject playerPrefab;
}

public class PlayerSpawnerBaker : Baker<PlayerSpawningAuthoring>
{
    public override void Bake(PlayerSpawningAuthoring authoring)
    {
        AddComponent(new PlayerSpawnerComponent() 
        {
            playerPrefab = GetEntity(authoring.playerPrefab)
        });
    }
}