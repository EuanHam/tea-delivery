using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleScreenController : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private Button playButton;
    
    void Start()
    {
        if (playButton != null)
        {
            playButton.onClick.AddListener(GoToLevelSelection);
        }
        else
        {
            Debug.LogError("Play button not configured in TitleScreenController");
        }
    }
    
    void GoToLevelSelection()
    {
        SceneManager.LoadScene("LevelSelection");
    }
}