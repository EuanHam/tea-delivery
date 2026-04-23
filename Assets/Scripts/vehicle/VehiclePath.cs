using UnityEngine;
using UnityEngine.Splines;
using Unity.Mathematics;

public class VehiclePath : MonoBehaviour
{
    [SerializeField] private SplineContainer splineContainer;
    [SerializeField] private float speed = 2f;
    [SerializeField] private bool loop = true;


    [SerializeField] private float checkDistance = 1.5f;
    [SerializeField] private float sphereRadius = 0.3f;
    [SerializeField] private LayerMask obstacleMask; 
    [SerializeField] private LayerMask crossingMask; 

    [SerializeField] private float heightOffset = 0.5f;

    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip honk;

    private bool isBlocked;

    private float t;

    void Update()
    {
        if (splineContainer == null)
            return;

        float3 tangent = splineContainer.EvaluateTangent(t);
        Vector3 forward = ((Vector3)tangent).normalized;

        if (forward.sqrMagnitude < 0.0001f)
            return;

        Vector3 castOrigin = transform.position + Vector3.up * heightOffset;


        bool blocked = Physics.SphereCast(
            castOrigin,
            sphereRadius,
            forward,
            out RaycastHit hit,
            checkDistance,
            obstacleMask,
            QueryTriggerInteraction.Ignore
        );
        if (blocked)
        {

            if (Physics.Raycast(
            hit.point,
            Vector3.down,
            out RaycastHit groundHit,
            5f,
            crossingMask,
            QueryTriggerInteraction.Ignore))
            {
                if (!isBlocked)
                {
                    StartHonking();
                    isBlocked = true;
                }
                return;
            }
        }
        else
        {
            if (isBlocked)
            {
                StopHonking();
                isBlocked = false;
            }
        }



        float splineLength = splineContainer.CalculateLength();
        t += speed * Time.deltaTime / splineLength;

        if (loop)
            t %= 1f;
        else
            t = Mathf.Clamp01(t);

        float3 position = splineContainer.EvaluatePosition(t);
        float3 newTangent = splineContainer.EvaluateTangent(t);

        transform.position = position;

        if (math.lengthsq(newTangent) > 0.0001f)
            transform.rotation = Quaternion.LookRotation((Vector3)newTangent, Vector3.up);
    }

    void StartHonking()
    {
        if (audioSource != null && honk != null)
        {
            audioSource.clip = honk;
            audioSource.loop = true;
            audioSource.Play();
        }
    }

    void StopHonking()
    {
        if (audioSource != null && audioSource.isPlaying)
        {
            audioSource.Stop();
        }
    }
}
