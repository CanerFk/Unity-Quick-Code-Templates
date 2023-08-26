using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Camera playerCamera;
    [SerializeField] Transform groundCheckTransform;
    [SerializeField] LayerMask groundLayer;
    public float mouseSensitivity { get; private set; } = 175f;
    public float xRotation { get; private set; } = 0;
    public bool isGrounded { get; private set; } = false;
    public bool isWalking { get; private set; } = false;
    public bool isJumping { get; private set; } = false;
    [System.NonSerialized] public Vector3 jumpVelocity = Vector3.zero;

    private CharacterController characterController;
    private PlayerStats playerStats;
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        playerStats = GetComponent<PlayerStats>();
        Cursor.lockState = CursorLockMode.Locked;
    }
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheckTransform.position, 0.4f, groundLayer);
        HandleJumpInput();
        HandleMovement();
        HandleMouseLook();
    }
    void HandleMovement()
    {
        float verticalInput = Input.GetAxis("Vertical");
        float horizontalInput = Input.GetAxis("Horizontal");
        isWalking = Input.GetKey(KeyCode.LeftShift);
        Vector3 movementVector = Vector3.ClampMagnitude(verticalInput * transform.forward + horizontalInput * transform.right, 1.0f);
        if(isWalking)
        {
            characterController.Move(movementVector * playerStats.walkingMovementSpeed * Time.deltaTime);
        }
        else
        {
            characterController.Move(movementVector * playerStats.runningMovementSpeed * Time.deltaTime);
        }
        
    }
    void HandleMouseLook()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        playerCamera.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);
    }
    void HandleJumpInput()
    {
        bool isTryingtoJump = Input.GetKeyDown(KeyCode.Space);
        if(isTryingtoJump && isGrounded)
        {
            isJumping = true;
        }
        else
        {
            isJumping = false;
        }
        if (isGrounded && jumpVelocity.y <0f)
        {
            jumpVelocity.y = -2f;
        }
        if (isJumping==true)
        {
            jumpVelocity.y = Mathf.Sqrt(playerStats.jumpHeight * -2 * playerStats.gravity);
        }
        jumpVelocity.y += playerStats.gravity * Time.deltaTime;
        characterController.Move(jumpVelocity * Time.deltaTime);
    }
}
