using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Camera playerCamera;
    [SerializeField] Transform groundCheckTransform;
    [SerializeField] LayerMask groundLayer;
    public float mouseSensitivity { get; private set; } = 100f;
    public float xRotation { get; private set; } = 0;
    public bool isGrounded { get; private set; } = false;
    public bool isWalking { get; private set; } = false;
    public bool isJumping { get; private set; } = false;
    public bool isCrouching { get; private set; } = false;

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
        isCrouching = Input.GetKey(KeyCode.LeftControl);
        
        if(isCrouching)
        {
            HandleCrouch();
        } else
        {
            HandleStand();
        }

        if (isCrouching)
        {
            characterController.Move(movementVector * playerStats.crouchMovementSpeed * Time.deltaTime);
        }else if (isWalking)
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
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime * 2;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime * 2;

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
    void HandleCrouch()
    {
        if (characterController.height > playerStats.crouchHeightY)
        {
            UpdateCharacterHeight(playerStats.crouchHeightY);

            if (characterController.height - 0.05f <= playerStats.crouchHeightY)
            {
                characterController.height = playerStats.crouchHeightY;
            }
        }
    }
    void HandleStand()
    {
        if(characterController.height < playerStats.standingHeightY)
        {
            float lastHeight = characterController.height;

            //Checking for the presence of an object that will prevent the player from standing up
            RaycastHit hit; 
            if (Physics.Raycast(transform.position,transform.up,out hit, playerStats.standingHeightY))
            {
                if(hit.distance < playerStats.standingHeightY - playerStats.crouchHeightY)
                {
                    UpdateCharacterHeight(playerStats.crouchHeightY + hit.distance);
                    return;
                }
                else
                {
                    UpdateCharacterHeight(playerStats.standingHeightY);
                }
            }
            else
            {
                UpdateCharacterHeight(playerStats.standingHeightY);
            }

            if(characterController.height + 0.05f >= playerStats.standingHeightY)
            {
                characterController.height = playerStats.standingHeightY;
            }
            transform.position += new Vector3(0, (characterController.height-lastHeight/2),0);
        }
    }
    void UpdateCharacterHeight(float newHeight)
    {
        characterController.height = Mathf.Lerp(characterController.height, newHeight, playerStats.crouchSpeed * Time.deltaTime);
    }
}
