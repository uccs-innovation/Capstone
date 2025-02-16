﻿using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class PlayerController : Agent
{
    private PlayerInput playerInput;
    private Vector2 moveInput;

    public Animator animator;

    [Header("Player Settings")]
    public float moveSpeed = 2f;

    protected override void Awake()
    {
        base.Awake();
        playerInput = GetComponent<PlayerInput>();
        targetTransform = GameObject.FindWithTag("Enemy").transform;
    }

    private void OnEnable()
    {
        playerInput.onActionTriggered += HandleInput;
    }

    private void OnDisable()
    {
        playerInput.onActionTriggered -= HandleInput;
    }

    private void Update()
    {
        HandleMovement();
        RotateToMidpoint();
    }

    private void HandleInput(InputAction.CallbackContext context)
    {
        if (context.action.name == "Move")
        {
            moveInput = context.ReadValue<Vector2>();
        }
        else if (context.action.name == "Right Hook" && context.performed)
        {
            TriggerHook();
        }
    }

    private void HandleMovement()
    {
        Vector3 inputDirection = new Vector3(moveInput.x, 0, moveInput.y).normalized;

        if (inputDirection.magnitude > 0)
        {
            // Convert input to local space relative to the player's orientation
            Vector3 localDirection = transform.TransformDirection(inputDirection);

            Move(localDirection, moveSpeed);
        }
    }

    public void TriggerHook()
    {
        Debug.Log("Hook Thrown!");
        animator.SetTrigger("Right_Hook");
    }

    protected override void OnHealthChanged()
    {
        Debug.Log("Player health changed: " + health);
    }

    protected override void OnDeath()
    {
        Debug.Log("Player has died.");
        GameManager.Instance.HandleGameOver(false);
    }
}
