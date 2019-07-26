using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityLaunchProjectile : Ability
{
    [Space(30)]


    public Rigidbody2D projectile;

    public float impulse;

    CharacterStats characterStats;

    public int direction { get { return characterStats ? characterStats.direction : 1; } }

    public Vector2 offset;

    new void Start()
    {
        base.Start();

        characterStats = GetComponent<CharacterStats>();
    }


    public override void Cast()
    {
        if (onCooldown) return;
        base.Cast();
        LaunchProjectile();
        timer = 0f;

    }

    public void LaunchProjectile()
    {
        var projRb = Instantiate(projectile, new Vector2(transform.position.x, transform.position.y) + (offset * new Vector2(direction, 1)), Quaternion.Euler(0, characterStats.direction == 1 ? 0 : 180,0));
        projRb.GetComponent<RockAttack>().SetLayer(LayerMask.LayerToName(gameObject.layer),GetComponent<Collider2D>());
        projRb.AddForce(Vector2.right * direction * impulse);
        projRb.gameObject.layer = LayerMask.NameToLayer(gameObject.layer == LayerMask.NameToLayer("Player") ? "Player Projectile" : "Enemy Projectile");
        projRb.GetComponent<RockAttack>().damage = damage;
        projRb.GetComponent<RockAttack>().notifyPlayer = UltCharge;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(new Vector2(transform.position.x, transform.position.y) + (offset * new Vector2(direction,1)), 0.1f);
    }


    public void UltCharge()
    {
        if (characterStats is PlayerStats)
        {
            (characterStats as PlayerStats).currentUlt += 5;
            (characterStats as PlayerStats).currentUlt = Mathf.Clamp((characterStats as PlayerStats).currentUlt, 0, (characterStats as PlayerStats).maxUlt);
        }
    }

}
