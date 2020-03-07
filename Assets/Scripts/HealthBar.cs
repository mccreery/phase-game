using UnityEngine;

public class HealthBar : MonoBehaviour
{
    [SerializeField]
    private HealthManager healthManager = default;

    [SerializeField]
    private GameObject heartImage = default;

    private void Awake()
    {
        for (int i = 0; i < healthManager.MaxHealth; i++)
        {
            Instantiate(heartImage, transform);
        }
        healthManager.HealthChanged.AddListener(SyncHearts);
    }

    private void SyncHearts()
    {
        int health = healthManager.Health;

        // Enable only the hearts that are filled
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(i < health);
        }
    }
}
