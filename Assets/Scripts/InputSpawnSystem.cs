using Unity.Entities;
using Unity.Collections;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;
using Unity.Burst;
using UnityEngine.Rendering;

public partial class InputSpawnSystem : SystemBase
{
    //This will be our query for Players
    private EntityQuery m_PlayerQuery;

    // private PlayerComponent playerComponent; //TODO May not need this
    // private EntityCommandBuffer entityCommandBuffer; //TODO May not need this
    
    //We will use the BeginSimulationEntityCommandBufferSystem for our structural changes
    private BeginSimulationEntityCommandBufferSystem m_BeginSimECB;
    private SystemHandle m_systemHandle;

    //This will save our Player prefab to be used to spawn Players
    private Entity m_Prefab;
    protected override void OnCreate()
    {
        // m_BeginSimECB.CreateCommandBuffer();
        //This is an EntityQuery for our Players, they must have an PlayerTag
        m_PlayerQuery = GetEntityQuery(ComponentType.ReadWrite<PlayerTag>());

        // entityCommandBuffer = SystemAPI.GetSingleton<BeginSimulationEntityCommandBufferSystem.Singleton>()
        //     .CreateCommandBuffer(World.Unmanaged); //TODO Maybe change some of the code with this?
        
        // playerComponent = SystemAPI.GetSingleton<PlayerComponent>(); //TODO if playerComponent
        //This will grab the BeginSimulationEntityCommandBuffer system to be used in OnUpdate
        // m_BeginSimECB = World.GetOrCreateSystem<BeginSimulationEntityCommandBufferSystem>(); //TODO maybe some system changed during updates?
        // m_BeginSimECB.EntityManager.World.GetOrCreateSystem<BeginSimulationEntityCommandBufferSystem>(); //Is this the same thing as above^?
        // m_BeginSimECB.World.GetOrCreateSystem<BeginSimulationEntityCommandBufferSystem>();
        // m_systemHandle = World.GetOrCreateSystem<BeginSimulationEntityCommandBufferSystem>(); //TODO Maybe return in SystemHandle?

        // m_BeginSimECB = SystemAPI.GetSingleton<BeginSimulationEntityCommandBufferSystem.Singleton>()
        //     .CreateCommandBuffer(); //TODO my attempts to grab something from this class.
    }

    protected override void OnUpdate()
    {
        //Here we set the prefab we will use
        if (m_Prefab == Entity.Null)
        {
            //We grab the converted PrefabCollection Entity's PlayerAuthoringComponent
            //and set m_Prefab to its Prefab value
            
            m_Prefab = GetSingleton<PlayerComponent>().Prefab;

            //we must "return" after setting this prefab because if we were to continue into the Job
            //we would run into errors because the variable was JUST set (ECS funny business)
            //comment out return and see the error
            return;
        }
        byte shoot;
        shoot = 0;
        var playerCount = m_PlayerQuery.CalculateEntityCountWithoutFiltering();

        Debug.Log("PlayerCount: " + playerCount); //Just to check how many players has spawned
        if (Input.GetKey("space"))
        {
            shoot = 1;
        }

        if (shoot == 1 && playerCount < 1)
        {
            EntityManager.Instantiate(m_Prefab);
            return;
        }
    }
}