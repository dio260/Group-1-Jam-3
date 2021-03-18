using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    public static bool StopPlayer { get; set; }
    [ReadOnly] [SerializeField] private float gravity;
    [SerializeField] private float movementSpeed, gravityModifier, jumpForce, raycastDistance;
    private Action state;
    private CharacterController controller;
    private float defaultStep;
    private CalculateFloorCollision collision;

    private void Awake()
    {
        collision = transform.Find("FloorCollision").GetComponent<CalculateFloorCollision>();
        state = Movement;
        controller = GetComponent<CharacterController>();
        defaultStep = controller.stepOffset;
    }

    private void FixedUpdate()
    {
        HandleGravity();
    }

    private void Update()
    {
        if (StopPlayer) state = StopActions;
        state();
    }

    private void Movement()
    {
        Vector3 movement = new Vector3();

        if (Input.GetKey(KeyCode.S)) movement.z = -1;
        if (Input.GetKey(KeyCode.A)) movement.x = -1;
        if (Input.GetKey(KeyCode.W)) movement.z = 1;
        if (Input.GetKey(KeyCode.D)) movement.x = 1;

        if (Input.GetKeyDown(KeyCode.Space) && collision.IsGrounded)
        {
            gravity = jumpForce;
        }

        controller.Move(movement.normalized * movementSpeed * Time.deltaTime);
    }
    
    private void StopActions()
    {
        if (!StopPlayer) state = Movement;
    }

    private void HandleGravity()
    {
        if (collision.IsGrounded && gravity < 0)
        {
            controller.stepOffset = defaultStep;
            gravity = 0;
        }
        else if (!collision.IsGrounded)
        {
            controller.stepOffset = 0;
            gravity += gravityModifier * Time.fixedDeltaTime;
        }

        controller.Move(new Vector3(0, gravity, 0) * Time.fixedDeltaTime);
    }
}
