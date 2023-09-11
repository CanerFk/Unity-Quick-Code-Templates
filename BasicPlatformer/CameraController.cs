using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KameraTakip : MonoBehaviour
{
    public Transform target;
    public float softness = 5.0f;
    private Vector3 distance;

    public Transform roomBoundary;

    private float minX, maxX, minY, maxY;

    void Start()
    {
        distance = transform.position - target.position;

        if (roomBoundary != null)
        {
            Bounds bounds = roomBoundary.GetComponent<Collider2D>().bounds;
            minX = bounds.min.x;
            maxX = bounds.max.x;
            minY = bounds.min.y;
            maxY = bounds.max.y;
        }
    }

    void FixedUpdate()
    {
        Vector3 targetPos = target.position + distance;

        targetPos.x = Mathf.Clamp(targetPos.x, minX, maxX);
        targetPos.y = Mathf.Clamp(targetPos.y, minY, maxY);

        transform.position = Vector3.Lerp(transform.position, targetPos, softness * Time.fixedDeltaTime);
    }
}
