using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using System;
using TMPro;

public struct Dialogue
{
    public string speaker;
    public string text;

    public static implicit operator string(Dialogue d) => d.text;
}

public class UIManager : MonoBehaviour
{
    private Dialogue[] tutorialDialogue = new Dialogue[]
    {
        new Dialogue { speaker = "Robbi", text = "Welcome to the city, Robbi!" },
        new Dialogue { speaker = "Robbi", text = "Say Hello to Robbi!\n(Press space)" },
        new Dialogue { speaker = "Robbi", text = "Help Robbi find the Bubble Tea Shop!" },
        new Dialogue { speaker = "Robbi", text = "Use WASD to move around and press SPACE to honk!" }
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
    [SerializeField] private Slider deliveryUI;
    [SerializeField] private TMP_Text deliveryText;

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
        deliveryUI.gameObject.SetActive(false);
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

        if (player.load != null) {
            deliveryUI.gameObject.SetActive(true);
            deliveryText.text = TimeSpan.FromSeconds(player.load.ttl).ToString(@"mm\:ss");
            deliveryUI.value = player.load.ttl / player.load.duration;
        }
        
        else deliveryUI.gameObject.SetActive(false);
        
        balanceText.text = "$" + player.balance.ToString();

        timeText.text = TimeSpan.FromSeconds(lm.time).ToString(@"mm\:ss");

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
        if (index >= tutorialDialogue.Length)
        {
            instructionUI.SetActive(false);
            return;
        }
        
        Dialogue line = tutorialDialogue[index];
        instructionText.text = line.speaker + ": " + line.text;
        // hintText.text = "(Press space to continue)";
    }

    public bool instructionActive()
    {
        return instructionUI.activeSelf;
    }

}
