
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private Vector2 moveInput;
    private Vector2 lookInput;
    private float xRotation = 0f;

    public Rigidbody rb;
    public Transform cameraTransform;

    public float moveSpeed = 5f;
    public float lookSpeed = 0.1f;
    public float gravityStrength = 20f;
    public float rotateSpeed = 5f;

    public float groundCheckDistance = 1.1f;

    private Vector3 gravityDirection = Vector3.down;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        rb.useGravity = false;
    }

    private void FixedUpdate()
    {
        rb.AddForce(gravityDirection * gravityStrength, ForceMode.Acceleration);
        Vector3 movement = new Vector3(moveInput.x, 0f, moveInput.y);
        movement = transform.TransformDirection(movement);
        movement = Vector3.ProjectOnPlane(movement, gravityDirection).normalized;
        Vector3 gravityVelocity = Vector3.Project(rb.velocity, gravityDirection);
        rb.velocity = movement * moveSpeed + gravityVelocity;
        Vector3 upDirection = -gravityDirection;
        Vector3 forwardDirection = Vector3.ProjectOnPlane(
            transform.forward, gravityDirection
        ).normalized;

        if (forwardDirection.sqrMagnitude > 0.01f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(
                forwardDirection, upDirection
            );

            rb.MoveRotation(targetRotation);
        }
    }
    private void Update()
    {
        float yRotation = lookInput.x * lookSpeed;
        transform.Rotate(0f, yRotation, 0f);
        xRotation -= lookInput.y * lookSpeed;
        xRotation = Mathf.Clamp(xRotation, -80f, 80f);

        cameraTransform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
    }

    public void GetVector(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    public void GetLook(InputAction.CallbackContext context)
    {
        lookInput = context.ReadValue<Vector2>();
    }

    public void Interact(InputAction.CallbackContext context)
    {
        if (context.performed && IsGrounded())
        {
            ChangeGravity();
        }
    }

    private void ChangeGravity()
    {
        if (gravityDirection == Vector3.down)
            gravityDirection = Vector3.left;
        else if (gravityDirection == Vector3.left)
            gravityDirection = Vector3.up;
        else if (gravityDirection == Vector3.up)
            gravityDirection = Vector3.right;
        else
            gravityDirection = Vector3.down;
        rb.velocity = Vector3.zero;
    }

    private bool IsGrounded()
    {
        RaycastHit[] hits = Physics.RaycastAll(transform.position,gravityDirection, groundCheckDistance);

        foreach (RaycastHit hit in hits)
        {
            if (!hit.collider.CompareTag("Player"))
            {
                return true;
            }
        }

        return false;
    }
}