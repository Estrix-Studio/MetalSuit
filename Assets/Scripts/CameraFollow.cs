using UnityEngine;
using UnityEngine.Serialization;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] public Vector3 cameraOffset;
    public Transform target;
    public float smoothSpeed = 0.125f;
    

    
    void FixedUpdate()
    {
        var position = target.position;
        Vector3 desiredPosition = position + cameraOffset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;

        Vector3 lookAtPosition = position;
        lookAtPosition.y += 1;
        
        transform.LookAt(lookAtPosition);
    }
}