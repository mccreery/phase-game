using UnityEngine;

public class PlayerMovement : CharacterMovement
{
    public Door door;

    protected override float InputX => Input.GetAxisRaw("Horizontal");

    protected override bool IsJumpRequested(bool held)
    {
        return !door.TouchingPlayer && (held ? Input.GetButton("Jump") : Input.GetButtonDown("Jump"));
    }

    private EnergyMeter meter;

    public bool HasKey { get; private set; }

    public float slideVelocity = -2.0f;

    protected override void Awake()
    {
        base.Awake();
        meter = GetComponent<EnergyMeter>();
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();

        if (HuggingWall && rigidbody2D.velocity.y < slideVelocity)
        {
            Vector2 velocity = rigidbody2D.velocity;
            velocity.y = slideVelocity;
            rigidbody2D.velocity = velocity;
        }
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.CompareTag("Battery"))
        {
            meter.Energy += 50;
            Destroy(coll.gameObject);
        }
        else if (coll.gameObject.CompareTag("Key"))
        {
            HasKey = true;
            Destroy(coll.gameObject);
        }
        else if (coll.gameObject.CompareTag("Takeable"))
        {
            Destroy(coll.gameObject);
        }
    }
}
