using UnityEngine;
using UnityEngine.Audio;

public class LoadAudioMixer : MonoBehaviour
{
    [SerializeField]
    private AudioMixer audioMixer = default;

    [SerializeField]
    public string[] parameterNames = default;

    private void Start()
    {
        foreach (string parameterName in parameterNames)
        {
            audioMixer.SetFloat(parameterName, PlayerPrefs.GetFloat(parameterName));
        }
    }
}
