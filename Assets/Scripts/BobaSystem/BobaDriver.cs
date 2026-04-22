using UnityEngine;
using TMPro;
public class BobaDriver : MonoBehaviour
{
    public NewOrder load;
    public int balance;
    public int ordersCompleted;
    public int npcsHit;

    [SerializeField] private DyanmicMinimap minimap;
    [SerializeField] private GameObject BobaShop;
    private int collisions;

    void Start()
    {
        balance = 350;
        collisions = 0;
        ordersCompleted = 4;
        npcsHit = 5;
    }

    // Update is called once per frame
    void Update()
    {
        if (load != null)
        {
            minimap.dest = load.customer;
            load.ttl -= Time.deltaTime;
            
            if (load.ttl <= 0) {
                minimap.dest = BobaShop;
                load = null;
            }
        } else
        {
            minimap.dest = BobaShop;
        }
    }
}
