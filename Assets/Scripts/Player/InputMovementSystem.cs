using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using Unity.Collections;
using Unity.Jobs;
using Unity.Jobs.LowLevel.Unsafe;
using UnityEngine;
using Unity.Physics;
using UnityEngine.UIElements;

partial class InputMovementSystem : SystemBase
{

    protected override void OnCreate()
    {
        //We will use playerForce from the GameSettingsComponent to adjust velocity
        // RequireSingletonForUpdate<GameSettingsComponent>();
        // gameSettings = GetSingleton<GameSettingsComponent>();
        
    }

    protected override void OnUpdate()
    {
        //we must declare our local variables to be able to use them in the .ForEach() below
        // var deltaTime = Time.DeltaTime;
        var deltaTime = SystemAPI.Time.DeltaTime;
        
        //we will control thrust with WASD"
        byte right, left, thrust, reverseThrust;
        right = left = thrust = reverseThrust = 0;
        
        //we will use the mouse to change rotation
        // float mouseX = 0;
        // float mouseY = 0;
        
        //we grab "WASD" for thrusting
        if (Input.GetKey("d"))
        {
            right = 1;
        }
        if (Input.GetKey("a"))
        {
            left = 2;
        }
        if (Input.GetKey("w"))
        {
            thrust = 3;
        }
        if (Input.GetKey("s"))
        {
            reverseThrust = 4;
        }
        // //we will activate rotating with mouse when the right button is clicked
        // if (Input.GetMouseButton(1))
        // {
        //     mouseX = Input.GetAxis("Mouse X");
        //     mouseY = Input.GetAxis("Mouse Y");
        //
        // }

        
        
        foreach (var player in SystemAPI.Query<PlayerAspect>().WithAll<PlayerTag>())
        {
            foreach (var bullet in SystemAPI.Query<BulletAspect>())
            {
                if (right == 1)
                {
                    //thrust to the right of where the player is facing
                    // player.MoveValue += new float3(1, 0, 0).xyz * player.MoveSpeed * deltaTime;
                    // player.MoveValue += new float3(1, 0, 0) * deltaTime;
                    player.MoveTransform.ValueRW.Position += new float3(1, 0, 0) * player.MoveSpeed * deltaTime;
                    // player.MoveTransform.ValueRW.Position += player.MoveValue;
                    bullet.SpawnPositon = player.MoveTransform.ValueRW.Position;
                }
                else if (left == 2)
                {
                    //thrust to the left of where the player is facing
                    player.MoveTransform.ValueRW.Position += new float3(-1, 0, 0) * player.MoveSpeed * deltaTime;
                    bullet.SpawnPositon = player.MoveTransform.ValueRW.Position;
                }
                else if (thrust == 3)
                {
                    //thrust forward of where the player is facing
                    player.MoveTransform.ValueRW.Position += new float3(0, 0, 1) * player.MoveSpeed * deltaTime;
                    bullet.SpawnPositon = player.MoveTransform.ValueRW.Position;
                }
                else if (reverseThrust == 4)
                {
                    //thrust backwards of where the player is facing
                    player.MoveTransform.ValueRW.Position += new float3(0, 0, -1) * player.MoveSpeed * deltaTime;

                    bullet.SpawnPositon = player.MoveTransform.ValueRW.Position;

                }
            }
        }




    }
}

