using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AI;

public class DyanmicMinimap : MonoBehaviour
{
    private NavMeshPath path;

    [SerializeField] private LineRenderer lr;
    public float lineWidth = 5f;

    public Transform src;
    public Transform dest;

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
    }

    // Update is called once per frame
    async Task Update()
    {
        time += Time.deltaTime;

        if (time > 0.5f)
        {
            time = 0.0f;

            NavMesh.CalculatePath(src.position, dest.position, NavMesh.AllAreas, path);
        }
        if (path.status == NavMeshPathStatus.PathComplete) DrawRoute();
    }

    private void DrawRoute()
    {
        if (src == null || dest == null) return;
        
        lr.positionCount = path.corners.Length;
        lr.SetPosition(0, src.position);

        for (int i = 1; i < path.corners.Length; i++)
        {
            lr.SetPosition(i , path.corners[i]);
        }
    }
}
