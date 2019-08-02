using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityDash : Ability
{
    /// <summary>
    /// Position of the attack boxcast
    /// </summary>
    [Space(30)]
    public Vector2 attackPos;

    /// <summary>
    /// Width & Height of the attack boxcast
    /// </summary>
    public Vector2 attackRange;

    Rigidbody2D rigidBody;

    public LayerMask whatAreEnemies;


    /// <summary>
    /// The array that stores each collider hit by the attack boxcast
    /// </summary>
    Collider2D[] enemiesInRange;

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

    /// <summary>
    /// Keeps track of enemies that have been hit by the melee attack
    /// </summary>
    List<BaseCharacter> enemiesHit;

    // Start is called before the first frame update
    new void Start()
    {
        base.Start();

        rigidBody = characterMovement.rigidBody;

        enemiesInRange = new Collider2D[10];

        prevLayer = gameObject.layer;

        enemiesHit = new List<BaseCharacter>();
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
        enemiesHit.Clear();

        for (float i = 0; i < dashDuration; i += time)
        {
            
            var amtOfEnemies = Physics2D.OverlapBoxNonAlloc(transform.position + new Vector3(attackPos.x * dir, attackPos.y), attackRange, 0, enemiesInRange);
            for (int j = 0; j < amtOfEnemies; j++)
            {
                var enemy = enemiesInRange[j].GetComponent<BaseCharacter>();
                if (!enemy || enemy == characterMovement.character || enemiesHit.Contains(enemy)) continue;
                Debug.Log("Hit enemy!");
                enemy.RecieveDamage(0);
                enemiesHit.Add(enemy);
            }



            rigidBody.MovePosition((Vector2)transform.position + new Vector2((dashDist/dashDuration) * time * dir, 0));
            yield return null;
        }
        characterMovement.animator.SetBool("AbilityActive", false);
        rigidBody.gravityScale = prevGravity;
        characterMovement.input.EnableControls();
        gameObject.layer = prevLayer;
        print("end of dash!");
        yield break;
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position + new Vector3(attackPos.x * characterMovement.faceDirection, attackPos.y), attackRange);
    }


}
