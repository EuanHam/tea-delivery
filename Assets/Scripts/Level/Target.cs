using UnityEngine;
using UnityEngine.SceneManagement;

public class Target : MonoBehaviour
{
    private InstructionManager instructionManager;
    
    void Start()
    {
        instructionManager = FindObjectOfType<InstructionManager>();
        
        if (instructionManager == null)
        {
            Debug.LogError("InstructionManager not found in scene");
        }
    }
    
    void OnTriggerEnter(Collider other)
    {
        Debug.Log($"Something entered: {other.gameObject.name}, Tag: {other.tag}");
        
        // check if collision or parent has tag
        if (other.CompareTag("Player") || 
            (other.transform.parent != null && other.transform.parent.CompareTag("Player")))
        {
            Debug.Log("Player detected! Showing congratulations...");
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
    
    private System.Collections.IEnumerator ReturnToLevelSelectionAfterDelay(float delaySeconds)
    {
        yield return new WaitForSeconds(delaySeconds);
        SceneManager.LoadScene("LevelSelection");
    }
}