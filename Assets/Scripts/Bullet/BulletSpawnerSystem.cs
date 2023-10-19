using Unity.Entities;
using Unity.Transforms;
using Unity.Burst;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

[BurstCompile]
public partial struct BulletSpawnerSystem : ISystem
{
    public void OnCreate(ref SystemState state){}
    
    public void OnDestroy(ref SystemState state){}
    
    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        EntityCommandBuffer.ParallelWriter ecb = GetEntityCommandBuffer(ref state);

        
        
        if (Input.GetKey("Space"))
        {
            
        }
        
        
        
    }
    private EntityCommandBuffer.ParallelWriter GetEntityCommandBuffer(ref SystemState state)
    {
        var ecbSingleton = SystemAPI.GetSingleton<BeginSimulationEntityCommandBufferSystem.Singleton>();
        var ecb = ecbSingleton.CreateCommandBuffer(state.WorldUnmanaged); 
        return ecb.AsParallelWriter();
    }
    
    public partial struct ProcessBulletSpawner : IJobEntity
    {
        public EntityCommandBuffer.ParallelWriter Ecb;

        private void Execute([ChunkIndexInQuery] int chunkIndex, ref BulletSpawnComponent BulletSpawn)
        {

            Entity newEntity = Ecb.Instantiate(chunkIndex, BulletSpawn.Prefab);
            
            // Ecb.SetComponent(chunkIndex, newEntity, );
        }
    }
    
}
