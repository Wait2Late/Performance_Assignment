using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public partial class PlayerSpawnerSystem : SystemBase
{
    protected override void OnUpdate()
    {
        // EntityQuery playerEntityQuery = EntityManager.CreateEntityQuery(typeof(PlayerTag)); //This one as well
        // PlayerSpawnerComponent playerSpawnerComponent = SystemAPI.GetSingleton<PlayerSpawnerComponent>(); // For some reason this need to assign to something otherwise errors?
        //
        // EntityCommandBuffer entityCommandBuffer = 
        //     SystemAPI.GetSingleton<BeginSimulationEntityCommandBufferSystem.Singleton>().CreateCommandBuffer(World.Unmanaged);
        //
        //
        // int spawnAmount = 2;
        // if (playerEntityQuery.CalculateEntityCount() < spawnAmount)
        // {
        //     /* Entity spawnedEntity =*/ entityCommandBuffer.Instantiate(playerSpawnerComponent.playerPrefab);
        //     // entityCommandBuffer.SetComponent();
        //     EntityManager.Instantiate(playerSpawnerComponent.playerPrefab);
        // }

    }
}
