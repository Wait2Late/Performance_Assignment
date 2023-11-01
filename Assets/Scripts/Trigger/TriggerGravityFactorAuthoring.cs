using Unity.Entities;
using UnityEngine;
using UnityEngine.Serialization;


public class TriggerGravityFactorAuthoring : MonoBehaviour
{
    public float gravityFactor = 0f;
    public float dampingFactor = 0.9f;

    class Baker : Baker<TriggerGravityFactorAuthoring>
    {
        public override void Bake(TriggerGravityFactorAuthoring authoring)
        {
            var entity = GetEntity(TransformUsageFlags.Dynamic);
            AddComponent(entity, new TriggerGravityFactor()
            {
                GravityFactor = authoring.gravityFactor,
                DampingFactor = authoring.dampingFactor,
            });
        }
    }
}

public struct TriggerGravityFactor : IComponentData
{
    public float GravityFactor;
    public float DampingFactor;
}