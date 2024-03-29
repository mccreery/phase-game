﻿using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SpriteRenderer))]
public abstract class CharacterMovement : MonoBehaviour
{
    protected new Rigidbody2D rigidbody2D;
    private new Collider2D collider2D;
    private Animator animator;
    private SpriteRenderer spriteRenderer;

    public float runAcceleration = 20.0f;
    public float runMaxVelocity = 7.0f;

    public float jumpVelocity = 17.5f;
    public Vector2 wallJumpVelocity = new Vector2(8.0f, 15.0f);

    protected WallFlags lastWallFlags;
    private bool jumpPending;
    protected bool lastFacingLeft;

    protected virtual float InputX { get; }
    protected abstract bool IsJumpRequested(bool held);

    protected bool HuggingWall => !lastWallFlags.Any(WallFlags.Floor) && (
        lastWallFlags.All(WallFlags.LeftWall) && InputX < 0
            || lastWallFlags.All(WallFlags.RightWall) && InputX > 0
    );

    protected virtual void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        collider2D = GetComponent<Collider2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    protected void Update()
    {
        jumpPending = jumpPending || IsJumpRequested(false)
            && lastWallFlags.Any(WallFlags.Floor | WallFlags.Horizontal);
    }

    protected virtual void FixedUpdate()
    {
        lastWallFlags = WallTest.GetFlags(collider2D);
        Vector2 velocity = rigidbody2D.velocity;

        float cachedInputX = InputX;
        velocity.x = Mathf.MoveTowards(velocity.x, runMaxVelocity * cachedInputX, runAcceleration * Time.deltaTime);

        if (cachedInputX != 0)
        {
            lastFacingLeft = cachedInputX < 0;
        }

        // Player still has to be pressing and touching a wall for the jump to succeed
        if (jumpPending && IsJumpRequested(true))
        {
            if (lastWallFlags.Any(WallFlags.Floor))
            {
                velocity.y = jumpVelocity;
            }
            else if (HuggingWall)
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
        jumpPending = false;

        velocity = lastWallFlags.Clamp(velocity);
        rigidbody2D.velocity = velocity;
        UpdateAnimator();
    }

    public static readonly string HorizontalSpeedKey = "HorizontalSpeed";
    public static readonly string VerticalSpeedKey = "VerticalSpeed";
    public static readonly string GroundedKey = "Grounded";
    public static readonly string TouchingWallKey = "TouchingWall";
    public static readonly string FacingLeftKey = "FacingLeft";

    protected void UpdateAnimator()
    {
        animator.SetFloat(HorizontalSpeedKey, Mathf.Abs(rigidbody2D.velocity.x));
        animator.SetFloat(VerticalSpeedKey, rigidbody2D.velocity.y);
        animator.SetBool(GroundedKey, lastWallFlags.Any(WallFlags.Floor));
        animator.SetBool(TouchingWallKey, HuggingWall);
        spriteRenderer.flipX = lastFacingLeft;
    }
}
