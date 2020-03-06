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
            int oldHealth = health;
            health = value;

            CheckHealth();
            if (oldHealth != health)
            {
                SetInvul();
                healthChanged.Invoke();
            }
        }
    }

    [SerializeField]
    private int maxHealth = 3;
    public int MaxHealth => maxHealth;

    [SerializeField]
    private UnityEvent healthChanged = new UnityEvent();
    public UnityEvent HealthChanged => healthChanged;

    private void OnValidate()
    {
        CheckHealth();
        SetInvul();
        healthChanged.Invoke();
    }

    private void CheckHealth()
    {
        if (health <= 0)
        {
            StartCoroutine(Kill());
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

    [SerializeField]
    private GameObject splatEffect;

    private void Start()
    {
        SetInvul();
    }

    private void SetInvul()
    {
        invulTime = Time.timeSinceLevelLoad + invulTimeOnHit;
        if (Invulnerable && !blinking && !gameObject.activeInHierarchy)
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

        spriteToBlink.enabled = health > 0;
        blinking = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy") && !Invulnerable)
        {
            --Health;
        }
    }

    public IEnumerator Kill()
    {
        // Make sure the world knows we have no health
        // Don't set the property directly as this will cause a loop
        invulTime = 0;
        health = 0;
        healthChanged.Invoke();

        spriteToBlink.enabled = false;

        // Can't instantiate from editor until end of frame
        yield return new WaitForEndOfFrame();
        Instantiate(splatEffect, transform);

        Time.timeScale = 0;
        yield return new WaitForSecondsRealtime(3);
        Time.timeScale = 1;

        LevelSelect.ReloadLevel();
    }
}
