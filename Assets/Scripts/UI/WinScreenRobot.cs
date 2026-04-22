using UnityEngine;

public class WinScreenRobot : MonoBehaviour
{
    [SerializeField] private Transform[] wheels;
    [SerializeField] private float startDriveSpeed = 5f;
    [SerializeField] private float driveOffSpeed = 10f;
    [SerializeField] private float startWheelRotateSpeed = 500f;
    [SerializeField] private float driveOffWheelRotateSpeed = 1000f;
    [SerializeField] private Vector3 startOffset = new Vector3(0f, 0f, 2f);
    [SerializeField] private Vector3 finalOffset = new Vector3(0f, 0f, 10f);

    private Vector3 firstPosition;
    private Vector3 finalPosition;
    private bool drivingIntro = false;
    private bool drivingOutro = false;

    private Vector3 basePosition;
    private bool hasArrived = false;

    void Start()
    {
        firstPosition = transform.position;
        transform.position = firstPosition - startOffset;
        finalPosition = firstPosition - finalOffset;
    }

    public void DriveIn()
    {
        drivingIntro = true;
    }

    public void DriveOff() 
    {
        drivingOutro = true;
    }

    void Update()
    {

        if (!drivingIntro && !drivingOutro)
        {
            if (hasArrived)
            {
                transform.position = basePosition + new Vector3(0f, Mathf.Sin(Time.unscaledTime * 10f) * 0.01f, 0f); 
                foreach (Transform wheel in wheels)
                {
                    wheel.Rotate(Vector3.right, startWheelRotateSpeed * Time.unscaledDeltaTime, Space.World);
                }
            }
            return;
        }

        if (drivingIntro) {
            transform.position = Vector3.MoveTowards(
                transform.position,
                firstPosition,
                startDriveSpeed * Time.unscaledDeltaTime
            );

            foreach (Transform wheel in wheels)
            {
                wheel.Rotate(Vector3.right, startWheelRotateSpeed * Time.unscaledDeltaTime, Space.World);
            }
        }

        if (drivingOutro) {
            transform.position = Vector3.MoveTowards(
                transform.position,
                finalPosition,
                driveOffSpeed * Time.unscaledDeltaTime
            );
            foreach (Transform wheel in wheels)
            {
                wheel.Rotate(Vector3.right, driveOffWheelRotateSpeed * Time.unscaledDeltaTime, Space.World);
            }
        }

        if (transform.position == firstPosition)
        {
            drivingIntro = false;
            basePosition = firstPosition;
             hasArrived = true;
        }

        if (transform.position == finalPosition)
        {
            drivingOutro = false;
        }
    }
}