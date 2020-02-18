using UnityEngine;
using UnityEngine.UI;

public class EnergyMeter : MonoBehaviour
{
    public float startEnergy = 500;
    public float depleteRate = 200;
    public Slider energySlider;

    // Clamping is handled by Slider
    public float Energy
    {
        get => energySlider.value;
        set => energySlider.value = value;
    }
    public bool CanPhase => energySlider.value > 0;
    public bool Phasing => CanPhase && Input.GetButton("Phase");

    void Start()
    {
        energySlider.minValue = 0;
        energySlider.maxValue = startEnergy;
        energySlider.value = startEnergy;
    }

    void Update()
    {
        if (Phasing)
        {
            energySlider.value -= depleteRate * Time.deltaTime;
        }
    }
}
