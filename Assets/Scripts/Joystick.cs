using UnityEngine;
using UnityEngine.InputSystem;

public class Joystick : MonoBehaviour
{
    [SerializeField] private InputActionReference joystickAction;
    [SerializeField] private RectTransform joystickHandle;

    private Vector2 inputDirection;

    // Adjust this value to control the joystick's sensitivity
    [SerializeField] private float joystickRadius = 50f;

    private void OnEnable()
    {
        Debug.Log("g");
        joystickAction.action.Enable();
        joystickAction.action.performed += OnJoystickPerformed;
        joystickAction.action.canceled += OnJoystickCanceled;
    }

    private void OnDisable()
    {
        joystickAction.action.Disable();
        joystickAction.action.performed -= OnJoystickPerformed;
        joystickAction.action.canceled -= OnJoystickCanceled;
    }

    private void OnJoystickPerformed(InputAction.CallbackContext context)
    {
        inputDirection = context.ReadValue<Vector2>();
        UpdateJoystickHandle();
    }

    private void OnJoystickCanceled(InputAction.CallbackContext context)
    {
        inputDirection = Vector2.zero;
        UpdateJoystickHandle();
    }

    private void UpdateJoystickHandle()
    {
        Debug.Log("g");
        Vector2 handlePosition = inputDirection * joystickRadius;
        joystickHandle.anchoredPosition = handlePosition;
    }

    public Vector2 GetInputDirection()
    {
        return inputDirection.normalized;
    }
}
