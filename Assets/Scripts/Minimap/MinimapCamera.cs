using UnityEngine;

public class Minimap : MonoBehaviour
{
    public Transform target;

    void LateUpdate()
    {
        // Have camera follow the player around
        Vector3 newPosition = target.position;
        newPosition.y = transform.position.y;
        transform.position = newPosition;

        transform.rotation = Quaternion.Euler(90f, target.eulerAngles.y, 0f);
    }
}
