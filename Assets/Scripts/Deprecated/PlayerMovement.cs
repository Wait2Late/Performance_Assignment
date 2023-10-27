using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField] private float speed = 5;
    [SerializeField] private float rotationSpeed = 5;
    [SerializeField] private float gravity = 5;
    [SerializeField] private float jumpSpeed = 5;
    
    private float3 moveVelocity;
    private float3 turnVelocity;

    private CharacterController cc;
    void Start()
    {
        cc = GetComponent<CharacterController>();
    }

    void Update()
    {
        float hInput = Input.GetAxis("Horizontal");
        float vInput = Input.GetAxis("Vertical");

        moveVelocity = transform.forward * (speed * vInput);
        turnVelocity = transform.right * (rotationSpeed * hInput);

        if (Input.GetButtonDown("Jump"))
        {
            moveVelocity.y = jumpSpeed;
        }

        moveVelocity.y += gravity * Time.deltaTime;
        cc.Move(moveVelocity * Time.deltaTime);
        cc.Move(turnVelocity * Time.deltaTime);
        transform.Rotate(turnVelocity * Time.deltaTime);
    }
}
