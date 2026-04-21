using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.AI;

public class RobbiController : MonoBehaviour
{
    [SerializeField] private UIManager ui;
    [SerializeField] private bool isDrifting;
    [SerializeField] private Rigidbody rb, sphere;

    [SerializeField] private Transform frontWheels, rearWheel;

    [SerializeField] private float acceleration;
    [SerializeField] private float reverseAcceleration;

    [SerializeField] private float speed;
    [SerializeField] private float rotate;

    [SerializeField] private float turnSpeed;
    [SerializeField] private float topSpeed;
    [SerializeField] private float reverseSpeed;
    [SerializeField] private float drag;
    [SerializeField] private float gravity;
    [SerializeField] private LayerMask layerMask;
    private float Drag = 0.95f;

    void Start()
    {
        Application.targetFrameRate = 120;
        sphere.interpolation = RigidbodyInterpolation.Interpolate;
    }

    void Update()
    {
        speed = 0f;
        rotate = 0f;

        if (ui == null || !ui.instructionActive())
        {
            if (Keyboard.current.wKey.isPressed) speed = 1f;
            if (Keyboard.current.sKey.isPressed) speed = -1f;
            if (Keyboard.current.aKey.isPressed) rotate = -1f;
            if (Keyboard.current.dKey.isPressed) rotate = 1f;
        }
    }

    void LateUpdate()
    {
        transform.position = sphere.transform.position - new Vector3(0, 0.1f, 0f);
    }

    void FixedUpdate()
    {
        // Forward Speed
        if (speed > 0f && sphere.linearVelocity.magnitude < topSpeed)
            sphere.AddForce(transform.forward * speed * acceleration, ForceMode.Acceleration);

        // Reverse Speed
        else if (speed < 0f && sphere.linearVelocity.magnitude < reverseSpeed)
            sphere.AddForce(transform.forward * speed * reverseAcceleration, ForceMode.Acceleration);

        // Adding gravity
        sphere.AddForce(Vector3.down * gravity, ForceMode.Acceleration);

        // Adding Rotation
        Quaternion rotation = Quaternion.Euler(0f, rotate * turnSpeed * Time.fixedDeltaTime, 0f);
        transform.rotation *= rotation;

        // Adding Drag
        Vector3 localVel = transform.InverseTransformDirection(sphere.linearVelocity);

        // Reduce sideways sliding
        localVel.x *= drag;

        // Optional: small forward drag when not accelerating
        if (Mathf.Abs(speed) < 0.1f)
        {
            localVel.z *= 0.98f;
        }

        RaycastHit fl, fr, bl, br;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out fl, Mathf.Infinity, layerMask)) 
        {
            Debug.Log("Ray Hit");
        }

        sphere.linearVelocity = transform.TransformDirection(localVel);
    }

}