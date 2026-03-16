using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class MinimapRouteDrawer : MonoBehaviour
{
    public Transform robot;
    public Transform destination;
    public IntersectionNode[] allNodes;

    public float heightOffset = 100f;
    public float recomputeInterval = 0.3f;
    public float lineWidth = 12f;
    public float nodeReachDistance = 15f;

    private IntersectionNode lastPassedNode;

    private LineRenderer lr;
    private float timer = 0f;
    private List<IntersectionNode> currentPath = new List<IntersectionNode>();

    void Awake()
    {
        lr = GetComponent<LineRenderer>();
        lr.useWorldSpace = true;

        lr.startWidth = lineWidth;
        lr.endWidth = lineWidth;

        // Constant width along whole line
        lr.widthCurve = AnimationCurve.Linear(0f, 1f, 1f, 1f);
    }

    void Update()
    {
        UpdatePassedNode();

        timer += Time.deltaTime;

        if (timer >= recomputeInterval)
        {
            timer = 0f;
            RecalculatePath();
        }

        DrawRoute();
    }

    void UpdatePassedNode()
    {
        if (robot == null || currentPath == null || currentPath.Count == 0)
            return;

        Vector3 robotFlat = robot.position;
        robotFlat.y = 0f;

        Vector3 firstNodeFlat = currentPath[0].transform.position;
        firstNodeFlat.y = 0f;

        float dist = Vector3.Distance(robotFlat, firstNodeFlat);

        if (dist <= nodeReachDistance)
        {
            lastPassedNode = currentPath[0];
            currentPath.RemoveAt(0);
        }
    }

    void RecalculatePath()
    {
        if (robot == null || destination == null || allNodes == null || allNodes.Length == 0)
        {
            Debug.Log("Missing robot, destination, or allNodes");
            return;
        }

        IntersectionNode startNode = FindNearestNode(robot.position);
        IntersectionNode goalNode = FindNearestNode(destination.position);

        List<IntersectionNode> newPath = AStarPathfinder.FindPath(startNode, goalNode);

        if (newPath == null || newPath.Count == 0)
        {
            Debug.Log("A* returned null or empty path");
            return;
        }

        Debug.Log("Path found with " + newPath.Count + " nodes");

        currentPath = newPath;
    }

    void DrawRoute()
    {
        if (robot == null || currentPath == null || currentPath.Count == 0)
        {
            lr.positionCount = 0;
            return;
        }

        lr.startWidth = lineWidth;
        lr.endWidth = lineWidth;

        lr.positionCount = currentPath.Count + 1;

        Vector3 robotPos = robot.position;
        robotPos.y = heightOffset;
        lr.SetPosition(0, robotPos);

        for (int i = 0; i < currentPath.Count; i++)
        {
            Vector3 p = currentPath[i].transform.position;
            p.y = heightOffset;
            lr.SetPosition(i + 1, p);
        }
    }

    IntersectionNode FindNearestNode(Vector3 position)
    {
        IntersectionNode nearest = null;
        float bestDist = Mathf.Infinity;

        foreach (IntersectionNode node in allNodes)
        {
            if (node == null) continue;

            if (robot.position.z > node.transform.position.z && Mathf.Abs(robot.position.x - node.transform.position.x) < 10f)
                continue;

            float dist = Vector3.Distance(position, node.transform.position);
            if (dist < bestDist)
            {
                bestDist = dist;
                nearest = node;
            }
        }

        return nearest;
    }
}