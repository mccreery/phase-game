using UnityEngine;
using UnityEngine.UI;

public class EnergyMeter : MonoBehaviour
{
    public float maxEnergy = 500.0f;
    public float useRate = 200.0f;
    public float recoveryRate = 5.0f;

    public Slider energySlider;

    private float energy;
    public float Energy
    {
        get => energy;
        set
        {
            energy = Mathf.Clamp(value, 0, maxEnergy);
            energySlider.value = energySlider.minValue + (energy / maxEnergy) * (energySlider.maxValue - energySlider.minValue);
        }
    }

    public bool CanPhase => energy > 0;
    public bool Phasing => CanPhase && Input.GetButton("Phase");

    private void Start()
    {
        Energy = maxEnergy;
    }

    void Update()
    {
        if (Phasing)
        {
            Energy -= useRate * Time.deltaTime;
        } 
        else
        {
            Energy += recoveryRate * Time.deltaTime;
        }
    }
}
