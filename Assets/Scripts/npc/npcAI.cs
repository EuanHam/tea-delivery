using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

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
        if (waypoints != null && waypoints.Length > 0)
        {
            currWaypoint = Random.Range(0, waypoints.Length);
            nma.SetDestination(waypoints[currWaypoint].transform.position);
        }

        //Debug.Log(nma.agentTypeID);
    }

    // Update is called once per frame
    void Update()
    {
        // npcs don't move in tutorial
        if (SceneManager.GetActiveScene().name == "Level0Tutorial")
        {
            nma.isStopped = true;
            return;
        }

        if (anim != null && nma != null && nma.enabled && nma.isOnNavMesh)
            anim.SetFloat("vely", nma.velocity.magnitude / nma.speed);
        

        if (waypoints == null || waypoints.Length == 0)
            return;

        if (nma == null || !nma.enabled || !nma.isOnNavMesh)
            return;

        if (!nma.pathPending && nma.remainingDistance <= 0.75f)
            setNextWaypoint();
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

    public void stopAgent(bool cond)
    {
        nma.isStopped = cond;
    }
}
