using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.AI;

public class RobbiController : MonoBehaviour
{
    [SerializeField] private UIManager ui;
    [SerializeField] private Rigidbody rb;

    [SerializeField] private Transform frontWheels, midWheels, rearWheel;

    [SerializeField] private float acceleration;
    [SerializeField] private float reverseAcceleration;

    [SerializeField] private float speed;
    [SerializeField] private float rotate;

    [SerializeField] private float turnSpeed;
    [SerializeField] private float topSpeed;
    [SerializeField] private float reverseSpeed;
    [SerializeField] private float drag;
    [SerializeField] private float gravity;
    [SerializeField] private float wheelRotationSpeed = 500f;
    private bool isGrounded = true;
    public bool stunned;


    void Start()
    {
        Application.targetFrameRate = 120;
        rb.interpolation = RigidbodyInterpolation.Interpolate;
    }

    void Update()
    {
        speed = 0f;
        rotate = 0f;

        if ((ui == null || !ui.instructionActive()) && !stunned)
        {
            rotate = Input.GetAxis("Horizontal");
            speed = Input.GetAxis("Vertical");

            // Jump input
            if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
            {
                rb.linearVelocity = new Vector3(rb.linearVelocity.x, 5f, rb.linearVelocity.z);
                isGrounded = false;
            }
        }
    }


    void LateUpdate()
    {
        transform.position = rb.transform.position - new Vector3(0, 0.1f, 0f);
    }

    void FixedUpdate()
    {
        // Check ground first before physics calculations
        CheckGrounded();

        // Forward Speed
        if (speed > 0f && rb.linearVelocity.magnitude < topSpeed)
            rb.AddForce(transform.forward * speed * acceleration, ForceMode.Acceleration);

        // Reverse Speed
        else if (speed < 0f && rb.linearVelocity.magnitude < reverseSpeed)
            rb.AddForce(transform.forward * speed * reverseAcceleration, ForceMode.Acceleration);

        // Adding gravity
        rb.AddForce(Vector3.down * gravity, ForceMode.Acceleration);

        // Adding Rotation
        Quaternion rotation = Quaternion.Euler(0f, rotate * turnSpeed * Time.fixedDeltaTime, 0f);
        transform.rotation *= rotation;

        // Adding Drag
        Vector3 localVel = transform.InverseTransformDirection(rb.linearVelocity);

        // Reduce sideways sliding
        localVel.x *= drag;

        // Optional: small forward drag when not accelerating
        if (Mathf.Abs(speed) < 0.1f)
        {
            localVel.z *= 0.98f;
        }

        // Preserve Y velocity and apply modified XZ velocity
        Vector3 worldVel = transform.TransformDirection(localVel);
        rb.linearVelocity = new Vector3(worldVel.x, rb.linearVelocity.y, worldVel.z);


        // Wheel Rotation
        float direction = 0f;

        // Forward = clockwise, Reverse = counterclockwise
        if (speed > 0f)
            direction = 1f; // clockwise
        else if (speed < 0f)
            direction = -1f;  // counterclockwise

        float rotationAmount = direction * wheelRotationSpeed * Time.fixedDeltaTime;

        // Rotate all wheels
        frontWheels.Rotate(rotationAmount, 0f, 0f);
        midWheels.Rotate(rotationAmount, 0f, 0f);
        rearWheel.Rotate(rotationAmount, 0f, 0f);
    }

    private void CheckGrounded()
    {
        RaycastHit hit;
        float groundCheckDistance = 1.5f;
        
        // Use rigidbody position for raycast instead of transform position (which is offset)
        Vector3 rayStartPos = rb.transform.position;
        
        // Multiple raycasts for better ground detection
        bool centerHit = Physics.Raycast(rayStartPos, Vector3.down, out hit, groundCheckDistance);
        bool forwardHit = Physics.Raycast(rayStartPos + transform.forward * 0.3f, Vector3.down, out hit, groundCheckDistance);
        bool backwardHit = Physics.Raycast(rayStartPos - transform.forward * 0.3f, Vector3.down, out hit, groundCheckDistance);
        
        isGrounded = centerHit || forwardHit || backwardHit;
    }

    public void lockMovement()
    {
        stunned = true;
    }

    public void unlockMovement()
    {
        stunned = false;
    }


}