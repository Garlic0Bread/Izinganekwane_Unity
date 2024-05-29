using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerManager : MonoBehaviour
{
    Animator animator;
    CameraManager camManager;
    InputManager inputManager;
    PlayerLocomotion playerLocomotion;

    public Vector2 joystickMoveVectror;
    public float jpystickMoveSpeed;

    public bool isInteracting;
    public int noOfClicks = 0;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        camManager = FindObjectOfType<CameraManager>();
        inputManager = GetComponent<InputManager>();
        playerLocomotion = GetComponent<PlayerLocomotion>();
    }

    private void Update()
    {
        inputManager.HandleAllInputs();
    }

    private void FixedUpdate()
    {
        playerLocomotion.HandleAllMovement();
    }

    private void LateUpdate()
    {
        camManager.HandleAllCameraMovements();
        if(isInteracting )
        {
            animator.GetBool("isInteracting");
            playerLocomotion.isJumping = animator.GetBool("isJumping");
            animator.SetBool("isGrounded", playerLocomotion.isGrounded);
        }
        inputManager.LightMelee_1 = false;
        inputManager.LightMelee_2 = false;
        inputManager.HeavyMelee_1 = false;
    }
    public void InputPlayer(InputAction.CallbackContext context)
    {
        joystickMoveVectror = context.ReadValue<Vector2>();
    }
}
