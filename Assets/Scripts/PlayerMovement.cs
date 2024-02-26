using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.EnhancedTouch;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float dashForce = 50f;
    [SerializeField] private float dashDistance = 5f;
    [SerializeField] private float slowdownThreshold = 1f;
    [SerializeField] private float maxDashDuration = 1f;

    private Vector3 dashStartPosition;
    private Vector2 swipeStartPosition;
    private Vector2 direction;
    private Vector3 lastMoveDirection;

    private bool isDashing = false;
    private float dashTimer = 0f;

    private bool isUsingSwipe = false;

    private bool isKnockback = false;

    [SerializeField] private InputActionReference PrimaryContact;
    [SerializeField] private InputActionReference PrimaryPosition;

    public void Initialize()
    {

        // Initialization code here, if needed
    }

    private void Start()
    {
        PrimaryContact.action.started += ctx => OnStartTouchPrimary(ctx);
        PrimaryContact.action.canceled += ctx => OnEndTouchPrimary(ctx);
        //PrimaryPosition.action.performed += _ => OnTapPerformed();
    }

    private void FixedUpdate()
    {
        if (!isDashing && isUsingSwipe)
        {
            direction = PrimaryPosition.action.ReadValue<Vector2>() - swipeStartPosition;
            MovePlayer(direction.normalized);
        }

        // If the player is dashing
        if (isDashing)
        {
            dashTimer += Time.fixedDeltaTime;

            // Check if the elapsed time exceeds the max dash duration
            if (dashTimer >= maxDashDuration)
            {
                isDashing = false;
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

    private void OnStartTouchPrimary(InputAction.CallbackContext context)
    {
        Debug.Log("Swipe");
        if (!isUsingSwipe)
        {
            isUsingSwipe = true;
            swipeStartPosition = PrimaryPosition.action.ReadValue<Vector2>();
        }
    }

    private void OnEndTouchPrimary(InputAction.CallbackContext context)
    {
        isUsingSwipe = false;
        Dash();
    }
    private void OnTapPerformed()
    {
        Debug.Log("Tap");
        if (!isDashing)
        {
            Dash();
        }
    }
    private void Dash()
    {
        if (!isDashing)
        {
            isDashing = true;
            dashStartPosition = transform.position;

            Vector3 dashDirection = lastMoveDirection;
            dashDirection.y = 0; // Ensure the dash is only on the X-Z plane

            GetComponent<Rigidbody>().velocity = dashDirection.normalized * dashForce;
            dashTimer = 0f; // Reset the dash timer
        }
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

    public void SetupMobileInput(InputActionReference swipeAction, InputActionReference tap)
    {
        this.PrimaryContact = swipeAction;
        this.PrimaryPosition = tap;
    }
}
