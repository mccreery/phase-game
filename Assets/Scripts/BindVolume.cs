using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class BindVolume : MonoBehaviour
{
    public AudioMixer audioMixer;
    public string parameterName;

    public float lowDB = -50.0f;

    private void Awake()
    {
        Slider slider = GetComponent<Slider>();

        audioMixer.GetFloat(parameterName, out float dB);
        slider.value = ReLerp(lowDB, 0, slider.minValue, slider.maxValue, dB);

        slider.onValueChanged.AddListener(fac =>
        {
            if (Mathf.Approximately(fac, slider.minValue))
            {
                audioMixer.SetFloat(parameterName, -80);
            }
            else
            {
                audioMixer.SetFloat(parameterName, ReLerp(slider.minValue, slider.maxValue, lowDB, 0, fac));
            }
        });
    }

    private void OnDestroy()
    {
        audioMixer.GetFloat(parameterName, out float dB);
        PlayerPrefs.SetFloat(parameterName, dB);
    }

    public static float ReLerp(float oldA, float oldB, float newA, float newB, float x)
    {
        float t = (x - oldA) / (oldB - oldA);
        return Mathf.Lerp(newA, newB, t);
    }
}
