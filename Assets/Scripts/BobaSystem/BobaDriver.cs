using UnityEngine;
using TMPro;
public class BobaDriver : MonoBehaviour
{
    public NewOrder load;
    public int balance;
    public int ordersCompleted;
    public int specialOrdersCompleted;
    public int npcsHit;
    public int vehicleCollisions;

    [SerializeField] private DyanmicMinimap minimap;
    [SerializeField] private GameObject BobaShop;
    private int collisions;

    void Start()
    {
        balance = 0;
        collisions = 0;
        ordersCompleted = 0;
        specialOrdersCompleted = 0;
        npcsHit = 0;
        vehicleCollisions = 0;

        // sanity check
        if (BobaShop == null)
        {
            BobaShop = GameObject.Find("BobaShop");
            if (BobaShop == null)
            {
                Debug.LogWarning("Cannot find boba shop.");
            }
        }
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
