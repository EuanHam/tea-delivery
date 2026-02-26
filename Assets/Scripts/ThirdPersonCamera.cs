using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private Vector3 offset = new Vector3(0, 4, -5);
    [SerializeField] private float smoothSpeed = 5f;
    [SerializeField] private float viewTargetHeight = 3f; 
    
    void LateUpdate()
    {
        if (target == null) return;
        
        Vector3 desiredPosition = target.position + target.TransformDirection(offset);
        transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
        
        Vector3 lookAtPoint = target.position + Vector3.up * viewTargetHeight;
        transform.LookAt(lookAtPoint);
    }
}