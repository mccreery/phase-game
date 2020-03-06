using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
public class EnemyFollow : MonoBehaviour
{
    private new Rigidbody2D rigidbody2D;
    private new Collider2D collider2D;

    public float runAcceleration = 15.0f;
    public float runMaxVelocity = 5.0f;

    public float jumpVelocity = 17.5f;
    public Vector2 wallJumpVelocity = new Vector2(8.0f, 15.0f);

    private WallFlags lastWallFlags;
    private Vector2 velocity;

    public Transform target;
    public float vertBias;
    private bool ifJump = false;
    private bool ifMove = false;
    private bool ifWallJump = false;
    private int direction;
    private bool wallJumped = false;

    void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        collider2D = GetComponent<Collider2D>();
    } 

    void OnTriggerStay2D(Collider2D coll)
    {
        AICollider aiColl = coll.gameObject.GetComponent<AICollider>();

        if (coll.gameObject.CompareTag("Point"))
        {        
            // just jump
            if (!aiColl.jump.noJump && !aiColl.jump.wallJump)
            {
                if (aiColl.horizontal.horiz && aiColl.vertical.vert)
                {
                    if (aiColl.either)
                    {
                        if (CheckPos(aiColl.horizontal.bias, true, aiColl.horizontal.right, aiColl.transform) || CheckPos(aiColl.vertical.bias, false, aiColl.vertical.above, aiColl.transform))
                        {
                            ifJump = !aiColl.jump.noJump;
                            if (aiColl.move.move)
                            {
                                ifMove = true;
                                direction = aiColl.move.moveRight ? 1 : -1;
                            }
                        }
                    }
                    else 
                    {
                        if (CheckPos(aiColl.horizontal.bias, true, aiColl.horizontal.right, aiColl.transform) && CheckPos(aiColl.vertical.bias, false, aiColl.vertical.above, aiColl.transform))
                        {
                            ifJump = !aiColl.jump.noJump;
                            if (aiColl.move.move)
                            {
                                ifMove = true;
                                direction = aiColl.move.moveRight ? 1 : -1;
                            }
                        }
                    }
                }
                else
                {
                    if (aiColl.horizontal.horiz)
                    {
                        if (CheckPos(aiColl.horizontal.bias, true, aiColl.horizontal.right, aiColl.transform))
                        {
                            ifJump = !aiColl.jump.noJump;
                            if (aiColl.move.move)
                            {
                                ifMove = true;
                                direction = aiColl.move.moveRight ? 1 : -1;
                            }
                        }
                    }
                    else
                    {
                        if (CheckPos(aiColl.vertical.bias, false, aiColl.vertical.above, aiColl.transform))
                        {
                            ifJump = !aiColl.jump.noJump;
                            if (aiColl.move.move)
                            {
                                ifMove = true;
                                direction = aiColl.move.moveRight ? 1 : -1;
                            }
                        }
                    }
                }
            }
            // just wall jump
            else if (aiColl.jump.wallJump)
            {
                if (aiColl.horizontal.horiz && aiColl.vertical.vert)
                {
                    if (aiColl.either)
                    {
                        if (CheckPos(aiColl.horizontal.bias, true, aiColl.horizontal.right, aiColl.transform) || CheckPos(aiColl.vertical.bias, false, aiColl.vertical.above, aiColl.transform))
                            ifWallJump = aiColl.jump.wallJump;
                    }
                    else
                    {
                        if (CheckPos(aiColl.horizontal.bias, true, aiColl.horizontal.right, aiColl.transform) && CheckPos(aiColl.vertical.bias, false, aiColl.vertical.above, aiColl.transform))
                            ifWallJump = aiColl.jump.wallJump;
                    }
                }
                else
                {
                    if (aiColl.horizontal.horiz)
                    {
                        if (CheckPos(aiColl.horizontal.bias, true, aiColl.horizontal.right, aiColl.transform))
                            ifWallJump = aiColl.jump.wallJump;
                    }
                    else
                    {
                        if (CheckPos(aiColl.vertical.bias, false, aiColl.vertical.above, aiColl.transform))
                            ifWallJump = aiColl.jump.wallJump;
                    }
                }
            }
            // just move
            else if (aiColl.move.move)
            {
                if (aiColl.horizontal.horiz && aiColl.vertical.vert)
                {
                    if (aiColl.either)
                    {
                        if (CheckPos(aiColl.horizontal.bias, true, aiColl.horizontal.right, aiColl.transform) || CheckPos(aiColl.vertical.bias, false, aiColl.vertical.above, aiColl.transform))
                        {
                            ifMove = aiColl.move.move;
                            direction = aiColl.move.moveRight ? 1 : -1;
                        }
                    }
                    else
                    {
                        if (CheckPos(aiColl.horizontal.bias, true, aiColl.horizontal.right, aiColl.transform) && CheckPos(aiColl.vertical.bias, false, aiColl.vertical.above, aiColl.transform))
                        {
                            ifMove = aiColl.move.move;
                            direction = aiColl.move.moveRight ? 1 : -1;
                        }
                    }
                }
                else
                {
                    if (aiColl.horizontal.horiz)
                    {
                        if (CheckPos(aiColl.horizontal.bias, true, aiColl.horizontal.right, aiColl.transform))
                        {
                            ifMove = aiColl.move.move;
                            direction = aiColl.move.moveRight ? 1 : -1;
                        }
                    }
                    else
                    {
                        if (CheckPos(aiColl.vertical.bias, false, aiColl.vertical.above, aiColl.transform))
                        {
                            ifMove = aiColl.move.move;
                            direction = aiColl.move.moveRight ? 1 : -1;
                        }
                    }
                }
            }
        }
    }

    void FixedUpdate()
    {
        lastWallFlags = WallTest.GetFlags(collider2D);
        velocity = rigidbody2D.velocity;
        
        if (ifJump)
        {
            ifJump = false;
            Jump();
        }
        if (ifWallJump)
        {
            if (ifMove)
                ifWallJump = false;
            Jump();
            WallJump();
        }
        if (ifMove)
        {
            Debug.Log("mov");
            ifMove = false;
            Move(direction);
        }
        else if (!ifWallJump)
        {
            direction = target.position.x > transform.position.x ? 1 : -1;
            Move(direction);
        }
        else
        {
            ifWallJump = false;
        }
            
        velocity = lastWallFlags.Clamp(velocity);
        rigidbody2D.velocity = velocity;
    }
        
    private void Move(int dir)
    {
        velocity.x = Mathf.MoveTowards(velocity.x, runMaxVelocity * dir, runAcceleration * Time.deltaTime);
    }

    private void Jump()
    {
        if (lastWallFlags.Any(WallFlags.Floor))
            velocity.y = jumpVelocity;
    }

    private void WallJump()
    {
        if (!lastWallFlags.Any(WallFlags.Floor))
        {
            if (lastWallFlags.Any(WallFlags.LeftWall))
            {
                velocity = wallJumpVelocity;
                velocity.x = Mathf.Abs(velocity.x);
                wallJumped = true;
            }
            else if (lastWallFlags.Any(WallFlags.RightWall))
            {
                velocity = wallJumpVelocity;
                velocity.x = -Mathf.Abs(velocity.x);
                wallJumped = true;
            }
        }
    }

    bool CheckPos(float bias, bool horiz, bool positive, Transform tr)
    {
        if (horiz)
        {
            if (positive)
                return (target.position.x < tr.position.x + bias && target.position.x > tr.position.x);
            else
                return (target.position.x > tr.position.x - bias && target.position.x < tr.position.x);
        }
        else
        {
            if (positive)
                return (target.position.y < tr.position.y + bias && target.position.y > tr.position.y + vertBias);
            else
                return (target.position.y > tr.position.y - bias && target.position.y < tr.position.y - vertBias);
        }
    }
}
