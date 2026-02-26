using UnityEngine;
using UnityEngine.InputSystem;

public class MopedControl : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 40f;
    [SerializeField] private float turnSpeed = 100f;
    
    private Rigidbody rb;
    private float moveInput = 0f;
    private float turnInput = 0f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        HandleInput();
    }

    void FixedUpdate()
    {
        ApplyMovement();
        ApplyRotation();
    }

    private void HandleInput()
    {
        if (Keyboard.current == null) return;

        moveInput = 0f;
        turnInput = 0f;

        if (Keyboard.current.wKey.isPressed)
        {
            moveInput = 1f;
        }
        
        if (Keyboard.current.sKey.isPressed)
        {
            moveInput = -1f;
        }

        if (Keyboard.current.aKey.isPressed)
        {
            turnInput = -1f;
        }
        
        if (Keyboard.current.dKey.isPressed)
        {
            turnInput = 1f;
        }
    }

    private void ApplyMovement()
    {
        if (rb != null && moveInput != 0f)
        {
            Vector3 moveForce = transform.forward * moveInput * moveSpeed;
            rb.AddForce(moveForce, ForceMode.Acceleration);
        }
    }

    private void ApplyRotation()
    {
        if (turnInput != 0f)
        {
            transform.Rotate(0, turnInput * turnSpeed * Time.fixedDeltaTime, 0);
        }
    }
}
