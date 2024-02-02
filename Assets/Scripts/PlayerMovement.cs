using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float dashForce = 10f;
    [SerializeField] private float dashDuration = 0.2f;

    private Joystick joystick;
    private bool isDashing = false;
    private Vector2 direction;
    private Vector3 lastMoveDirection;

    [SerializeField] private InputActionReference leftStick;
    [SerializeField] private InputActionReference attackButton;

    private void Start()
    {
        joystick = FindObjectOfType<Joystick>();
    }

    private void FixedUpdate()
    {
        if (!isDashing)
        {
            direction = leftStick.action.ReadValue<Vector2>();
            MovePlayer(direction);

            Vector2 keyboardInput = GetKeyboardInput();
            MovePlayer(keyboardInput);


            CheckForDash();
        }
    }

    private Vector2 GetKeyboardInput()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
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
        // Check for dash using joystick
        if (attackButton.action.ReadValue<float>() > 0 || Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Attack");
            StartCoroutine(Dash());
        }
    }

    private System.Collections.IEnumerator Dash()
    {
        if (direction.magnitude > 0)
        {
            isDashing = true;

            Vector3 dashDestination = transform.position + lastMoveDirection * dashForce;

            float elapsedTime = 0f;

            while (elapsedTime < dashDuration)
            {
                GetComponent<Rigidbody>().MovePosition(Vector3.Lerp(transform.position, dashDestination, elapsedTime / dashDuration));
                elapsedTime += Time.fixedDeltaTime;
                yield return null;
            }

            isDashing = false;
        }
    }

    public void SetDirection(Vector2 newDirection)
    {
        direction = newDirection.normalized;
    }
}
