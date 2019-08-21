using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BaseProjectile : MonoBehaviour, IPooledObject
{
    public AudioSource projectileAudioSource;

    public string thisLayer, opposingLayer;

    Collider2D ignoreCollider;

    public Rigidbody2D rb;

    public AudioClip whooshSound;

    public string projectile;

    public Ability ability;


    public delegate void DamageHandler(BaseCharacter character);
    public event DamageHandler DamageEvent;


    public bool persist;

    public void Start()
    {
        // make gravity persist between spawns?


        projectileAudioSource.clip = whooshSound;
        projectileAudioSource.Play();

        //Physics2D.IgnoreCollision(ignoreCollider, GetComponent<Collider2D>());

        rb = GetComponent<Rigidbody2D>();

    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        

        if (LayerMask.LayerToName(collision.gameObject.layer) == opposingLayer)
        {
            print("projectile hit!");
            var temp = collision.gameObject.GetComponentInChildren<BaseCharacter>();
            if (temp)
            {
                ability.DealDamage(temp);
            }
        }


        gameObject.layer = LayerMask.NameToLayer("Debris");
        if (rb) rb.gravityScale = 1f;
    }

    public void SetLayer(string layer, Collider2D col)
    {
        thisLayer = layer;
        ignoreCollider = col;

        opposingLayer = thisLayer == "Player" ? "Enemy" : "Player";

        // determine which layer this projectile will reside on; based on whether an enemy is firing or a player
        gameObject.layer = LayerMask.NameToLayer(thisLayer + " Projectile");

    }


    public IEnumerator BackToPoolAfterTime(float t)
    {
        yield return new WaitForSeconds(t);
        ObjectPoolManager.instance.BackToPool(transform, true);
    }


    public virtual void OnObjectSpawn()
    {
        StartCoroutine(BackToPoolAfterTime(7f));
        projectileAudioSource.Play();
    }

    public virtual void OnObjectDespawn()
    {
        
    }

}
