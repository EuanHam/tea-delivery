using UnityEngine;

public class House : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            BobaDeliveryManager.Instance.DeliverBoba();
        }
    }
}