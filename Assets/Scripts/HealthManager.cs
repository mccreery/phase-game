using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class HealthManager : MonoBehaviour
{
    [SerializeField]
    private int health = 3;

    public int Health
    {
        get => health;
        set
        {
            bool changed = health != value;
            health = value;
            CheckHealth();

            if (changed)
            {
                healthChanged.Invoke();
            }
        }
    }

    [SerializeField]
    private int maxHealth = 3;
    public int MaxHealth => maxHealth;

    [SerializeField]
    private UnityEvent healthChanged;
    public UnityEvent HealthChanged => healthChanged;

    private void OnValidate()
    {
        CheckHealth();
        healthChanged.Invoke();
    }

    private void CheckHealth()
    {
        if (health <= 0)
        {
            Kill();
        }
        else if (health > maxHealth)
        {
            health = maxHealth;
        }
    }

    [SerializeField]
    private float invulTimeOnHit = 1.0f;
    [SerializeField]
    private SpriteRenderer spriteToBlink;
    [SerializeField]
    private float blinkDelay = 0.05f;

    private float invulTime;
    private bool blinking = false;

    public bool Invulnerable => invulTime > Time.timeSinceLevelLoad;

    private void Start()
    {
        invulTime = Time.timeSinceLevelLoad + invulTimeOnHit;
    }

    void Update()
    {
        if (Invulnerable && !blinking)
        {
            blinking = true;
            StartCoroutine(Blink());
        }
    }

    private IEnumerator Blink()
    {
        while (Invulnerable)
        {
            yield return new WaitForSeconds(blinkDelay);
            spriteToBlink.enabled = !spriteToBlink.enabled;
        }

        spriteToBlink.enabled = true;
        blinking = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy") && !Invulnerable)
        {
            --Health;
            invulTime = Time.timeSinceLevelLoad + invulTimeOnHit;
        }
    }

    public void Kill()
    {
        LevelSelect.ReloadLevel();
    }
}
