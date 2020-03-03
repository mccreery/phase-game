using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class BindVolume : MonoBehaviour
{
    public AudioMixer audioMixer;
    public Slider slider;
    public string parameterName;

    private void Start()
    {
        float dB;
        audioMixer.GetFloat(parameterName, out dB);
        slider.value = DBToVolume(dB);
    }

    public void SetVolume(float value)
    {
        audioMixer.SetFloat(parameterName, VolumeToDB(value));
    }

    public static float VolumeToDB(float volume)
    {
        return Mathf.Max(-80.0f, 20.0f * Mathf.Log10(volume));
    }

    public static float DBToVolume(float dB)
    {
        return Mathf.Pow(10, dB / 20.0f);
    }
}
