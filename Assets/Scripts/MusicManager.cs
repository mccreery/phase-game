using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public AudioClip music;

    public static MusicManager Instance { get; private set; }

    void Start()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(transform.root.gameObject);
        }

        AudioSource audioSource = Instance.GetComponent<AudioSource>();

        if (audioSource.clip != music || !audioSource.isPlaying)
        {
            audioSource.Stop();
            audioSource.clip = music;
            audioSource.Play();
        }
    }
}
