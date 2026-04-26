using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private PowerUpManager powerUpManager;
    [SerializeField] private LevelManager levelManager;
    [SerializeField] private GameObject player;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip bell;

    private bool played;
    private bool isMuted = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        played = false;  
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = player.transform.position;

        if (levelManager.time <= 0f && !played)
        {
            played = true;
            StopMusic();
            PlayBell();
        }
    }
    public void StopMusic()
    {
        audioSource.Stop();
    }

    public void StartMusic()
    {
        audioSource.Play();
    }

    public void PlayBell()
    {
        if (bell != null)
        {
            AudioSource.PlayClipAtPoint(bell, transform.position);
        }
    }

    public void ToggleMute()
    {
        isMuted = !isMuted;
        audioSource.mute = isMuted;
    }
}
