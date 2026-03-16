using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleScreenController : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private Button playButton;
    
    [Header("Audio")]
    [SerializeField] private AudioSource audioSource;
    
    void Start()
    {
        if (playButton != null)
        {
            playButton.onClick.AddListener(PlaySoundAndGoToLevelSelection);
        }
        else
        {
            Debug.LogError("Play button not configured in TitleScreenController");
        }
    }
    
    void PlaySoundAndGoToLevelSelection()
    {
        PlaySound();
        StartCoroutine(GoToLevelSelectionWithDelay(0.5f));
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
}