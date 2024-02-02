using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float dashForce = 10f;
    [SerializeField] private float dashDuration = 0.2f;
    [SerializeField] private float dashCooldown = 0.75f;

    private bool isDashing = false;
    private bool isDashCooldown = false;
    private Vector2 direction;
    private Vector3 lastMoveDirection;

    [SerializeField] private InputActionReference leftStick;
    [SerializeField] private InputActionReference attackButton;

    private bool isUsingKeyboard;

    public void Initialize()
    {
        // Check if running in Editor or on PC

        // Initialization code here, if needed
    }

    public void FixedUpdate()
    {
        if (leftStick.action.ReadValue<Vector2>() != Vector2.zero)
            isUsingKeyboard = false;
        else
            isUsingKeyboard = true;

        if (!isDashing)
        {
            direction = isUsingKeyboard ? GetKeyboardInput() : leftStick.action.ReadValue<Vector2>();
            MovePlayer(direction);

            CheckForDash();
        }
    }

    private Vector2 GetKeyboardInput()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        float dash = Input.GetAxis("Dash");
        return new Vector2(horizontal, vertical);
    }

    private void MovePlayer(Vector2 moveDirection)
    {
        lastMoveDirection = new Vector3(moveDirection.x, 0, moveDirection.y);

        Vector3 movement = lastMoveDirection * moveSpeed * Time.fixedDeltaTime;
        GetComponent<Rigidbody>().MovePosition(transform.position + movement);
    }

    public void CheckForDash()
    {
        if (Input.GetAxis("Dash") > 0|| attackButton.action.ReadValue<float>() > 0)
        {
            Debug.Log("Attack");
            StartCoroutine(Dash());
        }
    }

    private System.Collections.IEnumerator Dash()
    {
        if (!isDashing && !isDashCooldown)
        {
            isDashing = true;

            Debug.Log(lastMoveDirection);
            Vector3 dashDestination = transform.position + lastMoveDirection * dashForce;

            float elapsedTime = 0f;

            while (elapsedTime < dashDuration)
            {
                GetComponent<Rigidbody>().MovePosition(Vector3.Lerp(transform.position, dashDestination, elapsedTime / dashDuration));
                elapsedTime += Time.fixedDeltaTime;
                yield return null;
            }

            isDashing = false;
            isDashCooldown = true;

            // Add cooldown duration (e.g., 3 seconds)
            yield return new WaitForSeconds(dashCooldown);

            isDashCooldown = false;
        }
    }

    public void SetDirection(Vector2 newDirection)
    {
        direction = newDirection.normalized;
    }

    public void SetupMobileInput(InputActionReference leftStick, InputActionReference attackButton)
    {
        this.leftStick = leftStick;
        this.attackButton = attackButton;
    }
}
