using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityDash : AbilityMelee
{
    /// <summary>
    /// The distance of the dash
    /// </summary>
    public float dashDist;

    /// <summary>
    /// The duration of the dash
    /// </summary>
    public float dashDuration;

    /// <summary>
    /// Stores the gravity of the character
    /// </summary>
    float prevGravity;

    /// <summary>
    /// Stores the layer of the character
    /// </summary>
    int prevLayer;

    Rigidbody2D rigidBody;

    // Start is called before the first frame update
    new void Start()
    {
        base.Start();

        rigidBody = characterMovement.rigidBody;

        prevLayer = gameObject.layer;

    }




    public override void Cast()
    {
        base.Cast();
        StartCoroutine(Dash());
    }




    public void StandStill()
    {
        prevGravity = rigidBody.gravityScale;
        rigidBody.gravityScale = 0f;
        rigidBody.velocity = Vector2.zero;
    }

    public IEnumerator Dash()
    {
        var dir = characterMovement.faceDirection;
        var time = Time.fixedDeltaTime;
        rigidBody.gravityScale = 0f;
        rigidBody.velocity = Vector2.zero;
        gameObject.layer = LayerMask.NameToLayer("Debris");
        
        for (float i = 0; i < dashDuration; i += time)
        {
            rigidBody.MovePosition((Vector2)transform.position + new Vector2((dashDist/dashDuration) * time * dir, 0));
            yield return null;
        }
        EndMelee();
        rigidBody.gravityScale = prevGravity;
        gameObject.layer = prevLayer;
        print("end of dash!");
        yield break;
    }





}
