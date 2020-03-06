using UnityEngine;

public class HealthBar : MonoBehaviour
{
    [SerializeField]
    private GameObject heartImage;

    [SerializeField]
    private int health = 3;

    [SerializeField]
    private int maxHealth = 3;

    public int Health
    {
        get => health;
        set
        {
            health = value;
            SyncHearts();
        }
    }

    public int MaxHealth => maxHealth;

    private void Awake()
    {
        for (int i = 0; i < maxHealth; i++)
        {
            Instantiate(heartImage, transform);
        }
    }

    private void OnValidate()
    {
        SyncHearts();
    }

    /// <summary>
    /// Called when the health value changes by any means.
    /// The bar is updated visually and level reset conditions are checked.
    /// </summary>
    private void SyncHearts()
    {
        if (health <= 0)
        {
            LevelSelect.ReloadLevel();
        }
        else if (health > maxHealth)
        {
            health = maxHealth;
        }

        // Enable only the hearts that are filled
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(i < health);
        }
    }
}
