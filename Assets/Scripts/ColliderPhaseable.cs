using UnityEngine;

[RequireComponent(typeof(EnergyMeter))]
[RequireComponent(typeof(HealthManager))]
[RequireComponent(typeof(Collider2D))]
public class ColliderPhaseable: MonoBehaviour
{
    [SerializeField]
    private Collider2D phaseWallCollider = default;

    private EnergyMeter energyMeter;
    private HealthManager healthManager;
    private new Collider2D collider2D;

    private void Awake()
    {
        energyMeter = GetComponent<EnergyMeter>();
        healthManager = GetComponent<HealthManager>();
        collider2D = GetComponent<Collider2D>();
    }

    private void FixedUpdate()
    {
        Physics2D.IgnoreCollision(collider2D, phaseWallCollider, energyMeter.Phasing);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider == phaseWallCollider && energyMeter.ColliderCooldown > 0)
        {
            healthManager.Health = 0;
        }
    }
}
