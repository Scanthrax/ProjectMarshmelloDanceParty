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


    //bool isRunning;

    public override void Start()
    {
        base.Start();

        enemiesInRange = new Collider2D[10];

        enemiesHit = new List<BaseCharacter>();


        abilityEndEvent += DisableAttackBox;
        //isRunning = false;

    }


    public override void Cast()
    {
        base.Cast();
        StartCoroutine(Melee());
    }



    public IEnumerator Melee()
    {
        //if(isRunning)
        //{
        //    print("This is already running?");
        //    yield break;
        //}
        //isRunning = true;
        //print("doing melee");
        attackBoxActive = true;
        var dir = characterMovement.faceDirection;
        enemiesHit.Clear();

        while (attackBoxActive)
        {
            var amtOfEnemies = Physics2D.OverlapBoxNonAlloc(transform.position + new Vector3(attackPos.x * dir, attackPos.y), attackRange, 0, enemiesInRange,whatAreEnemies);
            for (int j = 0; j < amtOfEnemies; j++)
            {
                var enemy = enemiesInRange[j].GetComponent<BaseCharacter>();
                if (!enemy || enemy == characterMovement.character || enemiesHit.Contains(enemy)) continue;
                
                enemiesHit.Add(enemy);

                DealDamage(enemy);

                //var hitSound = enemy.GetComponent<ImpactSound>();
                //if(hitSound)
                //    hitSound.
            }
            yield return null;
        }
        characterMovement.input.EnableControls();

        //isRunning = false;
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
