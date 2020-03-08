using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class BindVolume : MonoBehaviour
{
    public AudioMixer audioMixer;
    public string parameterName;

    private void Awake()
    {
        Slider slider = GetComponent<Slider>();

        audioMixer.GetFloat(parameterName, out float dB);
        slider.value = DBToVolume(dB);

        slider.onValueChanged.AddListener(value =>
        {
            audioMixer.SetFloat(parameterName, VolumeToDB(value));
        });
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
