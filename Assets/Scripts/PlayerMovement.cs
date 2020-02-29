using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(EnergyMeter))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SpriteRenderer))]
public class PlayerMovement : MonoBehaviour
{
    public float runAcceleration = 1.0f;
    public float runMaxVelocity = 7.0f;
    public float jumpVelocityY = 7.0f;
    public float wallJumpVelocityX = 6.0f;

    private new Rigidbody2D rigidbody2D;
    private new Collider2D collider2D;

    private WallFlags wallsHit;
    private bool jumpPending;

    private EnergyMeter meter;
    private Animator animator;
    private SpriteRenderer spriteRenderer;

    void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        collider2D = GetComponent<Collider2D>();
        meter = GetComponent<EnergyMeter>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        // Player has to start pressing while touching the floor or wall to jump
        jumpPending = jumpPending || Input.GetButtonDown("Jump") && wallsHit.Any(WallFlags.Floor | WallFlags.Horizontal);
    }

    void FixedUpdate()
    {
        wallsHit = WallTest.GetFlags(collider2D);

        Vector2 velocity = rigidbody2D.velocity;
        float inputX = Input.GetAxisRaw("Horizontal");
        velocity.x = Mathf.MoveTowards(velocity.x, runMaxVelocity * inputX, runAcceleration * Time.deltaTime);

        animator.SetFloat("GroundSpeed", Mathf.Abs(velocity.x));
        animator.SetBool("OnFloor", wallsHit.Any(WallFlags.Floor));
        animator.SetBool("OnWall", wallsHit.Any(WallFlags.Horizontal));
        if (inputX != 0)
        {
            spriteRenderer.flipX = inputX < 0;
        }

        // Player still has to be pressing and touching a wall for the jump to succeed
        if (jumpPending && Input.GetButton("Jump") && wallsHit.Any(WallFlags.Floor | WallFlags.Horizontal))
        {
            velocity.y = jumpVelocityY;

            if (!wallsHit.Any(WallFlags.Floor))
            {
                if (wallsHit.Any(WallFlags.LeftWall))
                {
                    velocity.x = wallJumpVelocityX;
                }
                else if (wallsHit.Any(WallFlags.RightWall))
                {
                    velocity.x = -wallJumpVelocityX;
                }
            }
        }
        jumpPending = false;

        velocity = wallsHit.Clamp(velocity);
        rigidbody2D.velocity = velocity;
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.CompareTag("Takeable"))
        {
            meter.Energy += 50;
            Destroy(coll.gameObject);
        }
    }
}
