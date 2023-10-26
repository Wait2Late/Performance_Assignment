using Unity.Entities;
using UnityEngine;
using UnityEngine.Rendering;

public class BulletAuthoring : MonoBehaviour
{
    public float speedValue;
}

public class BulletBaker : Baker<BulletAuthoring>
{
    public override void Bake(BulletAuthoring authoring)
    {
        var entity = GetEntity(TransformUsageFlags.Dynamic);
        
        AddComponent(entity, new BulletComponent()
        {
            Value = authoring.speedValue
        });
        
        AddComponent<BulletTag>(entity);
    }
}
    
    
