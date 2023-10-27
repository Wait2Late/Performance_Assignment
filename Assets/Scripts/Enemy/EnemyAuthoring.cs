using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;


public class EnemyAuthoring : MonoBehaviour
{
    public float speedValue;
    //TODO Add more values if needed
}

public class EnemyBaker : Baker<EnemyAuthoring>
{

    public override void Bake(EnemyAuthoring authoring)
    {
        var entity = GetEntity(TransformUsageFlags.Dynamic);
        
        AddComponent(entity, new EnemyComponent()
        {
            speedValue = authoring.speedValue
        });
        
        AddComponent<EnemyTag>(entity);
    }
}