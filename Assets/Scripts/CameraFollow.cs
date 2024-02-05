using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float distance = 10.0f; // Distance from the player
    [SerializeField] private float height = 5.0f; // Height above the player

    void FixedUpdate()
    {
        // Position the camera behind the player at the specified distance and height
        Vector3 targetPosition = target.position - target.forward * distance;
        targetPosition.y = target.position.y + height;
        transform.position = targetPosition;

        // Make the camera look at the player
        transform.LookAt(target);
    }
}
