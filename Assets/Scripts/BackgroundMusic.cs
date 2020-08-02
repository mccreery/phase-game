using UnityEngine;
using UnityEngine.Audio;

public class BackgroundMusic : MonoBehaviour
{
    [SerializeField]
    private AudioClip music = default;

    [SerializeField]
    private AudioMixerGroup output = default;

    private void Start()
    {
        GameObject gameObject = GameObject.Find(nameof(BackgroundMusic));
        AudioSource audioSource;

        if (gameObject == null)
        {
            gameObject = new GameObject(nameof(BackgroundMusic));
            DontDestroyOnLoad(gameObject);

            audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.loop = true;
            audioSource.outputAudioMixerGroup = output;
        }
        else
        {
            audioSource = gameObject.GetComponent<AudioSource>();
        }

        if (audioSource.clip != music || !audioSource.isPlaying)
        {
            audioSource.Stop();
            audioSource.clip = music;
            audioSource.Play();
        }
    }
}
