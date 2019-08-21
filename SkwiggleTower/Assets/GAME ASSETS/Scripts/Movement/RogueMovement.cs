using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RogueMovement : BaseMovement
{

    public float doubleJumpMult;

    public override void Jump()
    {
        if ((isOnGround && !jumped) || !doubleJumped)
        {
            if (!jumped)
                jumped = true;
            else if (!isOnGround)
            {
                doubleJumped = true;
                rigidBody.velocity = new Vector2(rigidBody.velocity.x, 0f);
            }

            rigidBody.AddForce(Vector2.up * jumpForce * (!doubleJumped ? 1 : doubleJumpMult), ForceMode2D.Impulse);

        }
    }


}
