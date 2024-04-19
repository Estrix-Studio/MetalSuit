using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 20;
    [SerializeField] private float dashForce = 50f;
    [SerializeField] private float dashDistance = 5f;
    [SerializeField] private float slowdownThreshold = 1f;
    [SerializeField] private float maxDashDuration = .5f;
    [SerializeField] private float maxSpeed = 20;

    [SerializeField] private InputActionReference PrimaryContact;
    [SerializeField] private InputActionReference PrimaryPosition;

    private PlayerController _playerController;

    private float clampedMagnitude;

    private Vector3 dashStartPosition;
    private float dashTimer;
    private Vector2 direction;

    private bool isDashing;

    private bool isKnockback;

    private bool isUsingSwipe;
    private Vector3 lastMoveDirection;
    private Vector2 swipeStartPosition;

    private void Start()
    {
        PrimaryContact.action.started += ctx => OnStartTouchPrimary(ctx);
        PrimaryContact.action.canceled += ctx => OnEndTouchPrimary(ctx);

        //PrimaryPosition.action.performed += _ => OnTapPerformed();
    }

    private void FixedUpdate()
    {
        if (isUsingSwipe && !isKnockback)
        {
            direction = PrimaryPosition.action.ReadValue<Vector2>() - swipeStartPosition;
            clampedMagnitude = Mathf.Clamp(direction.magnitude, 0f, 600f) / 600f;
            direction = direction.normalized; // Normalize the direction vector

            MovePlayer(direction.normalized);
            SoundManager.instance.PlayerSound(Sound.Walk);
        }
        else if (!isDashing && !isUsingSwipe && !isKnockback)
        {
            // Stop the player from sliding
            GetComponent<Rigidbody>().velocity = Vector3.zero;
            GetComponent<Rigidbody>().freezeRotation = true;
        }

        // If the player is dashing
        BrakeDash();
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Obstacle")
            SoundManager.instance.PlayerSound(Sound.PlayerHit);
    }

    public void Initialize()
    {
        // Initialization code here, if needed


        _playerController = GetComponent<PlayerController>();
    }


    private void BrakeDash()
    {
        if (isDashing && !isKnockback)
        {
            dashTimer += Time.fixedDeltaTime;
            // Apply slowdown only when the remaining distance is less than the slowdown threshold
            var remainingDistance = dashDistance - Vector3.Distance(dashStartPosition, transform.position);
            if (remainingDistance < slowdownThreshold)
            {
                var slowdownFactor = remainingDistance / slowdownThreshold;
                GetComponent<Rigidbody>().velocity *= slowdownFactor;
                if (GetComponent<Rigidbody>().velocity.sqrMagnitude == 0 || dashTimer >= maxDashDuration)
                {
                    isDashing = false;
                    dashTimer = 0f;
                    _playerController.Animator.SetBool(PlayerController.IsDashing, false);
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
            var toRotation = Quaternion.LookRotation(lastMoveDirection, Vector3.up);
            transform.rotation = Quaternion.Lerp(transform.rotation, toRotation, moveSpeed * Time.deltaTime);

            // Set the animator parameter to true
            _playerController.Animator.SetBool(PlayerController.IsWalking, true);
        }

        // Calculate the desired velocity
        var desiredVelocity = lastMoveDirection * moveSpeed;

        // Clamp the magnitude of the velocity to the maximum speed
        if (desiredVelocity.magnitude > maxSpeed * clampedMagnitude)
            desiredVelocity = desiredVelocity.normalized * maxSpeed * clampedMagnitude;

        // Apply the velocity change
        GetComponent<Rigidbody>().velocity = desiredVelocity;
    }


    private void Dash()
    {
        if (isDashing) return;
        isDashing = true;
        dashStartPosition = transform.position;

        var dashDirection = lastMoveDirection;
        dashDirection.y = 0; // Ensure the dash is only on the X-Z plane

        GetComponent<Rigidbody>().AddForce(dashDirection.normalized * dashForce, ForceMode.Impulse);
        _playerController.Animator.SetBool(PlayerController.IsWalking, false);
        _playerController.Animator.SetBool(PlayerController.IsDashing, true);
    }

    private void OnStartTouchPrimary(InputAction.CallbackContext context)
    {
        if (!isUsingSwipe)
        {
            swipeStartPosition = PrimaryPosition.action.ReadValue<Vector2>();
            isUsingSwipe = true;
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
        if (!isDashing) Dash();
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

    private IEnumerator Knockback(Vector3 damageSourcePosition, float knockbackForce, float knockbackDuration)
    {
        isKnockback = true;

        // Calculate the direction away from the damage source
        var knockbackDirection = (transform.position - damageSourcePosition).normalized;

        // Apply force to the Rigidbody
        GetComponent<Rigidbody>().AddForce(knockbackDirection * knockbackForce, ForceMode.Impulse);

        // Wait for the specified knockback duration
        yield return new WaitForSeconds(knockbackDuration);

        GetComponent<Rigidbody>().velocity = Vector3.zero;

        Debug.Log("Knock");
        isKnockback = false;
    }

    public void SetupMobileInput(InputActionReference swipeAction, InputActionReference tap)
    {
        PrimaryContact = swipeAction;
        PrimaryPosition = tap;
    }
}