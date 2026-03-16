using UnityEngine;
using TMPro;

public class UIScript : MonoBehaviour
{
    public BobaDeliveryManager bobaDeliveryManager;
    public GameObject robbi;
    
    public TMP_Text bobaTypeText;
    public TMP_Text customerNameText;
    public TMP_Text latitudeText;
    public TMP_Text longitudeText;
    public TMP_Text timerText;

    public TMP_Text hudText;

    public TMP_Text locationText;

    void Start()
    {
        bobaDeliveryManager = BobaDeliveryManager.Instance;
        robbi = GameObject.FindWithTag("Player");
    }
    
    // Update is called once per frame
    void Update()
    {
        Order currentOrder = bobaDeliveryManager.currentOrder;
        if (currentOrder == null) return;

        customerNameText.text = currentOrder.customer.name;
        bobaTypeText.text = currentOrder.bobaType.ToString();
        latitudeText.text = "Latitude: " + currentOrder.deliveryLocation.position.z.ToString("F2");
        longitudeText.text = "Longitude: " + currentOrder.deliveryLocation.position.x.ToString("F2");
        timerText.text = "Time Left: " + bobaDeliveryManager.timeLeft.ToString("F1");
        locationText.text = "Latitude: " + robbi.transform.position.z.ToString("F0") + " | Longitude: " + robbi.transform.position.x.ToString("F0");
    }

    public void UpdateHUD(string message)
    {
        if (hudText != null)
            hudText.text = message;
    }


}
