using UnityEngine;

public class CongratsController : MonoBehaviour
{
    [SerializeField] private GameObject congratsCanvas;

    // public void ShowCongrats()
    // {
    //     congratsCanvas.SetActive(true);
    //     MusicManager.Instance.PauseMusic();
    // }

    // public void HideCongrats()
    // {
    //     congratsCanvas.SetActive(false);
    //     MusicManager.Instance.ResumeMusic();
    // }

    void OnEnable()
    {
        // Called automatically when this GameObject is set active
        MusicManager.Instance.PauseMusic();
    }

    void OnDisable()
    {
        // Called automatically when this GameObject is set inactive
        MusicManager.Instance.ResumeMusic();
    }
}