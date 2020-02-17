using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
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

    void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        collider2D = GetComponent<Collider2D>();
		meter = GetComponent<EnergyMeter>();
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
        velocity.x = Mathf.MoveTowards(velocity.x, runMaxVelocity * Input.GetAxisRaw("Horizontal"), runAcceleration * Time.deltaTime);

        // Player still has to be pressing and touching a wall for the jump to succeed
        if (jumpPending && Input.GetButton("Jump") && wallsHit.Any(WallFlags.Floor | WallFlags.Horizontal))
        {
            velocity.y = jumpVelocityY;

            if (wallsHit.Any(WallFlags.LeftWall))
            {
                velocity.x = wallJumpVelocityX;
            }
            else if (wallsHit.Any(WallFlags.RightWall))
            {
                velocity.x = -wallJumpVelocityX;
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
			coll.gameObject.SetActive(false);
			if (meter.currentEnergy < (meter.startEnergy - 50))
			{
				meter.currentEnergy += 50;
			}
		}	
	}
}
