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
        slider.value = ReLerp(-80, 0, slider.minValue, slider.maxValue, dB);

        slider.onValueChanged.AddListener(fac =>
        {
            audioMixer.SetFloat(parameterName, ReLerp(slider.minValue, slider.maxValue, -80, 0, fac));
        });
    }

    public static float ReLerp(float oldA, float oldB, float newA, float newB, float x)
    {
        float t = (x - oldA) / (oldB - oldA);
        return Mathf.Lerp(newA, newB, t);
    }
}
