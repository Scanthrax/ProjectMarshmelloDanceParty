using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkullMovement : BaseMovement
{
    public bool canJump;

    public float horizontalForce;


    public new void Start()
    {
        canJump = false;
    }


    public new void FixedUpdate()
    {
        PhysicsCheck();

        if(isOnGround && canJump)
        {
            Jump();
        }


    }


    public override void Jump()
    {
        if ((isOnGround && !jumped))
        {
            if (!jumped)
                jumped = true;

            rigidBody.AddForce(new Vector2(horizontalForce * input.horizontal, jumpForce), ForceMode2D.Impulse);

        }
    }



    public void SetJump(bool b)
    {
        canJump = b;
    }
}
