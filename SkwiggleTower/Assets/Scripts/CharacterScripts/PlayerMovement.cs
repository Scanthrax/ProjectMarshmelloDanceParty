﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public bool drawDebugRaycasts = true;   //Should the environment checks be visualized

    [Header("Movement Properties")]
    public float speed = 8f;                //Player speed
    public float crouchSpeedDivisor = 3f;   //Speed reduction when crouching
    public float coyoteDuration = .05f;     //How long the player can jump after falling
    public float maxFallSpeed = -25f;       //Max speed player can fall

    [Header("Jump Properties")] //can be overridden in an separate class of Player Movement
    protected float jumpForce = 1f;          //Initial force of jump
    protected float crouchJumpBoost = 1f;    //Jump boost when crouching
    protected float hangingJumpForce = 15f;    //Force of wall hanging jumo
    protected float jumpHoldForce = 1.9f;      //Incremental force when jump is held
    protected float jumpHoldDuration = .1f;    //How long the jump key can be held

    [Header("Environment Check Properties")]
    public float footOffset = .2f;          //X Offset of feet raycast
    public float eyeHeight = 1.5f;          //Height of wall checks
    public float reachOffset = .7f;         //X offset for wall grabbing
    public float headClearance = .5f;       //Space needed above the player's head
    public float groundDistance = .2f;      //Distance player is considered to be on the ground
    public float grabDistance = .4f;        //The reach distance for wall grabs
    public LayerMask groundLayer;           //Layer of the ground

    [Header("Status Flags")]
    public bool isOnGround;                 //Is the player on the ground?
    public bool isJumping;                  //Is player jumping?
    public bool isHanging;                  //Is player hanging?
    public bool isCrouching;                //Is player crouching?
    public bool isHeadBlocked;

    PlayerInput input;                      //The current inputs for the player
    BoxCollider2D bodyCollider;             //The collider component
    Rigidbody2D rigidBody;                  //The rigidbody component

    float jumpTime;                         //Variable to hold jump duration
    float coyoteTime;                       //Variable to hold coyote duration
    float playerHeight;                     //Height of the player

    float originalXScale;                   //Original scale on X axis
    int direction = 1;                      //Direction player is facing

    Vector2 colliderStandSize;              //Size of the standing collider
    Vector2 colliderStandOffset;            //Offset of the standing collider
    Vector2 colliderCrouchSize;             //Size of the crouching collider
    Vector2 colliderCrouchOffset;           //Offset of the crouching collider

    const float smallAmount = .05f;         //A small amount used for hanging position


    void Start()
    {
        //Get a reference to the required components
        input = GetComponent<PlayerInput>();
        rigidBody = GetComponent<Rigidbody2D>();
        bodyCollider = GetComponent<BoxCollider2D>();

        //Record the original x scale of the player
        originalXScale = transform.localScale.x;

        //Record the player's height from the collider
        playerHeight = bodyCollider.size.y;

        //Record initial collider size and offset
        colliderStandSize = bodyCollider.size;
        colliderStandOffset = bodyCollider.offset;

        //Calculate crouching collider size and offset
        colliderCrouchSize = new Vector2(bodyCollider.size.x, bodyCollider.size.y / 2f);
        colliderCrouchOffset = new Vector2(bodyCollider.offset.x, bodyCollider.offset.y / 2f);
    }

    void FixedUpdate()
    {
        //Check the environment to determine status
        PhysicsCheck();

        //Process ground and air movements
        GroundMovement();
        MidAirMovement();
    }

    void PhysicsCheck()
    {
        //Start by assuming the player isn't on the ground and the head isn't blocked
        isOnGround = false;
        isHeadBlocked = false;

        //Cast rays for the left and right foot
        RaycastHit2D leftCheck = Raycast(new Vector2(-footOffset, 0f), Vector2.down, groundDistance);
        RaycastHit2D rightCheck = Raycast(new Vector2(footOffset, 0f), Vector2.down, groundDistance);

        //If either ray hit the ground, the player is on the ground
        if (leftCheck || rightCheck)
            isOnGround = true;

        //Cast the ray to check above the player's head
        RaycastHit2D headCheck = Raycast(new Vector2(0f, bodyCollider.size.y), Vector2.up, headClearance);

        //If that ray hits, the player's head is blocked
        if (headCheck)
            isHeadBlocked = true;

        //Determine the direction of the wall grab attempt
        Vector2 grabDir = new Vector2(direction, 0f);

        //Cast three rays to look for a wall grab
        RaycastHit2D blockedCheck = Raycast(new Vector2(footOffset * direction, playerHeight), grabDir, grabDistance);
        RaycastHit2D ledgeCheck = Raycast(new Vector2(reachOffset * direction, playerHeight), Vector2.down, grabDistance);
        RaycastHit2D wallCheck = Raycast(new Vector2(footOffset * direction, eyeHeight), grabDir, grabDistance);

        //If the player is off the ground AND is not hanging AND is falling AND
        //found a ledge AND found a wall AND the grab is NOT blocked...
        if (!isOnGround && !isHanging && rigidBody.velocity.y < 0f &&
            ledgeCheck && wallCheck && !blockedCheck)
        {
            //...we have a ledge grab. Record the current position...
            Vector3 pos = transform.position;
            //...move the distance to the wall (minus a small amount)...
            pos.x += (wallCheck.distance - smallAmount) * direction;
            //...move the player down to grab onto the ledge...
            pos.y -= ledgeCheck.distance;
            //...apply this position to the platform...
            transform.position = pos;
            //...set the rigidbody to static...
            rigidBody.bodyType = RigidbodyType2D.Static;
            //...finally, set isHanging to true
            isHanging = true;
        }
    }

    void GroundMovement()
    {
        //If currently hanging, the player can't move to exit
        if (isHanging)
            return;

        //Handle crouching input. If holding the crouch button but not crouching, crouch
        if (input.crouchHeld && !isCrouching && isOnGround)
            Crouch();
        //Otherwise, if not holding crouch but currently crouching, stand up
        else if (!input.crouchHeld && isCrouching)
            StandUp();
        //Otherwise, if crouching and no longer on the ground, stand up
        else if (!isOnGround && isCrouching)
            StandUp();

        //Calculate the desired velocity based on inputs
        float xVelocity = speed * input.horizontal;

        //If the sign of the velocity and direction don't match, flip the character
        if (xVelocity * direction < 0f)
            FlipCharacterDirection();

        //If the player is crouching, reduce the velocity
        if (isCrouching)
            xVelocity /= crouchSpeedDivisor;

        //Apply the desired velocity 
        rigidBody.velocity = new Vector2(xVelocity, rigidBody.velocity.y);

        //If the player is on the ground, extend the coyote time window
        if (isOnGround)
            coyoteTime = Time.time + coyoteDuration;
    }

    void MidAirMovement()
    {
        //If the player is currently hanging...
        if (isHanging)
        {
            //If crouch is pressed...
            if (input.crouchPressed)
            {
                //...let go...
                isHanging = false;
                //...set the rigidbody to dynamic and exit
                rigidBody.bodyType = RigidbodyType2D.Dynamic;
                return;
            }

            //If jump is pressed...
            if (input.jumpPressed)
            {
                Debug.Log("Jump Pressed");
                //...let go...
                isHanging = false;
                //...set the rigidbody to dynamic and apply a jump force...
                rigidBody.bodyType = RigidbodyType2D.Dynamic;
                rigidBody.AddForce(new Vector2(0f, hangingJumpForce), ForceMode2D.Impulse);
                //...and exit
                return;
            }
        }

        //If the jump key is pressed AND the player isn't already jumping AND EITHER
        //the player is on the ground or within the coyote time window...
        if (input.jumpPressed && !isJumping && (isOnGround || coyoteTime > Time.time))
        {
            //...check to see if crouching AND not blocked. If so...
            if (isCrouching && !isHeadBlocked)
            {
                //...stand up and apply a crouching jump boost
                StandUp();
                rigidBody.AddForce(new Vector2(0f, crouchJumpBoost), ForceMode2D.Impulse);
            }

            //...The player is no longer on the ground and is jumping...
            isOnGround = false;
            isJumping = true;

            //...record the time the player will stop being able to boost their jump...
            jumpTime = Time.time + jumpHoldDuration;

            //...add the jump force to the rigidbody...
            rigidBody.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);

            //...and tell the Audio Manager to play the jump audio
           // AudioManager.PlayJumpAudio();
        }
        //Otherwise, if currently within the jump time window...
        else if (isJumping)
        {
            //...and the jump button is held, apply an incremental force to the rigidbody...
            if (input.jumpHeld)
                rigidBody.AddForce(new Vector2(0f, jumpHoldForce), ForceMode2D.Impulse);

            //...and if jump time is past, set isJumping to false
            if (jumpTime <= Time.time)
                isJumping = false;
        }

        //If player is falling to fast, reduce the Y velocity to the max
        if (rigidBody.velocity.y < maxFallSpeed)
            rigidBody.velocity = new Vector2(rigidBody.velocity.x, maxFallSpeed);
    }

    void FlipCharacterDirection()
    {
        //Turn the character by flipping the direction
        direction *= -1;

        //Record the current scale
        Vector3 scale = transform.localScale;

        //Set the X scale to be the original times the direction
        scale.x = originalXScale * direction;

        //Apply the new scale
        transform.localScale = scale;
    }

    protected virtual void Crouch() //protected so it can be class specific
    {
        //The player is crouching
        isCrouching = true;

        //Apply the crouching collider size and offset
        bodyCollider.size = colliderCrouchSize;
        bodyCollider.offset = colliderCrouchOffset;
    }

    void StandUp()
    {
        //If the player's head is blocked, they can't stand so exit
        if (isHeadBlocked)
            return;

        //The player isn't crouching
        isCrouching = false;

        //Apply the standing collider size and offset
        bodyCollider.size = colliderStandSize;
        bodyCollider.offset = colliderStandOffset;
    }


    //These two Raycast methods wrap the Physics2D.Raycast() and provide some extra
    //functionality
    RaycastHit2D Raycast(Vector2 offset, Vector2 rayDirection, float length)
    {
        //Call the overloaded Raycast() method using the ground layermask and return 
        //the results
        return Raycast(offset, rayDirection, length, groundLayer);
    }

    RaycastHit2D Raycast(Vector2 offset, Vector2 rayDirection, float length, LayerMask mask)
    {
        //Record the player's position
        Vector2 pos = transform.position;

        //Send out the desired raycast and record the result
        RaycastHit2D hit = Physics2D.Raycast(pos + offset, rayDirection, length, mask);

        //If we want to show debug raycasts in the scene...
        if (drawDebugRaycasts)
        {
            //...determine the color based on if the raycast hit...
            Color color = hit ? Color.red : Color.green;
            //...and draw the ray in the scene view
            Debug.DrawRay(pos + offset, rayDirection * length, color);
        }

        //Return the results of the raycast
        return hit;
    }

    public int GetDirection()
    {
        return direction;
    }

}
