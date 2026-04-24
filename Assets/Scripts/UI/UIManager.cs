using UnityEngine;
using UnityEngine.InputSystem;
using System;
using TMPro;
public class UIManager : MonoBehaviour
{
    private string[] tutorialInstructions = new string[]
    {
        "Say Hello to Robbi!\n(Press space)",
        "Help Robbi find the Bubble Tea Shop!",
        "Use WASD to move around!"
    };
    private int instructionIndex;

    [SerializeField] private LevelManager lm;
    [SerializeField] private PowerUpManager powerUpManager;

    [SerializeField] private BobaDriver player;
    [SerializeField] private GameObject BobaShop;

    // UI for Balance
    [SerializeField] private GameObject balanceUI;
    [SerializeField] private TMP_Text balanceText;
    
    // UI for Delivery Info
    [SerializeField] private GameObject deliveryUI;

    // UI for Instructions
    [SerializeField] private GameObject instructionUI;
    [SerializeField] private TMP_Text instructionText, hintText;

    // UI for Level
    [SerializeField] private GameObject levelUI;
    [SerializeField] private TMP_Text timeText;
    [SerializeField] public GameObject endUI;
    [SerializeField] private GameObject doubleMoneyUI;
    [SerializeField] private GameObject invulnerableUI;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        instructionIndex = 0;

        balanceUI.SetActive(true);
        deliveryUI.SetActive(false);
        instructionUI.SetActive(true);
        levelUI.SetActive(true);
        endUI.SetActive(false);
        doubleMoneyUI.SetActive(false);
        invulnerableUI.SetActive(false);


        ShowInstruction(instructionIndex);
    }

    // Update is called once per frame
    void Update()
    {

        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            ShowInstruction(++instructionIndex);
        }

        if (player.load != null) deliveryUI.SetActive(true);
        else deliveryUI.SetActive(false);
        
        balanceText.text = "$" + player.balance.ToString();

        timeText.text = TimeSpan.FromSeconds(lm.time).ToString(@"mm\:ss");;

        if (lm.time <=0)
        {
            endUI.SetActive(true);
        }

        if (powerUpManager != null)
        {
            if (powerUpManager.isInvunerable()) 
            {
                invulnerableUI.SetActive(true);    
            }
            else {
                invulnerableUI.SetActive(false);
            }

            if (powerUpManager.isDoubleMoney()) 
            {
                doubleMoneyUI.SetActive(true);
            }
            else
            {
                doubleMoneyUI.SetActive(false);
            }
        }
    }

    private void ShowInstruction(int index)
    {
        if (tutorialInstructions.Length <= instructionIndex) 
            instructionUI.SetActive(false);
        else 
            instructionText.text = tutorialInstructions[index];
    }

    public bool instructionActive()
    {
        return instructionUI.activeSelf;
    }

    private void urgency(GameObject go)
    {
        
    }
}
