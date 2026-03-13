using UnityEngine;
using UnityEngine.InputSystem; // If using new Input System
using TMPro; // Add this for TextMeshPro

public class InstructionManager : MonoBehaviour
{
    [Header("UI References")]
    public GameObject instructionPanel;
    public TextMeshProUGUI instructionText; // Changed from Text to TextMeshProUGUI
    public GameObject congratsPanel;
    
    [Header("Instructions")]
    public string[] instructions = new string[]
    {
        "Welcome to the world Robbi! (Press space)",
        "Your life's ambition is delivering drinks!",
        "You currently have Jasmine Milk tea with boba!",
        "Let's find the target to delivery the boba!",
        "Use WASD to navigate!"
    };
    
    private int currentInstructionIndex = 0;
    
    void Start()
    {
        ShowInstruction(currentInstructionIndex);
        
        if (congratsPanel != null)
            congratsPanel.SetActive(false);
    }
    
    void Update()
    {
        // Adjust this based on your Input System setup
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            AdvanceInstruction();
        }
    }
    
    void ShowInstruction(int index)
    {
        if (index < instructions.Length)
        {
            instructionText.text = instructions[index];
        }
    }
    
    void AdvanceInstruction()
    {
        currentInstructionIndex++;
        
        if (currentInstructionIndex < instructions.Length)
        {
            ShowInstruction(currentInstructionIndex);
        }
        else
        {
            instructionPanel.SetActive(false);
        }
    }
    
    public void ShowCongratulations()
    {
        Debug.Log("ShowCongratulations called - hiding panel and showing congrats");
        
        if (instructionPanel != null)
        {
            instructionPanel.SetActive(false);
            Debug.Log($"Instruction panel active state set to: {instructionPanel.activeSelf}");
        }
        else
        {
            Debug.LogError("instructionPanel is NULL!");
        }
        
        if (congratsPanel != null)
        {
            congratsPanel.SetActive(true);
            Debug.Log($"Congrats panel active state set to: {congratsPanel.activeSelf}");
        }
        else
        {
            Debug.LogError("congratsPanel is NULL!");
        }
    }
}