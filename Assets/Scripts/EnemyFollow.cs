using UnityEngine;

public class EnemyFollow : CharacterMovement
{
    protected override bool IsJumpRequested(bool held)
    {
        return held ? Input.GetButton("Jump") : Input.GetButtonDown("Jump");
    }

    private Vector2 velocity;

    public Transform target;
    public float vertBias;
    private bool ifJump = false;
    private bool ifMove = false;
    private bool ifWallJump = false;
    private int direction;

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

    protected override void FixedUpdate()
    {
        lastWallFlags = WallTest.GetFlags(GetComponent<Collider2D>());
        velocity = GetComponent<Rigidbody2D>().velocity;
        
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
        GetComponent<Rigidbody2D>().velocity = velocity;
        UpdateAnimator();
    }
        
    private void Move(int dir)
    {
        lastFacingLeft = dir == 1 ? false : true;
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
            }
            else if (lastWallFlags.Any(WallFlags.RightWall))
            {
                velocity = wallJumpVelocity;
                velocity.x = -Mathf.Abs(velocity.x);
            }
        }
    }

    private bool CheckPos(float bias, bool horiz, bool positive, Transform tr)
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
