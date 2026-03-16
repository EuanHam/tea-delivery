using System.Collections.Generic;
using UnityEngine;

public class IntersectionNode : MonoBehaviour
{
    // Store each nodes neighbors
    public List<IntersectionNode> neighbors = new List<IntersectionNode>();

    // Draw visualization of the node graph
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