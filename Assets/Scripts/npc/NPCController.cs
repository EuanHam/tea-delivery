using UnityEngine;
using UnityEngine.AI;

public class NPCController : MonoBehaviour
{
    public GameObject prefab, center;
    public Material[] mat;
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

            NavMeshQueryFilter filter = new NavMeshQueryFilter();
            filter.agentTypeID = NavMesh.GetSettingsByIndex(1).agentTypeID;
            filter.areaMask = NavMesh.AllAreas;

            if (NavMesh.SamplePosition(pos, out hit, 1000f, filter))
            {
                npcs[i] = Instantiate(prefab, hit.position, Quaternion.identity, transform);


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


                foreach (Transform child in npcs[i].transform)
                {
                    Renderer rend = child.GetComponent<Renderer>();
                    if (rend != null)
                    {
                        if (moods[i] == mood.RUSHED) {
                            rend.material = mat[0];
                        } else if (moods[i] == mood.NORMAL) {
                            rend.material = mat[1];
                        } else {
                            rend.material = mat[2];
                        }
                        
                    }
                }

            }
            else
            {
                Debug.LogWarning($"Could not find NavMesh position for NPC {i}");
            }
        }

        
    }

    public GameObject getRandomNPC()
    {
        GameObject npc = npcs[Random.Range(0, size)];

        while (npc == null) npc = npcs[Random.Range(0, size)];

        return npc;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
