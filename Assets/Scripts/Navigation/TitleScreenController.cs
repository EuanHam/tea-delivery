using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleScreenController : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private Button playButton;
    [SerializeField] private Button exitButton;
    [SerializeField] private Button creditsButton;
    [Header("Audio")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioSource backgroundMusicSource;
    
    void Start()
    {
        if (playButton != null && exitButton != null && creditsButton != null)
        {
            playButton.onClick.AddListener(PlaySoundAndGoToLevelSelection);
            exitButton.onClick.AddListener(PlaySoundAndQuitGame);
            creditsButton.onClick.AddListener(PlaySoundAndShowCredits);
        }
        else
        {
            Debug.LogError("One or more buttons not configured in TitleScreenController");
        }
        
        if (backgroundMusicSource != null)
        {
            backgroundMusicSource.loop = true;
            backgroundMusicSource.Play();
        }
    }
    
    void PlaySoundAndGoToLevelSelection()
    {
        PlaySound();
        StartCoroutine(GoToLevelSelectionWithDelay(0.5f));
    }

    void PlaySoundAndQuitGame()
    {
        PlaySound();
        Invoke(nameof(QuitGame), 0.5f);
    }

    void PlaySoundAndShowCredits()
    {
        PlaySound();
        Invoke(nameof(ShowCredits), 0.5f);
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
    
    System.Collections.IEnumerator GoToLevelSelectionWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene("LevelSelection");
    }
    
    void GoToLevelSelection()
    {
        SceneManager.LoadScene("LevelSelection");
    }

    public void QuitGame()
    {
        Application.Quit();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }

    public void ShowCredits()
    {
        SceneManager.LoadScene("CreditsScreen");
    }

}