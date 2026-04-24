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
    [SerializeField] private float honkDuration = 2f;

    private bool isHonking = false;
    private bool isBlocked;
    private float t;

    void Update()
    {

        if (audioSource != null)
            audioSource.transform.position = transform.position;

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
                    isBlocked = true;
                    //StartHonking(); // Only triggers if not already honking
                }
                return;
            }
        }
        else
        {
            // Clear the blocked flag — vehicle will resume after honk finishes naturally
            isBlocked = false;
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
        // isHonking guard prevents re-triggering mid-honk
        if (!isHonking && audioSource != null && honk != null)
        {
            StartCoroutine(HonkRoutine());
        }
    }

    System.Collections.IEnumerator HonkRoutine()
    {
        isHonking = true;
        audioSource.clip = honk;
        audioSource.loop = false;

        float elapsed = 0f;

        // Keep replaying the clip until honkDuration is fully spent
        while (elapsed < honkDuration)
        {
            audioSource.Play();
            float clipRemaining = honk.length;
            float timeRemaining = honkDuration - elapsed;

            // Wait for whichever is shorter: the clip or remaining duration
            float waitTime = Mathf.Min(clipRemaining, timeRemaining);
            yield return new WaitForSeconds(waitTime);

            elapsed += waitTime;
        }

        audioSource.Stop();
        isHonking = false;
    }
}