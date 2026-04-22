using UnityEngine;
using UnityEngine.AI;
public class PowerUpManager : MonoBehaviour
{
    [SerializeField] private LevelManager levelManager;
    [SerializeField] private GameObject prefab;
    [SerializeField] private float lo;
    [SerializeField] private float hi;
    [SerializeField] private Transform player;
    private float time;
    public float invulnerableTimer;
    public float doubleMoneyTimer;

    void Start()
    {
        time = Random.Range(lo, hi);
    }

    // Update is called once per frame
    void Update()
    {
        time -= Time.deltaTime;

        Vector3 pos = Random.insideUnitSphere * 30f + player.position;
        
        NavMeshHit hit;

        if (time <= 0 && NavMesh.SamplePosition(pos, out hit, 30f, NavMesh.AllAreas))
        {
            GameObject powerUp = Instantiate(prefab, hit.position + Vector3.up * 0.5f, Quaternion.Euler(90f, 0f, 0f), transform);
            powerUp.transform.localScale = Vector3.one * 2f;
            powerUp.GetComponent<PowerUpCollision>().powerUpManager = this;
            time = Random.Range(lo, hi);
        } 

        if (doubleMoneyTimer > 0f)
        {
            doubleMoneyTimer -= Time.deltaTime;
        }

        if (invulnerableTimer > 0f)
        {
            invulnerableTimer -= Time.deltaTime;;
        }
    }

    public void setDoubleMoney()
    {
        doubleMoneyTimer = 45f;
    }
    public void setInvunerable()
    {
        invulnerableTimer = 45f;
    }

    public void extendTime()
    {
        levelManager.time += 45f;
    }

    public bool isDoubleMoney()
    {
        return doubleMoneyTimer > 0f;
    }

    public bool isInvunerable()
    {
        return invulnerableTimer > 0f;
    }
}
