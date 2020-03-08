using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(SpriteRenderer))]
public class EnergyMeter : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

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
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (Phasing)
        {
            Energy -= useRate * Time.deltaTime;
            spriteRenderer.maskInteraction = SpriteMaskInteraction.VisibleInsideMask;
        } 
        else
        {
            Energy += recoveryRate * Time.deltaTime;
            spriteRenderer.maskInteraction = SpriteMaskInteraction.None;
        }
    }
}
