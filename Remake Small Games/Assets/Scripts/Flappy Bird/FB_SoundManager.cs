using UnityEngine;

public class FB_SoundManager : MonoBehaviour
{
    public static FB_SoundManager Instance { get; private set; }

    private FB_PlayerController player;

    public AudioClip flapSound;
    public AudioClip scoreSound;
    public AudioClip gameOverSound;
    public AudioSource sfxSource;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (sfxSource == null)
        {
            sfxSource = GetComponent<AudioSource>();
        }
        FB_GameManager.Instance.OnScoreUpdated += PlayScoreSound;
        FB_GameManager.Instance.OnGameOver += PlayGameOverSound;
        player = FindFirstObjectByType<FB_PlayerController>();
        player.OnFlap += PlayFlapSound;
    }

    void OnDestroy()
    {
        if (FB_GameManager.Instance != null)
        {
            FB_GameManager.Instance.OnScoreUpdated -= PlayScoreSound;
            FB_GameManager.Instance.OnGameOver -= PlayGameOverSound;
        }
        player.OnFlap -= PlayFlapSound;
    }

    private void PlaySound(AudioClip clip)
    {
        if (clip != null && sfxSource != null)
        {
            sfxSource.PlayOneShot(clip);
        }
    }

    public void PlayFlapSound()
    {
        PlaySound(flapSound);
    }

    public void PlayScoreSound()
    {
        if (FB_GameManager.Instance.GetScore > 0)
            PlaySound(scoreSound);
    }

    public void PlayGameOverSound()
    {
        PlaySound(gameOverSound);
    }
}
