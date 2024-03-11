using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] public Vector3 cameraOffset;
    public Transform target;
    public float smoothSpeed = 0.125f;


    private void FixedUpdate()
    {
        var position = target.position;
        var desiredPosition = position + cameraOffset;
        var smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;

        var lookAtPosition = position;
        lookAtPosition.y += 1;

        transform.LookAt(lookAtPosition);
    }
}