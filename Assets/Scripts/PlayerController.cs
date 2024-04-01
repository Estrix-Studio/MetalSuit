using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed;

    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Transform groundPoint;
    private Vector2 _moveInputValue;
    private Rigidbody _rb;

    PlayerMovement playerMovement;
    [SerializeField] public Animator Animator;
    public static readonly int IsWalking = Animator.StringToHash("isWalking");
    public static readonly int IsDashing = UnityEngine.Animator.StringToHash("isDashing");

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        Animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        
        Run();
    }
    
    // Walk/Run function that switch between walk and run animations
    private void Run()
    {
        var result = new Vector3(_moveInputValue.x, 0, _moveInputValue.y) * (speed * Time.fixedDeltaTime);
        result.y = _rb.velocity.y;
        _rb.velocity = result;

        Animator.SetBool(IsWalking, _moveInputValue.magnitude > 0);
    }
    

    private void OnMove_IA(InputValue value)
    {
        _moveInputValue = value.Get<Vector2>();
    }

    private void OnJump_IA()
    {
        if (Physics.CheckSphere(groundPoint.position, 0.1f, groundLayer))
            _rb.AddForce(Vector3.up * 300);
    }
}