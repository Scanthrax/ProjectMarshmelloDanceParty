using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockAttack : MonoBehaviour
{
    public AudioSource rockImpact;

    string thisLayer, opposingLayer;

    Collider2D ignoreCollider;

    Rigidbody2D rb;

    public AudioClip whooshSound; 


    public int damage;

    public string projectile;


    public Action notifyPlayer;

    private void Start()
    {
        Destroy(gameObject,3f);


        opposingLayer = thisLayer == "Player" ? "Enemy" : "Player";

        // determine which layer this projectile will reside on; based on whether an enemy is firing or a player
        gameObject.layer = LayerMask.NameToLayer(thisLayer + " Projectile");

        rockImpact.clip = whooshSound;
        rockImpact.Play();

        //Physics2D.IgnoreCollision(ignoreCollider, GetComponent<Collider2D>());

        rb = GetComponent<Rigidbody2D>();
    }




    private void OnCollisionEnter2D(Collision2D collision)
    {
        var impact = collision.gameObject.GetComponent<ImpactSound>();

        if (LayerMask.LayerToName(collision.gameObject.layer) == opposingLayer)
        {
            var temp = collision.gameObject.GetComponent<BaseCharacter>();
            if (temp)
            {
                temp.RecieveDamage(damage);
                print(opposingLayer + " hit!");


                //notifyPlayer.Invoke();

                //if((temp.transform.position.x - transform.position.x) * temp.direction  > 0)
                //{
                //    var enemyAI = temp.GetComponent<EnemyAI>();
                //    if(enemyAI)
                //        enemyAI.ChangeDirection();
                //}

            }
        }

        rockImpact.clip = impact ? impact.GetSound(projectile) : null;
        rockImpact.Play();

        gameObject.layer = LayerMask.NameToLayer("Debris");
        if (rb) rb.gravityScale = 1f;
    }

    public void SetLayer(string layer, Collider2D col)
    {
        thisLayer = layer;
        ignoreCollider = col;
    }



}
