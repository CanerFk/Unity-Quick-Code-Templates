using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    [SerializeField] float carSpeed;
    [SerializeField] float maxSpeed;

    [SerializeField] float steerAngle;
    float dragAmount = 0.98f;
    [SerializeField] float Traction;
    public Transform lw, rw;
    Vector3 _moveVec;
    Vector3 _rotVec;

    void Update()
    {
        /* _moveVec += transform.forward * carSpeed * Time.deltaTime;
         transform.position += _moveVec * Time.deltaTime;

         _rotVec = new Vector3(0, SimpleInput.GetAxis("Horizontal"), 0);

         transform.Rotate(Vector3.up * SimpleInput.GetAxis("Horizontal") * steerAngle * Time.deltaTime * _moveVec.magnitude);

         _moveVec *= dragAmount;
         _moveVec = Vector3.ClampMagnitude(_moveVec, maxSpeed);
         _moveVec = Vector3.Lerp(_moveVec.normalized, transform.forward, Traction * Time.deltaTime) * _moveVec.magnitude;

         _rotVec = Vector3.ClampMagnitude(_rotVec, steerAngle);
         lw.localRotation = Quaternion.Euler(_rotVec);
         rw.localRotation = Quaternion.Euler(_rotVec);*/

        _moveVec += transform.forward * carSpeed * Time.deltaTime * Input.GetAxis("Vertical");
        transform.position += _moveVec * Time.deltaTime;

        float steerInput = Input.GetAxis("Horizontal");
        transform.Rotate(Vector3.up * steerInput * steerAngle * _moveVec.magnitude * Time.deltaTime);

        _moveVec *= dragAmount;
        _moveVec = Vector3.ClampMagnitude(_moveVec, maxSpeed);

        Debug.DrawRay(transform.position, _moveVec.normalized * 3);
        Debug.DrawRay(transform.position, transform.position * 3, Color.blue);
        _moveVec = Vector3.Lerp(_moveVec.normalized, transform.forward, Traction * Time.deltaTime) * _moveVec.magnitude;
    }
}
