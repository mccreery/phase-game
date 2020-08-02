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
    public float sliderSkip = 1.0f;

    public TargetVolume sfxVolumeController;

    private float energy;
    public float Energy
    {
        get => energy;
        set
        {
            energy = Mathf.Clamp(value, 0, maxEnergy);
            UpdateSlider();
        }
    }

    private void UpdateSlider()
    {
        if (energySlider != null)
        {
            float t = Mathf.InverseLerp(0, maxEnergy, energy);
            float skippedMin = energySlider.minValue + sliderSkip;

            energySlider.value = Mathf.Lerp(skippedMin, energySlider.maxValue, t);

            if (Mathf.Approximately(energySlider.value, skippedMin))
            {
                energySlider.value = energySlider.minValue;
            }
        }
    }

    private void Start()
    {
        Energy = maxEnergy;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        UpdatePhasing();

        if (Phasing)
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
        }
    }

    public SharedBool hasDevice;

    public bool Phasing { get; private set; }
    private void UpdatePhasing()
    {
        bool button = Input.GetButton("Phase");
        if (Phasing)
        {
            if (Energy == 0)
            {
                Phasing = false;
                Cooldown = 2.0f;
            }
            else if (!button)
            {
                Phasing = false;
                Cooldown = 0.1f;
            }
        }
        else if (Energy > 0 && Cooldown == 0 && hasDevice && button)
        {
            Phasing = true;
        }
    }

    private float cooldownTime;
    public float Cooldown
    {
        get => Mathf.Max(0, cooldownTime - Time.time);
        private set => cooldownTime = Time.time + value;
    }
}
