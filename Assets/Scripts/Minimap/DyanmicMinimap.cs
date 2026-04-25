using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AI;

public class DyanmicMinimap : MonoBehaviour
{
    private NavMeshPath path;

    [SerializeField] private LineRenderer lineRenderer;
    public float lineWidth = 5f;

    public GameObject target;
    public GameObject dest;

    private NavMeshQueryFilter filter;

    [SerializeField] private float time;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        path = new NavMeshPath();
        time = 0.0f;
    }

    void Awake()
    {
        lineRenderer.material.color = Color.cyan;

        lineRenderer.useWorldSpace = true;

        lineRenderer.startWidth = lineRenderer.endWidth = lineWidth;

        filter = new NavMeshQueryFilter();
        filter.agentTypeID = NavMesh.GetSettingsByIndex(0).agentTypeID;
        filter.areaMask = NavMesh.AllAreas;
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;

        //Debug.Log(NavMesh.agentTypeID);

        if (time > 0.5f)
        {
            time = 0.0f;
            
            NavMesh.CalculatePath(target.transform.position, 
                                    dest.transform.position, 
                                    filter,
                                    path);

        }
        if (path.status == NavMeshPathStatus.PathComplete) DrawRoute();
    }

    private void DrawRoute()
    {
        if (target == null) {
            lineRenderer.positionCount = 0;
            return;
        }

        lineRenderer.positionCount = path.corners.Length;

        lineRenderer.SetPosition(0, target.transform.position);
        Vector3 pos = target.transform.position;
        pos.y = 5f;
        lineRenderer.SetPosition(0, pos);
        for (int i = 1; i < path.corners.Length; i++)
        {
            path.corners[i].y = 5f;
            lineRenderer.SetPosition(i , path.corners[i]);
        }
    }
}
