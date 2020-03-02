using UnityEngine;

public class PlayerMovement : CharacterMovement
{
    protected override float InputX => Input.GetAxisRaw("Horizontal");

    protected override bool IsJumpRequested(bool held)
    {
        return held ? Input.GetButton("Jump") : Input.GetButtonDown("Jump");
    }
}
