using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

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
            backButton.onClick.AddListener(() => StartCoroutine(PlaySoundAndGoToTitleScreen()));
        }
        else
        {
            Debug.LogError("Configure back button on LevelSelectionController");
        }

        if (levelButtons != null && levelButtons.Length >= 4)
        {
            levelButtons[0].onClick.AddListener(() => StartCoroutine(PlaySoundThenLoadLevel("Level0Tutorial")));
            levelButtons[1].onClick.AddListener(() => StartCoroutine(PlaySoundThenLoadLevel("Level1")));
            levelButtons[2].onClick.AddListener(() => StartCoroutine(PlaySoundThenLoadLevel("Level2")));
            levelButtons[3].onClick.AddListener(() => StartCoroutine(PlaySoundThenLoadLevel("Level3")));
        }
        else
        {
            Debug.LogError("Check the number of buttons assigned!");
        }
    }

    private IEnumerator PlaySoundAndGoToTitleScreen()
    {
        PlaySound();
        if (audioSource != null)
            yield return new WaitForSeconds(audioSource.clip.length);
        SceneManager.LoadScene("TitleScreen");
    }

    private IEnumerator PlaySoundThenLoadLevel(string levelName)
    {
        PlaySound();
        if (audioSource != null)
            yield return new WaitForSeconds(audioSource.clip.length);
        SceneManager.LoadScene(levelName);
    }

    private void PlaySound()
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
}