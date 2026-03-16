using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

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
            playButton.onClick.AddListener(() => StartCoroutine(PlaySoundAndGoToLevelSelection()));
        }
        else
        {
            Debug.LogError("Play button not configured in TitleScreenController");
        }
    }

    private IEnumerator PlaySoundAndGoToLevelSelection()
    {
        if (audioSource != null)
        {
            audioSource.Play();
            yield return new WaitForSeconds(audioSource.clip.length);
        }

        SceneManager.LoadScene("LevelSelection");
    }
}