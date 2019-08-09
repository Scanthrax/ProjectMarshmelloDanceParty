﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityLaunchProjectile : Ability
{
    [Space(30)]


    public BaseProjectile projectile;

    public float impulse;

    public Vector2 offset;

    new void Start()
    {
        base.Start();

    }


    public override void Cast()
    {
        base.Cast();
        LaunchProjectile();

    }

    public void LaunchProjectile()
    {
        // instantiate an instance of the projectile
        var projectile = Instantiate(this.projectile, new Vector2(transform.position.x, transform.position.y) + (offset * new Vector2(characterMovement.faceDirection, 1)), Quaternion.Euler(0, characterMovement.faceDirection == 1 ? 0 : 180,0));
        // add an impulse to the projectile based on the direction that the character is facing
        projectile.rb.AddForce(Vector2.right * characterMovement.faceDirection * impulse);

        projectile.SetLayer(LayerMask.LayerToName(gameObject.layer), GetComponent<Collider2D>());
        projectile.ability = this;

    }

    private void OnDrawGizmos()
    {
        if(characterMovement)
            Gizmos.DrawWireSphere(new Vector2(transform.position.x, transform.position.y) + (offset * new Vector2(characterMovement.faceDirection, 1)), 0.1f);
    }



}
