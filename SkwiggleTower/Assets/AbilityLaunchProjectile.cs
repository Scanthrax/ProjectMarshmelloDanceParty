using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityLaunchProjectile : Ability
{
    public Rigidbody2D projectile;

    public float impulse;

    CharacterStats characterStats;

    public int direction { get { return characterStats ? characterStats.direction : 1; } }

    public Vector2 offset;

    private void Start()
    {
        characterStats = GetComponent<CharacterStats>();
    }


    public void LaunchProjectile()
    {
        var projRb = Instantiate(projectile, new Vector2(transform.position.x, transform.position.y) + (offset * new Vector2(direction, 1)), Quaternion.identity);
        projRb.AddForce(Vector2.right * direction * impulse);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(new Vector2(transform.position.x, transform.position.y) + (offset * new Vector2(direction,1)), 0.1f);
    }

}
