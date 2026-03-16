using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelectionController : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private Button backButton;
    [SerializeField] private Button[] levelButtons;
    
    [Header("Audio")]
    [SerializeField] private AudioSource audioSource;
    
    void Start()
    {
        if (backButton != null)
        {
            backButton.onClick.AddListener(() => PlaySoundAndGoToTitleScreen());
        }
        else
        {
            Debug.LogError("Configure back button on LevelSelectionController");
        }
        
        if (levelButtons != null && levelButtons.Length >= 4)
        {
            levelButtons[0].onClick.AddListener(() => PlaySoundThenLoadLevel("Level0Tutorial"));
            
            // levels 1-3 (later)
            levelButtons[1].onClick.AddListener(() => PlaySoundThenLoadLevel("Level1"));
            levelButtons[2].onClick.AddListener(() => PlaySoundThenLoadLevel("Level2"));
            levelButtons[3].onClick.AddListener(() => PlaySoundThenLoadLevel("Level3"));
        }
        else
        {
            Debug.LogError("Check the number of buttons assigned!");
        }
    }
    
    void PlaySoundAndGoToTitleScreen()
    {
        PlaySound();
        GoToTitleScreen();
    }
    
    void PlaySoundThenLoadLevel(string levelName)
    {
        PlaySound();
        LoadLevel(levelName);
    }
    
    void PlaySound()
    {
        if (audioSource != null)
        {
            audioSource.Play();
        }
        else
        {
            Debug.LogError("AudioSource not assigned!");
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