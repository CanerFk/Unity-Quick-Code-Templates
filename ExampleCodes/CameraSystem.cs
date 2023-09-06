using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraSystem : MonoBehaviour
{
    //"Add the Cinemachine system and create a virtual camera. Create an empty object called 'Camera System,' and assign it to the Follow and Look At sections inside Cinemachine.
    //Adjust the Follow Offset values according to your preferences; I used 0, 20, -22 values for myself. For sharper movements, you can set the x, y, and z damping values to 0.

    [SerializeField] private CinemachineVirtualCamera cinemachineVirtualCamera;
    private float targetFieldOfView = 50f;
    private bool isRotating;
    private void Update()
    {
        HandleMovement();
        HandleZoom();
        HandleRotation();
    }
    private void HandleMovement()
    {
        // Camera Movement
        Vector3 inputDir = Vector3.zero;

        if (Input.GetKey(KeyCode.W)) inputDir.z = 1f;
        if (Input.GetKey(KeyCode.S)) inputDir.z = -1f;
        if (Input.GetKey(KeyCode.D)) inputDir.x = 1f;
        if (Input.GetKey(KeyCode.A)) inputDir.x = -1f;

        Vector3 moveDir = transform.forward * inputDir.z + transform.right * inputDir.x;
        moveDir.Normalize();

        float moveSpeed = 30f;
        transform.position += moveDir *moveSpeed* Time.deltaTime;
    }
    private void HandleZoom()
    {
        if(Input.mouseScrollDelta.y > 0)
        {
            targetFieldOfView -= 5f;
        }
        else if (Input.mouseScrollDelta.y < 0)
        {
            targetFieldOfView += 5f;
        }

        targetFieldOfView = Mathf.Clamp(targetFieldOfView, 10f, 50f);
        float zoomSpeed = 10f;
        cinemachineVirtualCamera.m_Lens.FieldOfView = Mathf.Lerp(cinemachineVirtualCamera.m_Lens.FieldOfView, targetFieldOfView, Time.deltaTime * zoomSpeed);
    }
    private void HandleRotation()
    {
        if (Input.GetMouseButtonDown(1)) isRotating = true;
        else if (Input.GetMouseButtonUp(1)) isRotating = false;

        if(isRotating)
        {
            float rotationSpeed = 3f;
            float mouseX = Input.GetAxis("Mouse X") * rotationSpeed;
            Vector3 Rotation = new Vector3(0f, mouseX, 0f);
            transform.Rotate(Rotation);
        }
    }
}
