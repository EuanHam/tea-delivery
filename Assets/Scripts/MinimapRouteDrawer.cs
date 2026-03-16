using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class MinimapRouteDrawer : MonoBehaviour
{
    public Transform robot;
    public Transform[] waypoints;
    public float heightOffset;
    public float reachDistance;

    private LineRenderer lr;

    private int currentWaypointIndex = 0;


    void Awake()
    {
        lr = GetComponent<LineRenderer>();
        lr.startWidth = 6f;
        lr.endWidth = 6f;
    }

    void Update()
    {
        UpdateWaypointProgress();
        DrawRoute();
    }

    void UpdateWaypointProgress()
    {
        if (robot == null || waypoints == null || currentWaypointIndex >= waypoints.Length)
            return;

        Vector3 robotFlat = robot.position;
        Vector3 waypointFlat = waypoints[currentWaypointIndex].position;

        robotFlat.y = 0f;
        waypointFlat.y = 0f;

        if (Vector3.Distance(robotFlat, waypointFlat) <= reachDistance)
        {
            currentWaypointIndex++;
        }
    }

    void DrawRoute()
    {
        if (robot == null || waypoints == null || currentWaypointIndex >= waypoints.Length)
        {
            lr.positionCount = 0;
            return;
        }

        int remaining = waypoints.Length - currentWaypointIndex;

        // robot position + remaining waypoints
        lr.positionCount = remaining + 1;

        Vector3 robotPos = robot.position;
        robotPos.y += heightOffset;
        lr.SetPosition(0, robotPos);

        for (int i = 0; i < remaining; i++)
        {
            Vector3 p = waypoints[currentWaypointIndex + i].position;
            p.y += heightOffset;
            lr.SetPosition(i + 1, p);
        }
    }
}