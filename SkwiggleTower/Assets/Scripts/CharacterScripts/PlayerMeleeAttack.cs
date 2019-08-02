using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMeleeAttack : Ability
{
    public float startAttackCooldown;

    public Vector2 attackPos;
    public LayerMask whatAreEnemies;
    public Vector2 attackRange;

    public List<Collider2D> enemiesHit;


    public bool startMelee;


    float prevImpulse;

    public override void Start()
    {
        base.Start();
        startMelee = false;
    }

    // Update is called once per frame
    void Update()
    {
        //once 0, player can attack

        if(startMelee)
        {
            Collider2D[] enemiesInRange = Physics2D.OverlapBoxAll(transform.position + new Vector3(attackPos.x * characterMovement.faceDirection,attackPos.y), attackRange, whatAreEnemies);
            for(int i = 0; i < enemiesInRange.Length; i++)
            {
                var characterStats = enemiesInRange[i].GetComponent<CharacterStats>();

                if (enemiesHit.Contains(enemiesInRange[i]) || !characterStats || enemiesInRange[i] == GetComponent<Collider2D>() || enemiesInRange[i].gameObject.layer == LayerMask.NameToLayer("Enemy")) continue;



                characterStats.TakeDamage(baseDamage);
                Debug.Log("Got 'em");

                enemiesHit.Add(enemiesInRange[i]);
            }
        }

    }


    public override void Cast()
    {
        if (onCooldown) return;
        base.Cast();
        GetComponent<Animator>().SetTrigger("PrimaryTrigger");

        var enemyAI = GetComponent<EnemyAI>();

        prevImpulse = enemyAI.impulse;
        enemyAI.impulse = 0f;
    }

    public void StartMelee()
    {
        startMelee = true;
        enemiesHit.Clear();
    }
    public void EndMelee()
    {
        startMelee = false;
    }


    public void SetImpulseBack()
    {
        var enemyAI = GetComponent<EnemyAI>();
        enemyAI.impulse = prevImpulse;
    }


    //This method shows the attack radius, which can be manipulated in Unity
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        int i = characterMovement.faceDirection == 1 ? characterMovement.faceDirection : 1;
        Gizmos.DrawWireCube(transform.position + new Vector3(attackPos.x * i, attackPos.y), attackRange);
    }

}
