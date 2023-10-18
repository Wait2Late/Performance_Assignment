using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public class GameSettingsAuthoring : MonoBehaviour
{
    // public float asteroidVelocity;
    public float playerForce;
    public float bulletVelocity;
    // public int numAsteroids;
    // public int levelWidth;
    // public int levelHeight;
    // public int levelDepth;
}

public class GameSettingsBaker : Baker<GameSettingsAuthoring>
{
    public override void Bake(GameSettingsAuthoring authoring)
    {
        var entity = GetEntity(TransformUsageFlags.Dynamic);
        AddComponent(entity, new GameSettingsComponent()
        {
            playerForce = authoring.playerForce,
            bulletVelocity = authoring.bulletVelocity,
        });
    }
}