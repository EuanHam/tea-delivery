using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class npcAI : MonoBehaviour
{

    public Animator anim;
    public NavMeshAgent nma;

    public GameObject[] waypoints;

    private int currWaypoint;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currWaypoint = -1;
        nma = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetFloat("vely", nma.velocity.magnitude / nma.speed);
        if (nma.remainingDistance < 0.1f && !nma.pathPending) setNextWaypoint();
    }

    private void setNextWaypoint() 
    {
        currWaypoint += 1;

        if (waypoints.Length <= currWaypoint)
        {
            currWaypoint = 0;
        }

        nma.SetDestination(waypoints[currWaypoint].transform.position);
    }
}
