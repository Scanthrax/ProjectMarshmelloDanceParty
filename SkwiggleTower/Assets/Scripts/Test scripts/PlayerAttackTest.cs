﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackTest : MonoBehaviour
{

    private float attackCooldown;
    public float startAttackCooldown;

    public Transform attackPos;
    public LayerMask whatAreEnemies;
    public float attackRange;
    public int damage;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        ////once 0, player can attack
        //if (attackCooldown <= 0)
        //{
        //    if (Input.GetMouseButtonDown(0))
        //    {
        //        Collider2D[] enemiesInRange = Physics2D.OverlapCircleAll(attackPos.position, attackRange, whatAreEnemies);
        //        for (int i = 0; i < enemiesInRange.Length; i++)
        //        {
        //            //Uses a method in JacobEnemyTest.cs for enemy to take damage
        //            //enemiesInRange[i].GetComponent<JacobEnemyTest>().takeDamage(damage);
        //        }
        //    }
        //    attackCooldown = startAttackCooldown;
        //}
        //else
        //{
        //    attackCooldown -= Time.deltaTime;
        //}
    }


    public void Attack()
    {
        Collider2D[] enemiesInRange = Physics2D.OverlapCircleAll(attackPos.position, attackRange, whatAreEnemies);
        for (int i = 0; i < enemiesInRange.Length; i++)
        {
            //Uses a method in JacobEnemyTest.cs for enemy to take damage
            var canBeDamaged = enemiesInRange[i].GetComponent<CharacterStats>();

            if (canBeDamaged)
            {
                canBeDamaged.TakeDamage(damage);

                // RON: added impulse
                canBeDamaged.GetComponent<Rigidbody2D>().AddForce(Vector2.right * -1000f + Vector2.up * 200f);
            }

        }
    }


    //This method shows the attack radius, which can be manipulated in Unity
    private void OnDrawGizmosSelected()
    {
        if (attackPos)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(attackPos.position, attackRange);
        }
    }

}
