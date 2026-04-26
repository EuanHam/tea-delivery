using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using System;
using TMPro;
using UnityEngine.SceneManagement;

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
        new Dialogue { speaker = "Mayor", text = "Welcome to the city, Robbi!" },
        new Dialogue { speaker = "Mayor", text = "We at Bobaville are all so excited for your new boba shop!!!" },
        new Dialogue { speaker = "Mayor", text = "Pick up the boba order from the shop and deliver it to the customer. Feel free to use your built in minimap on the bottom right!" },
        new Dialogue { speaker = "Mayor", text = "Use WASD to move around \n Press SPACE to jump" },
        new Dialogue { speaker = "Mayor", text = "And mind the time limit on the top of the screen" },
        new Dialogue { speaker = "Mayor", text = "For your first delivery, the entire town has decided to support you. We will all stand still so we don't get in the way of your work. No pressure!" },
        new Dialogue { speaker = "Robbi", text = "Beep boop ('''' •᷄ ᴗ •᷅ )" }
    };

    private Dialogue[] level1Dialogue = new Dialogue[]
    {
        new Dialogue { speaker = "Mayor", text = "Congrats on your first order!" },
        new Dialogue { speaker = "Mayor", text = "We have now unlocked the entire town for you and people will now move." },
        new Dialogue { speaker = "Mayor", text = "Don't hit anyone because that is bad for business. You might also lose a bit of money." },
        new Dialogue { speaker = "Mayor", text = "By the way, you can collect power-ups! They look like question marks and will help you on your journey." },
        new Dialogue { speaker = "Mayor", text = "They can help you double your next earnings, get more time, or become invulnerable.  Good luck!" },
        new Dialogue { speaker = "Robbi", text = "Beep boop (^-^ ) " }
    };

    private Dialogue[] level2Dialogue = new Dialogue[]
    {
        new Dialogue { speaker = "Mayor", text = "Yay! Looks like things are going well. I think you're ready for vehicles." },
        new Dialogue { speaker = "Mayor", text = "If you collide with them you will lose a lot of money. And also be stunned. Good luck!" },
        new Dialogue { speaker = "Robbi", text = "Beep boop (˵ •̀ ᴗ •́ ˵ )" }
    };

    private Dialogue[] level3Dialogue = new Dialogue[]
    {
        new Dialogue { speaker = "Mayor", text = "Excellent work! For your next challenge, the penalties will be even higher. Good luck!" },
        new Dialogue { speaker = "Robbi", text = "Beep boop (ᵔuᵔ)" }
    };

    private Dialogue[] currentDialogue;
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


        string levelName = SceneManager.GetActiveScene().name;

        switch (levelName)
        {
            case "Level0Tutorial":
                currentDialogue = tutorialDialogue;
                break;
            case "Level1":
                currentDialogue = level1Dialogue;
                break;
            case "Level2":
                currentDialogue = level2Dialogue;
                break;
            case "Level3":
                currentDialogue = level3Dialogue;
                break;
            default:
                currentDialogue = tutorialDialogue;
                break;
        }


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
        if (index >= currentDialogue.Length)
        {
            instructionUI.SetActive(false);
            return;
        }


        
        Dialogue line = currentDialogue[index];
        instructionText.text = line.speaker + ": " + line.text;
        // hintText.text = "(Press space to continue)";
    }

    public bool instructionActive()
    {
        return instructionUI.activeSelf;
    }

}
