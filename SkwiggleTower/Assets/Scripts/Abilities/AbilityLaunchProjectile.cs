using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityLaunchProjectile : Ability
{
    [Space(30)]


    public Rigidbody2D projectile;

    public float impulse;

    public Vector2 offset;

    new void Start()
    {
        base.Start();

    }


    public override void Cast()
    {
        if (onCooldown) return;
        base.Cast();
        LaunchProjectile();

    }

    public void LaunchProjectile()
    {
        // instantiate an instance of the projectile
        var projRb = Instantiate(projectile, new Vector2(transform.position.x, transform.position.y) + (offset * new Vector2(characterMovement.faceDirection, 1)), Quaternion.Euler(0, characterMovement.faceDirection == 1 ? 0 : 180,0));
        // add an impulse to the projectile based on the direction that the character is facing
        projRb.AddForce(Vector2.right * characterMovement.faceDirection * impulse);


        // get the projectile's script
        var projAttack = projRb.GetComponent<RockAttack>();
        projAttack.SetLayer(LayerMask.LayerToName(gameObject.layer), GetComponent<Collider2D>());

        // relay the damage
        projAttack.damage = baseDamage;

        projAttack.notifyPlayer = UltCharge;
    }

    private void OnDrawGizmos()
    {
        if(characterMovement)
            Gizmos.DrawWireSphere(new Vector2(transform.position.x, transform.position.y) + (offset * new Vector2(characterMovement.faceDirection, 1)), 0.1f);
    }


    public void UltCharge()
    {
        //if (characterStats is PlayerStats)
        //{
        //    (characterStats as PlayerStats).currentUlt += 5;
        //    (characterStats as PlayerStats).currentUlt = Mathf.Clamp((characterStats as PlayerStats).currentUlt, 0, (characterStats as PlayerStats).maxUlt);
        //}
    }

}
