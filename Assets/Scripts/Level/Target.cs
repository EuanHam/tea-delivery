using UnityEngine;
using UnityEngine.SceneManagement;

public class Target : MonoBehaviour
{
    BobaDeliveryManager bobaDeliveryManager = BobaDeliveryManager.Instance;

    private InstructionManager instructionManager;
    
    void Start()
    {
        instructionManager = FindObjectOfType<InstructionManager>();
        bobaDeliveryManager = BobaDeliveryManager.Instance;

        if (instructionManager == null)
        {
            Debug.LogError("InstructionManager not found in scene");
        }
    }
    
    void OnCollisionEnter(Collision collision)
    {
        Debug.Log($"Something entered: {collision.gameObject.name}, Tag: {collision.gameObject.tag}");
        
        // check if collision or parent has tag
        if (collision.gameObject.CompareTag("Player")) { 
            if (bobaDeliveryManager.hasBoba == false)
            {
                bobaDeliveryManager.UpdateHUD("You don't have a boba to deliver!");
                return;
            }
            if (bobaDeliveryManager.currentOrder != null)
            {
                Transform deliveryLocation = bobaDeliveryManager.currentOrder.deliveryLocation;
                if (Vector3.Distance(transform.position, deliveryLocation.position) > 5f)
                {
                    bobaDeliveryManager.UpdateHUD("This isn't the right house!");
                    // return;
                } else
                {
                    bobaDeliveryManager.DeliverBoba();
                    // return;
                }

                if (instructionManager != null)
                {
                    instructionManager.ShowCongratulations();
                    StartCoroutine(ReturnToLevelSelectionAfterDelay(5f));
                }
                else
                {
                    Debug.LogError("InstructionManager is null");
                }
            }
        }

    }
    
    private System.Collections.IEnumerator ReturnToLevelSelectionAfterDelay(float delaySeconds)
    {
        yield return new WaitForSeconds(delaySeconds);
        SceneManager.LoadScene("LevelSelection");
    }
}