using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RogueMovement : BaseMovement
{

    public float doubleJumpMult;
    public bool doubleJumped;

    float prevGravity;


    public new void Start()
    {
        base.Start();
        character.secondary.AbilityEndEvent += RevertGravity;
    }

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


    public new void FixedUpdate()
    {
        base.FixedUpdate();

        if (isOnGround)
            if (doubleJumped)
                doubleJumped = false;
    }



    public void StandStill()
    {
        prevGravity = rigidBody.gravityScale;
        rigidBody.gravityScale = 0f;
        rigidBody.velocity = Vector2.zero;
    }

    public void RevertGravity()
    {
        rigidBody.gravityScale = prevGravity;
    }

}
