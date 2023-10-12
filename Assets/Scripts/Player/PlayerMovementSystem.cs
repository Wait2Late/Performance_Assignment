using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public partial class PlayerMovementSystem : SystemBase
{
    private struct Filter
    {
        public Rigidbody Rigidbody;
        public InputComponent InputComponent;
    }
    
    protected override void OnUpdate()
    {
        float deltaTime = UnityEngine.Time.deltaTime;

        

    }
    
}
