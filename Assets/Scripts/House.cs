using UnityEngine;

public class House : MonoBehaviour
{
    BobaDeliveryManager bobaDeliveryManager = BobaDeliveryManager.Instance;
    
    void Start()
    {
        bobaDeliveryManager = BobaDeliveryManager.Instance;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (bobaDeliveryManager.hasBoba == false)
            {
                bobaDeliveryManager.UpdateHUD("You don't have a boba to deliver!");
                return;
            }
            if (bobaDeliveryManager.currentOrder != null)
            {
                Transform deliveryLocation = bobaDeliveryManager.currentOrder.deliveryLocation;
                if (Vector3.Distance(transform.position, deliveryLocation.position) > 2f)
                {
                    bobaDeliveryManager.UpdateHUD("This isn't the right house!");
                    return;
                } else
                {
                    bobaDeliveryManager.DeliverBoba();
                    return;
                }
            }
        }
    }
}