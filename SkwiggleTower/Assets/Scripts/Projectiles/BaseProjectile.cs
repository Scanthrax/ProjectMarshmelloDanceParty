﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BaseProjectile : MonoBehaviour
{
    public AudioSource rockImpact;

    string thisLayer, opposingLayer;

    Collider2D ignoreCollider;

    public Rigidbody2D rb;

    public AudioClip whooshSound;

    public string projectile;

    public Ability ability;


    public delegate void DamageEvent(BaseCharacter character);
    public event DamageEvent damageEvent;


    public bool persist;

    private void Start()
    {
        // make gravity persist between spawns?

        opposingLayer = thisLayer == "Player" ? "Enemy" : "Player";

        // determine which layer this projectile will reside on; based on whether an enemy is firing or a player
        gameObject.layer = LayerMask.NameToLayer(thisLayer + " Projectile");

        rockImpact.clip = whooshSound;
        rockImpact.Play();

        //Physics2D.IgnoreCollision(ignoreCollider, GetComponent<Collider2D>());

        rb = GetComponent<Rigidbody2D>();

        damageEvent += ability.DealDamage;

    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        var impact = collision.gameObject.GetComponent<ImpactSound>();

        if (LayerMask.LayerToName(collision.gameObject.layer) == opposingLayer)
        {
            var temp = collision.gameObject.GetComponent<BaseCharacter>();
            if (temp)
            {
                damageEvent(temp);
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