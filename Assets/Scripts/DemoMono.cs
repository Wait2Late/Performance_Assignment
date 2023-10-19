using Unity.Burst;
using Unity.Entities;
using Unity.VisualScripting;
using UnityEngine;





public struct DamageProperties : IComponentData
{
    public int Value;
}

public struct HealthPropertise: IComponentData
{
    public int Helth;
    public int DamageResistance;
}

public struct DefenderTag : IComponentData
{

}

public readonly partial struct AttackerAspect : IAspect
{
    private readonly Entity entity;
    private readonly RefRO<DamagePropertis> DamagePropertis;

    public int GetDamage => DamagePropertis.ValueRO.Damage;
}


public readonly partial struct DefenderAspect : IAspect
{
    private readonly Entity entity;
    private readonly RefRW<HealthPropertise> HealthPropertise;
    private readonly RefRO<DefenderTag> DefenderTag;

    public int GetDamageRes => HealthPropertise.ValueRO.DamageResistance;
    

    public void Damage(int Ammount)
    {
        HealthPropertise.ValueRW.Helth -= (Ammount-GetDamageRes);
        Debug.Log("Was Attacked");
    }
}

public partial struct DamagerSystem : ISystem
{
    public void OnUpdate(ref SystemState state)
    {
        foreach(var defender in SystemAPI.Query<DefenderAspect>())
        {
            foreach (var Attacker in SystemAPI.Query<AttackerAspect>())
            {
                defender.Damage(Attacker.GetDamage);
            }
        }

        foreach (var Attacker in SystemAPI.Query<AttackerAspect>())
        {
            new DamageJob
            {
                Ammont = Attacker.GetDamage
            }.ScheduleParallel();
        }
    }
    public partial struct DamageJob : IJobEntity
    {
        public int Ammont;

        private void Execute(DefenderAspect defender)
        {
            defender.Damage(Ammont);
        }
    }

}

















public class DemoMono : MonoBehaviour
{
    public int Health = 100;
}

public class DemoBaker : Baker<DemoMono>
{
    public override void Bake(DemoMono authoring)
    {
        var entity = GetEntity(TransformUsageFlags.None);

        AddComponent(entity, new DemoProperties
        {
            Value = authoring.Health
        });
    }

}


public struct DemoProperties : IComponentData
{
    public int Value;
}

public struct DamagePropertis : IComponentData
{
    public int Damage;
}

public readonly partial struct DemoAspect : IAspect
{
    private readonly Entity entity;
    private readonly RefRW<DemoProperties> DemoProps;

    public void DoStuff()
    {
        DemoProps.ValueRW.Value += 10;
    }
    public void LogDeltaTime(float DelaTime)
    {
        Debug.Log(DelaTime);
    }

    public void LogProp()
    {
        Debug.Log(DemoProps.ValueRO.Value);
    }
}



public partial struct DemoSystem : ISystem
{
    public void OnUpdate(ref SystemState state)
    {
        var DemoEntity = SystemAPI.GetSingletonEntity<DemoProperties>();
        DemoAspect demoAspect = SystemAPI.GetAspect<DemoAspect>(DemoEntity);

        demoAspect.LogProp();

        var deltaTime = SystemAPI.Time.DeltaTime;
        demoAspect.LogDeltaTime(deltaTime);
    }
}

public partial struct DemoSystem2 : ISystem
{
    public void OnUpdate(ref SystemState state)
    {
        var DemoEntity = SystemAPI.GetSingletonEntity<DemoProperties>();
        DemoAspect demoAspect = SystemAPI.GetAspect<DemoAspect>(DemoEntity);

        demoAspect.DoStuff();
    }

}