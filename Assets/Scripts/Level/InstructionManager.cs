using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections.Generic;

public class InstructionManager : MonoBehaviour
{
    [Header("UI References")]
    public GameObject instructionPanel;
    public TextMeshProUGUI instructionText;
    public GameObject congratsPanel;
    
    [Header("Audio")]
    [SerializeField] private AudioSource backgroundMusicSource;
    
    private string[] instructions;
    private int currentInstructionIndex = 0;

    private BobaDeliveryManager bobaDeliveryManager;
    
    void Start()
    {
        instructions = GetInstructionsForLevel();
        
        if (instructionPanel != null)
        {
            instructionPanel.SetActive(true);
            ShowInstruction(currentInstructionIndex);
        }

        if (congratsPanel != null)
            congratsPanel.SetActive(false);
        
        if (backgroundMusicSource != null)
        {
            backgroundMusicSource.loop = true;
            backgroundMusicSource.Play();
        }
    }
    
    void Update()
    {
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            AdvanceInstruction();
        }
    }
    
    string[] GetInstructionsForLevel()
    {
        string levelName = SceneManager.GetActiveScene().name;
        
        switch (levelName)
        {
            case "Level0Tutorial":
                return new string[]
                {
                    "Say Hello to Robbi!\n(Press space)",
                    "Help Robbi find the Bubble Tea Shop!",
                    "Use WASD to move around!"
                };
            
            case "Level1":
                return new string[]
                {
                    "Congrats on passing training!\n(Press space)",
                    "The icecream truck owner wants matcha now!",
                    "Some roads are barricaded now so find the right path!",
                    "Psst psst hint: find the green space!",
                    "Good luck!"
                };
            
            case "Level2":
                return new string[]
                {
                    "Level 2!"
                };
            
            case "Level3":
                return new string[]
                {
                    "Level 3!"
                };
            
            default:
                return new string[] { "Default" };
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
        if (instructionPanel != null)
            instructionPanel.SetActive(false);
        
        if (congratsPanel != null)
            congratsPanel.SetActive(true);
    }
}