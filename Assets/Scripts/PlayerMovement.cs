using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float dashForce = 30;
    [SerializeField] private float dashDistance = 3f;
    [SerializeField] private float slowdownThreshold = 1f;
    
    private Vector3 dashStartPosition;
    private Vector2 direction;
    private Vector3 lastMoveDirection;

    [SerializeField] private InputActionReference leftStick;

    private bool isUsingKeyboard;

    public void Initialize()
    {
        // Subscribe to the performed and canceled events of the joystick action
        leftStick.action.performed += _ => OnJoystickMoved();
        leftStick.action.canceled += _ => OnJoystickReleased();
    }

    public void FixedUpdate()
    {
        
        direction = leftStick.action.ReadValue<Vector2>();
        
        MovePlayer(direction);
        
    // Check if the player has dashed the maximum distance
    if (Vector3.Distance(dashStartPosition, transform.position) >= dashDistance)
    {
        // Stop the dash
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        dashStartPosition = transform.position;
    }

    // If the player is dashing
    if (GetComponent<Rigidbody>().velocity != Vector3.zero)
    {
        float remainingDistance = dashDistance - Vector3.Distance(dashStartPosition, transform.position);

        // If the remaining distance is less than the slowdown threshold, start reducing the player's speed
        if (remainingDistance < slowdownThreshold)
        {
            float slowdownFactor = remainingDistance / slowdownThreshold;
            GetComponent<Rigidbody>().velocity *= slowdownFactor;
        }
    }
    }

    private void MovePlayer(Vector2 moveDirection)
    {
        lastMoveDirection = new Vector3(moveDirection.x, 0, moveDirection.y);

        // Rotate the player to face in the direction of movement only if the player is moving
        if (lastMoveDirection != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(lastMoveDirection, Vector3.up);
            transform.rotation = Quaternion.Lerp(transform.rotation, toRotation, moveSpeed * Time.deltaTime);
        }

        Vector3 movement = lastMoveDirection * moveSpeed * Time.deltaTime;
        GetComponent<Rigidbody>().MovePosition(transform.position + movement);
    }

    private void OnJoystickMoved()
    {
        direction = leftStick.action.ReadValue<Vector2>();
        MovePlayer(direction);
    }

    private void OnJoystickReleased()
    {
        Dash();
    }

    private void Dash()
    {
        // Start the dash
        var position = transform.position;

        // Use the last move direction as the dash direction
        Vector3 dashDirection = this.transform.forward;
        dashDirection.y = 0; // Ensure the dash is only on the X-Z plane

        GetComponent<Rigidbody>().velocity =
            Vector3.Lerp(position, position + dashDirection * dashDistance, dashForce);

        // Reset the dash start position
        dashStartPosition = transform.position;
    }

    public void SetupMobileInput(InputActionReference leftStick)
    {
        this.leftStick = leftStick;
    }
}