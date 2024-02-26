using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float dashForce = 50f;
    [SerializeField] private float dashDistance = 5f;
    [SerializeField] private float slowdownThreshold = 1f;
    [SerializeField] private float maxDashDuration = .75f;

    private Vector3 dashStartPosition;
    private Vector2 direction;
    private Vector3 lastMoveDirection;

    private bool isDashing = false;
    private float dashTimer = 0f;

    bool isKnockback;

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
        if (!isDashing)
        {
            direction = leftStick.action.ReadValue<Vector2>();
            MovePlayer(direction);
        }

        // If the player is dashing
        if (isDashing)
        {
            Debug.Log("Dashing");
            dashTimer += Time.fixedDeltaTime;

            // Check if the elapsed time exceeds the max dash duration
            if (dashTimer >= maxDashDuration)
            {
                isDashing = false;
                Debug.Log("Stop");
                GetComponent<Rigidbody>().velocity = Vector3.zero;
                dashStartPosition = transform.position;
                dashTimer = 0f; // Reset the dash timer
            }
            else
            {
                // Apply slowdown only when the remaining distance is less than the slowdown threshold
                float remainingDistance = dashDistance - Vector3.Distance(dashStartPosition, transform.position);
                if (remainingDistance < slowdownThreshold)
                {
                    float slowdownFactor = remainingDistance / slowdownThreshold;
                    GetComponent<Rigidbody>().velocity *= slowdownFactor;
                }
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
        Debug.Log("Release");
        Dash();
    }

    private void Dash()
    {
        if (!isDashing)
        {
            Debug.Log("Start Dash");
            isDashing = true;
            dashStartPosition = transform.position;

            Vector3 dashDirection = this.transform.forward;
            dashDirection.y = 0; // Ensure the dash is only on the X-Z plane

            GetComponent<Rigidbody>().velocity = dashDirection * dashForce;
            dashTimer = 0f; // Reset the dash timer
        }
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
