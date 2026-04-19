using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.AI;

public class RobbiController : MonoBehaviour
{
    public bool isDrifting;
    public Rigidbody rb, sphere;

    public Transform frontWheels, rearWheel;
    public float acceleration;
    public float reverseAcceleration;

    public float currentSpeed, speed;
    public float currentRotate, rotate;

    public float turnSpeed;

    public float topSpeed;

    public float reverseSpeed;

    public float drag;

    public float gravity;



    private float Drag = 0.95f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Application.targetFrameRate = 120;
        sphere.interpolation = RigidbodyInterpolation.Interpolate;

    }
    void Update()
    {
        speed = 0f;
        rotate = 0f;
        
        if (Keyboard.current.wKey.isPressed) speed = 1f;
        if (Keyboard.current.sKey.isPressed) speed = -1f;
        if (Keyboard.current.aKey.isPressed) rotate = -1f;
        if (Keyboard.current.dKey.isPressed) rotate = 1f;
    }

    void LateUpdate()
    {
        transform.position = sphere.transform.position - new Vector3(0, 0.1f, 0f);
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        // Forward Speed
        if (speed > 0f && sphere.linearVelocity.magnitude < topSpeed)
            sphere.AddForce(transform.forward * speed * acceleration, ForceMode.Acceleration);

        // Reverse Speed
        else if (speed < 0f && sphere.linearVelocity.magnitude < reverseSpeed)
            sphere.AddForce(transform.forward * speed * reverseAcceleration, ForceMode.Acceleration);

        // Adding gravity
        sphere.AddForce(Vector3.down * gravity , ForceMode.Acceleration);

        // Adding Rotation
        Quaternion rotation = Quaternion.Euler(0f, rotate * turnSpeed * Time.fixedDeltaTime, 0f);
        transform.rotation *= rotation;

        // Adding Drag
        Vector3 localVel = transform.InverseTransformDirection(sphere.linearVelocity);

        // reduce sideways sliding
        localVel.x *= drag;

        // optional: small forward drag when not accelerating
        if (Mathf.Abs(speed) < 0.1f)
        {
            localVel.z *= 0.98f;
        }

        sphere.linearVelocity = transform.TransformDirection(localVel);
                
    }
}
