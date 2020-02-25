using UnityEngine;

public class HealthBar : MonoBehaviour
{
    public GameObject heartImage;

    public int maxHealth;
    private int health;

    public int Health
    {
        get => health;
        set
        {
            health = value;

            int i = 0;
            foreach (Transform childTransform in transform)
            {
                GameObject child = childTransform.gameObject;
                child.SetActive(i < health);
                ++i;
            }
        }
    }

    public void Reset()
    {
        Health = maxHealth;
    }

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < maxHealth; i++)
        {
            Instantiate(heartImage, transform);
        }
        Reset();
    }
}
