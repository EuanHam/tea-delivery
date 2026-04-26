using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CreditsScreenController : MonoBehaviour
{

    [SerializeField] private AudioSource backgroundMusicSource;
    [SerializeField] private Button backButton;
    
    void Start()
    {
        if (backgroundMusicSource != null)
        {
            backgroundMusicSource.loop = true;
            backgroundMusicSource.Play();
        }
        else
        {
            Debug.LogError("Background music source not assigned in CreditsScreenController");
        }

        if (backButton != null)
        {
            backButton.onClick.AddListener(GoBackToTitleScreen);
        }
        else
        {
            Debug.LogError("Back button not assigned in CreditsScreenController");
        }
    }


    // Update is called once per frame
    void Update()
    {
        
    }

    public void GoBackToTitleScreen()
    {
        SceneManager.LoadScene("TitleScreen");
    }
}
