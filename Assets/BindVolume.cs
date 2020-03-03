using UnityEngine;
using UnityEngine.Audio;

public class BindVolume : MonoBehaviour
{
    public AudioMixer audioMixer;
    public string parameterName;

    public void SetVolume(float value)
    {
        audioMixer.SetFloat(parameterName, VolumeTo_dB(value));
    }

    public static float VolumeTo_dB(float volume)
    {
        return Mathf.Max(-80.0f, 20.0f * Mathf.Log10(volume));
    }
}
