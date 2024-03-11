using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed;

    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Transform groundPoint;
    private Vector2 moveInputValue;
    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        Run();
    }

    private void OnMove_IA(InputValue value)
    {
        moveInputValue = value.Get<Vector2>();
    }

    private void OnJump_IA()
    {
        if (Physics.CheckSphere(groundPoint.position, 0.1f, groundLayer))
            rb.AddForce(Vector3.up * 300);
    }

    private void Run()
    {
        var result = new Vector3(moveInputValue.x, 0, moveInputValue.y) * speed * Time.fixedDeltaTime;
        result.y = rb.velocity.y;
        rb.velocity = result;
    }
}