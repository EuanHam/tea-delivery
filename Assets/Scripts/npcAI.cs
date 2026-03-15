using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class npcAI : MonoBehaviour
{
    public Animator anim;
    public NavMeshAgent nma;

    public GameObject[] waypoints;

    public int currWaypoint;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currWaypoint = Random.Range(0, waypoints.Length);
        nma = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetFloat("vely", nma.velocity.magnitude / nma.speed);
        if (waypoints.Length != 0 && nma.remainingDistance < 1f && !nma.pathPending) setNextWaypoint();
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
