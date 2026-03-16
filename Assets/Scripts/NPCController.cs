using UnityEngine;
using UnityEngine.AI;

public class NPCController : MonoBehaviour
{
    public GameObject prefab, center;
    public int size;
    public GameObject[] poi;
    private GameObject[] npcs;
    private Animator[] anims;
    private NavMeshAgent[] nmas;

    private enum mood
    {
        RUSHED,
        SLUGISH,
        NORMAL
    }

    private mood[] moods;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        npcs = new GameObject[size];
        anims = new Animator[size];
        nmas = new NavMeshAgent[size];
        moods = new mood[size];

        for (int i = 0; i < size; i++)
        {
            Vector3 pos = Random.insideUnitSphere * 250f + center.transform.position;
            
            NavMeshHit hit;
            if (NavMesh.SamplePosition(pos, out hit, 1000f, NavMesh.AllAreas))
            {
                npcs[i] = Instantiate(prefab, hit.position, Quaternion.identity);
                npcAI nAI = npcs[i].GetComponent<npcAI>();

                nAI.waypoints = poi;

                anims[i] = npcs[i].GetComponent<Animator>();
                nmas[i] = npcs[i].GetComponent<NavMeshAgent>();

                switch (Random.Range(0, 3))
                {
                    case 0:
                        npcs[i].name = "Rushed NPC";
                        moods[i] = mood.RUSHED;
                        anims[i].speed = 1.25f;
                        nmas[i].speed = 4.0f;
                        break;
                    case 1:
                        npcs[i].name = "Slugish NPC";
                        moods[i] = mood.SLUGISH;
                        anims[i].speed = 0.75f;
                        nmas[i].speed = 2.625f;
                        break;
                    default:
                        npcs[i].name = "Normal NPC";
                        moods[i] = mood.NORMAL;
                        break;
                }
            }
            else
            {
                Debug.LogWarning($"Could not find NavMesh position for NPC {i}");
            }
        }

        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
