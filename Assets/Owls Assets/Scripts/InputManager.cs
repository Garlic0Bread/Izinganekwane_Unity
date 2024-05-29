using SG;
using UnityEngine;
using System.Collections;
using UnityEngine.Windows;
using UnityEngine.InputSystem;
using System.Collections.Generic;

public class InputManager : MonoBehaviour
{
    PlayerAttacker playerAttacker;
    PlayerControls playerControls;
    PlayerInventory playerInventory;
    PlayerLocomotion playerLocomotion;
    PlayerManager playerManager;

    [SerializeField] private Vector2 movementInput;
    [SerializeField] private Vector2 cameraInput;

    public float cameraInputX;
    public float cameraInputY;

    public float moveAmount;
    public float verticalInput;
    public float horizontalInput;

    public bool jumping_Input;
    public bool sprinting_Input;
    public bool LightMelee_1;
    public bool LightMelee_2;
    public bool HeavyMelee_1;

    AnimatorManager animManager;

    private void Awake()
    {
        animManager = GetComponentInChildren<AnimatorManager>();
        playerLocomotion = GetComponent<PlayerLocomotion>();
        playerInventory = GetComponent<PlayerInventory>();
        playerAttacker = GetComponent<PlayerAttacker>();
        playerManager = GetComponent<PlayerManager>();
    }

    private void OnEnable()
    {
        if (playerControls == null)
        {
            playerControls = new PlayerControls();
            playerControls.PlayerMovement.Movement.performed += i => movementInput = i.ReadValue<Vector2>();
            playerControls.PlayerMovement.Camera.performed += i => cameraInput = i.ReadValue<Vector2>();

            playerControls.PlayerActions.Shift.performed += i => sprinting_Input = true;
            playerControls.PlayerActions.Shift.canceled += i => sprinting_Input = false;

            playerControls.PlayerActions.Jump.canceled += i => jumping_Input = true;
        }
        playerControls.Enable();
    }
    private void OnDisable()
    {
        playerControls.Disable();
    }

    public void HandleAllInputs()
    {
        HandleJumpInput();
        HandleAttackInput();
        HandleMovementInput();
        HandleSprintingInput();
    }

    private void HandleJumpInput()
    {
        if (jumping_Input)
        {
            jumping_Input = false;
            playerLocomotion.HandleJumping();
        }
    }
    private void HandleMovementInput()
    {
        verticalInput = movementInput.y;
        horizontalInput = movementInput.x;

        cameraInputX = cameraInput.x;
        cameraInputY = cameraInput.y;

        moveAmount = Mathf.Clamp01(Mathf.Abs(horizontalInput) + Mathf.Abs(verticalInput));
        animManager.UpdateAnimatorValues(0, moveAmount, playerLocomotion.isSprinting);
    }
    private void HandleSprintingInput()
    {
        if (sprinting_Input == true && moveAmount > 0.5f)
        {
            playerLocomotion.isSprinting = true;
        }
        else
        {
            playerLocomotion.isSprinting= false;
        }
    }
    private void HandleAttackInput()
    {
        playerControls.PlayerActions.Attack.performed += i => HeavyMelee_1 = true;
        playerControls.PlayerActions.Attack.performed += i => LightMelee_1 = true;
        playerControls.PlayerActions.Attack.performed += i => LightMelee_2 = true;

        if (playerManager.joystickMoveVectror.y >= .5f)
        {
            playerAttacker.HandleLightMelee_1(playerInventory.rightHandWeapon);
        }

        if (playerManager.joystickMoveVectror.y >= .8f)
        {
            playerAttacker.HandleHeavyMelee_1(playerInventory.rightHandWeapon);
        }

        if (playerManager.joystickMoveVectror.y <= -.5f)
        {
            playerAttacker.HandleLightMelee_2(playerInventory.leftHandWeapon);
        }
    }
}
