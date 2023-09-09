using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcadeCarController : MonoBehaviour
{
    private float moveInput;
    private float turnInput;
    private bool isGrounded;

    public float airDrag;
    public float groundDrag;

    public float fwdSpeed;
    public float revSpeed;
    public float turnSpeed;

    public Rigidbody sphereB;
    public LayerMask groundLayer;

    void Start()
    {
        sphereB.transform.parent = null;   
    }
    void Update()
    {
        moveInput = Input.GetAxisRaw("Vertical");
        turnInput = Input.GetAxisRaw("Horizontal");

        moveInput *= moveInput > 0 ? fwdSpeed : revSpeed;

        float newRotation = turnInput * turnSpeed * Time.deltaTime* Input.GetAxisRaw("Vertical");

        transform.Rotate(0, newRotation, 0, Space.World);
        transform.position = sphereB.transform.position;


        RaycastHit hit;
        isGrounded = Physics.Raycast(transform.position, -transform.up, out hit,1f, groundLayer);
        transform.rotation = Quaternion.FromToRotation(transform.up, hit.normal) * transform.rotation;

        if(isGrounded)
        {
            sphereB.drag = groundDrag;
        }
        else
        {
            sphereB.drag = airDrag;
        }    

    }
    private void FixedUpdate()
    {
        if (isGrounded)
        {
            sphereB.AddForce(moveInput * transform.forward, ForceMode.Acceleration);
        }
        else
        {
            sphereB.AddForce(transform.up * -9.8f);
        }
    }
}
