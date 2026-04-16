using UnityEngine;

public class NewOrder
{
    public GameObject customer;
    public float ttl;

    public float low, upper;

    public NewOrder(GameObject customer, float low, float upper)
    {
        this.customer = customer;
        this.ttl = Random.Range(low, upper);
    }
}
