using UnityEngine;

public class Target : MonoBehaviour
{
    private InstructionManager instructionManager;
    
    void Start()
    {
        // Find the InstructionManager in the scene
        instructionManager = FindObjectOfType<InstructionManager>();
        
        if (instructionManager == null)
        {
            Debug.LogError("InstructionManager not found in the scene!");
        }
    }
    
    void OnTriggerEnter(Collider other)
    {
        Debug.Log($"Something entered: {other.gameObject.name}, Tag: {other.tag}");
        
        // Check if the colliding object OR its parent has the "Vehicle" tag
        if (other.CompareTag("Vehicle") || 
            (other.transform.parent != null && other.transform.parent.CompareTag("Vehicle")))
        {
            Debug.Log("VEHICLE DETECTED! Showing congratulations...");
            if (instructionManager != null)
            {
                instructionManager.ShowCongratulations();
            }
            else
            {
                Debug.LogError("InstructionManager is NULL!");
            }
        }
    }
    
    // If you're using 2D colliders, use this instead:
    /*
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Vehicle") || other.CompareTag("Player"))
        {
            if (instructionManager != null)
            {
                instructionManager.ShowCongratulations();
            }
        }
    }
    */
}