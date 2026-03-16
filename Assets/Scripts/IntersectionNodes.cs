using System.Collections.Generic;
using UnityEngine;

public class IntersectionNode : MonoBehaviour
{
    public List<IntersectionNode> neighbors = new List<IntersectionNode>();

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawSphere(transform.position, 2f);

        Gizmos.color = Color.green;
        foreach (var neighbor in neighbors)
        {
            if (neighbor != null)
            {
                Gizmos.DrawLine(transform.position, neighbor.transform.position);
            }
        }
    }
}