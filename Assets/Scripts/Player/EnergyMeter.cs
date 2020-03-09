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
    public TargetVolume sfxVolumeController;

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

    private bool phasingLast;
    public float recentUnphase = -1;

    void Update()
    {
        bool phasing = Phasing;

        if (phasing)
        {
            Energy -= useRate * Time.deltaTime;
            spriteRenderer.maskInteraction = SpriteMaskInteraction.VisibleInsideMask;
            sfxVolumeController.targetVolume = 1.0f;
        } 
        else
        {
            Energy += recoveryRate * Time.deltaTime;
            spriteRenderer.maskInteraction = SpriteMaskInteraction.None;
            sfxVolumeController.targetVolume = 0.0f;

            if (phasingLast)
            {
                recentUnphase = Time.timeSinceLevelLoad;
            }
        }
        phasingLast = phasing;
    }
}
