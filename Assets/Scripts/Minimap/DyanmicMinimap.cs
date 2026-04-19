using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AI;

public class DyanmicMinimap : MonoBehaviour
{
    private NavMeshPath path;

    [SerializeField] private LineRenderer lr;
    public float lineWidth = 5f;

    public GameObject src;
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
        lr.useWorldSpace = true;

        lr.startWidth = lr.endWidth = lineWidth;

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
            
            NavMesh.CalculatePath(src.transform.position, 
                                    dest.transform.position, 
                                    filter,
                                    path);

        }

        if (path.status == NavMeshPathStatus.PathComplete) DrawRoute();
    }

    private void DrawRoute()
    {
        if (src == null) {
            lr.positionCount = 0;
            return;
        }

        lr.positionCount = path.corners.Length;
        lr.SetPosition(0, src.transform.position);

        for (int i = 1; i < path.corners.Length; i++)
        {
            lr.SetPosition(i , path.corners[i]);
        }
    }
}
