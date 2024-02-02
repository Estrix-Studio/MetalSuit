using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Vector3 offset;
    [SerializeField] private Transform target;
    
    private Vector3 velocity = Vector3.zero;
    private float smoothTime = 0.25f;

    void FixedUpdate()
    {
        Vector3 targetPosition = target.position + offset;
        targetPosition.y = offset.y;
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
    }
}
