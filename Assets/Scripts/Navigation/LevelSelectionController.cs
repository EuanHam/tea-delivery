using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelectionController : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private Button backButton;
    [SerializeField] private Button[] levelButtons;
    
    void Start()
    {
        if (backButton != null)
        {
            backButton.onClick.AddListener(GoToTitleScreen);
        }
        else
        {
            Debug.LogError("Configure back button on LevelSelectionController");
        }
        
        if (levelButtons != null && levelButtons.Length >= 4)
        {
            levelButtons[0].onClick.AddListener(() => LoadLevel("Level0Tutorial"));
            
            // levels 1-3 (later)
            levelButtons[1].onClick.AddListener(() => LoadLevel("Level1"));
            levelButtons[2].onClick.AddListener(() => LoadLevel("Level2"));
            levelButtons[3].onClick.AddListener(() => LoadLevel("Level3"));
        }
        else
        {
            Debug.LogError("Check the number of buttons assigned!");
        }
    }
    
    void GoToTitleScreen()
    {
        SceneManager.LoadScene("TitleScreen");
    }
    
    void LoadLevel(string levelName)
    {
        SceneManager.LoadScene(levelName);
    }
}