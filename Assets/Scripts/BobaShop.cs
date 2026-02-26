using UnityEngine;

public class BobaShop : MonoBehaviour
{
    private bool alreadyCollected = false;
    
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") && !alreadyCollected && !BobaDeliveryManager.Instance.gameWon)
        {
            BobaDeliveryManager.Instance.CollectBoba();
            alreadyCollected = true;
            
            GetComponent<Renderer>().material.color = Color.gray;
        }
    }
}