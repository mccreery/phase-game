using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
public class EnemyFollow : MonoBehaviour
{
    public float runAcceleration = 1.0f;
    public float runMaxVelocity = 7.0f;
    public float jumpVelocityY = 7.0f;
    public float wallJumpVelocityX = 6.0f;

    private new Rigidbody2D rigidbody2D;
    private new Collider2D collider2D;

    private WallFlags wallsHit;

    public Transform target;

    void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        collider2D = GetComponent<Collider2D>();
    }
        
    void FixedUpdate()
    {
        wallsHit = WallTest.GetFlags(collider2D);

        Vector2 velocity = rigidbody2D.velocity;
        int horiz = target.position.x > transform.position.x ? 1 : -1;
        int vert = target.position.y > transform.position.y + 1 ? 1 : -1;

        if ((horiz == 1 && wallsHit.Any(WallFlags.RightWall)) || (horiz == -1 && wallsHit.Any(WallFlags.LeftWall)))
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
        velocity.x = Mathf.MoveTowards(velocity.x, runMaxVelocity * horiz, runAcceleration * Time.deltaTime);
       
        if (vert == 1 && wallsHit.Any(WallFlags.Floor | WallFlags.Horizontal))
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


        velocity = wallsHit.Clamp(velocity);
        rigidbody2D.velocity = velocity;
    }
        
}
