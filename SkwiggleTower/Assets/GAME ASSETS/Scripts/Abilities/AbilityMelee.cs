using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityMelee : Ability
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


    public LayerMask whatAreEnemies;


    /// <summary>
    /// The array that stores each collider hit by the attack boxcast
    /// </summary>
    protected Collider2D[] enemiesInRange;

    /// <summary>
    /// Keeps track of enemies that have been hit by the melee attack
    /// </summary>
    protected List<BaseCharacter> enemiesHit;

    protected bool attackBoxActive;


    public delegate void OnKillHandler();
    public event OnKillHandler OnKillEvent;


    public bool alwaysActive;

    public override void Start()
    {
        base.Start();

        enemiesInRange = new Collider2D[10];

        enemiesHit = new List<BaseCharacter>();


        AbilityEndEvent += DisableAttackBox;
        //isRunning = false;

        if (alwaysActive)
            Cast();

    }


    public override void Cast()
    {
        base.Cast();
        StartCoroutine(Melee());
    }



    public IEnumerator Melee()
    {
        if (!alwaysActive)
        {
            // activate the attack hitbox upon starting
            attackBoxActive = true;

            // keep track of the character's direction
            var dir = characterMovement.faceDirection;

            // clear the list of enemies (this can go either at the end of the attack or the beginning)
            enemiesHit.Clear();

            // this occurs during the duration of the attack
            while (attackBoxActive)
            {
                var amtOfEnemies = Physics2D.OverlapBoxNonAlloc(transform.position + new Vector3(attackPos.x * dir, attackPos.y), attackRange, 0, enemiesInRange, whatAreEnemies);
                for (int j = 0; j < amtOfEnemies; j++)
                {
                    var enemy = enemiesInRange[j].GetComponent<BaseCharacter>();
                    if (!enemy || enemy == characterMovement.character || enemiesHit.Contains(enemy)) continue;

                    enemiesHit.Add(enemy);

                    DealDamage(enemy);
                }

                if (oneShot) break;

                yield return null;
            }
            characterMovement.input.EnableControls();

            //isRunning = false;
        }

        else
        {
            //print("SKULL ATTACKING");
            // activate the attack hitbox upon starting
            attackBoxActive = true;

            

            enemiesInRange = new Collider2D[10];

            // this occurs during the duration of the attack
            while (attackBoxActive)
            {
                // keep track of the character's direction
                var dir = characterMovement.faceDirection;

                var amtOfEnemies = Physics2D.OverlapBoxNonAlloc(transform.position + new Vector3(attackPos.x * dir, attackPos.y), attackRange, 0, enemiesInRange, whatAreEnemies);
                for (int j = 0; j < amtOfEnemies; j++)
                {
                    var enemy = enemiesInRange[j].GetComponent<BaseCharacter>();
                    if (!enemy || enemy == characterMovement.character) continue;


                    DealDamage(enemy);
                }

                //print("SKULL ATTACKING");
                yield return null;
            }
        }
    }

    public void DisableAttackBox()
    {
        if (attackBoxActive)
        {
            attackBoxActive = false;
        }
    }




    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position + new Vector3(attackPos.x * characterMovement.faceDirection, attackPos.y), attackRange);
    }


}
