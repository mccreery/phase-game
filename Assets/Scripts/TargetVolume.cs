using UnityEngine;

public class TargetVolume : MonoBehaviour
{
    public AudioSource audioSource;

    public float transitionTime = 1.0f;
    public float targetVolume = 1.0f;

    private void OnValidate()
    {
        targetVolume = Mathf.Clamp(targetVolume, 0, 1);
    }

    void Update()
    {
        audioSource.volume = Mathf.MoveTowards(audioSource.volume, targetVolume, Time.deltaTime / transitionTime);
    }
}
