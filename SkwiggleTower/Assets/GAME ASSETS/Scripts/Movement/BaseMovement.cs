using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BaseCharacter))]
public class BaseMovement : MonoBehaviour
{
    /// <summary>
    /// 1 means right; -1 means left
    /// </summary>
    public int faceDirection;

    /// <summary>
    /// The character's movement speed
    /// </summary>
    public float movementSpeed;

    public float jumpForce;


    [Header("Environment Check Properties")]
    public float groundDistance;      //Distance player is considered to be on the ground
    public float groundDistOffset;
    public float wallDist;
    public float wallOffset;
    public LayerMask groundLayer;           //Layer of the ground

    [Header("Status Flags")]
    public bool isOnGround;                 //Is the player on the ground?

    public BaseInput input;                      //The current inputs for the player
    public Collider2D bodyCollider;             //The collider component
    public Rigidbody2D rigidBody;                  //The rigidbody component
    public Animator animator;

    Vector2 size;
    public bool jumped, doubleJumped;


    public BaseCharacter character;


    [Range(2f,8f)]
    public float fallSpeed;

    [HideInInspector]
    public float horizontalAxis;





    void FixedUpdate()
    {
        //Check the environment to determine status
        PhysicsCheck();



        rigidBody.velocity = new Vector2(movementSpeed * horizontalAxis, rigidBody.velocity.y);


        animator.SetFloat("moveSpeed", Mathf.Abs(rigidBody.velocity.x));



    }

    void PhysicsCheck()
    {
        // store 1 raycast hit; it 'should' be the ground
        // we can worry about other cases later; i.e. cases where we're on top of other characters
        RaycastHit2D[] hits = new RaycastHit2D[1];

        // left & right raycasts
        // a return value of 1 means that there has been a hit with the raycast
        var leftFootRay = Physics2D.RaycastNonAlloc(
            (Vector2)transform.position + new Vector2(-size.x * 0.5f, -size.y * 0.5f + groundDistOffset),
            Vector2.down,
            hits,
            groundDistance + groundDistOffset);
        var rightFootRay = Physics2D.RaycastNonAlloc(
            (Vector2)transform.position + new Vector2(size.x * 0.5f, -size.y * 0.5f + groundDistOffset),
            Vector2.down,
            hits,
            groundDistance + groundDistOffset);



        var lwall = false;
        var rwall = false;

        var leftWallRay = Physics2D.RaycastNonAlloc(
            (Vector2)transform.position + new Vector2(0f, wallOffset),
            Vector2.left,
            hits,
            wallDist,
            groundLayer);

        if(leftWallRay == 1)
            lwall = true;

        var rightWallRay = Physics2D.RaycastNonAlloc(
            (Vector2)transform.position + new Vector2(0f, wallOffset),
            Vector2.right,
            hits,
            wallDist,
            groundLayer);

        if (rightWallRay == 1)
            rwall = true;


        horizontalAxis = Mathf.Clamp(horizontalAxis, lwall ? 0 : horizontalAxis, rwall ? 0 : horizontalAxis);


        // there is ground beneath the character if one of the raycasts returns 1
        var onGround = leftFootRay == 1 || rightFootRay == 1;

        // if there is a difference between what's been recorded & what we've detected this frame, it means there is a transition
        // the transitions are going to be Landing vs Off the ground
        // NOTE: off the ground can either mean a jump, or falling off of a ledge
        if (isOnGround != onGround)
        {
            // LANDING
            if (onGround)
            {
                //print("landed");

                // reset jump values when we land
                jumped = false;
                doubleJumped = false;

                character.footstepSource.clip = character.landingClip;
                character.footstepSource.Play();
            }
            // OFF THE GROUND
            else
            {
                //print("off the ground!");
                // we say that we've jumped so that way only the double-jump can be triggered while we are airborne
                jumped = true;
            }

            // we record that we've transitioned our binary ground state
            isOnGround = onGround;

            if (animator)
                animator.SetBool("isOnGround", isOnGround);
        }

    }

    public virtual void Jump()
    {
        if ((isOnGround && !jumped))
        {
            if (!jumped)
                jumped = true;

            rigidBody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);

        }
    }

    void MidAirMovement()
    {
        
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawLine((Vector2)transform.position + new Vector2(-size.x*0.5f,-size.y * 0.5f + groundDistOffset), (Vector2)transform.position + new Vector2(-size.x * 0.5f, -size.y * 0.5f - groundDistance));
        Gizmos.DrawLine((Vector2)transform.position + new Vector2(size.x * 0.5f, -size.y * 0.5f + groundDistOffset), (Vector2)transform.position + new Vector2(size.x * 0.5f, -size.y * 0.5f - groundDistance));

        Gizmos.DrawLine((Vector2)transform.position + new Vector2(0f,wallOffset), (Vector2)transform.position + new Vector2(0f + wallDist, wallOffset));
        Gizmos.DrawLine((Vector2)transform.position + new Vector2(0f, wallOffset), (Vector2)transform.position + new Vector2(0f - wallDist, wallOffset));
    }

    private void OnValidate()
    {
        if(bodyCollider)
            size = (bodyCollider as CapsuleCollider2D).size;

        if (rigidBody)
        {
            rigidBody.gravityScale = fallSpeed;
            jumpForce = fallSpeed*2.4f + 6.2f;
        }


    }
}
