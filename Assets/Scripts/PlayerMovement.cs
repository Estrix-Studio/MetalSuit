using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float dashForce = 50f;
    [SerializeField] private float dashDistance = 5f;
    [SerializeField] private float slowdownThreshold = 1f;
    
    private Vector3 dashStartPosition;
    private Vector2 direction;
    private Vector3 lastMoveDirection;

    private bool isDashing = false;
    private bool isKnockback = false;

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
            isDashing = false;
    }

    // If the player is dashing
    if (GetComponent<Rigidbody>().velocity != Vector3.zero)
    {
            isDashing = true;
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
        if (movement.magnitude > 0)
        {
            SoundManager.instance.PlayerSound(Sound.Walk);
        }
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
        Vector3 dashDirection = this.transform.forward;
        dashDirection.y = 0; // Ensure the dash is only on the X-Z plane

        GetComponent<Rigidbody>().velocity =
            Vector3.Lerp(dashDirection, dashDirection * dashDistance, dashForce);
    }

    public void SetupMobileInput(InputActionReference leftStick)
    {
        this.leftStick = leftStick;
    }

    public bool GetIsDashing()
    {
        return isDashing;
    }

    public void TakeDamage(Vector3 damageSourcePosition, float knockbackForce, float knockbackDuration)
    {
        if (!isKnockback)
        {
            isDashing = false;
            StartCoroutine(Knockback(damageSourcePosition, knockbackForce, knockbackDuration));
        }
    }

    private System.Collections.IEnumerator Knockback(Vector3 damageSourcePosition, float knockbackForce, float knockbackDuration)
    {
        isKnockback = true;

        // Calculate the direction away from the damage source
        Vector3 knockbackDirection = (transform.position - damageSourcePosition).normalized;

        // Apply force to the Rigidbody
        GetComponent<Rigidbody>().AddForce(knockbackDirection * knockbackForce, ForceMode.Impulse);

        // Wait for the specified knockback duration
        yield return new WaitForSeconds(knockbackDuration);

        GetComponent<Rigidbody>().velocity = Vector3.zero;

        isKnockback = false;
    }
}