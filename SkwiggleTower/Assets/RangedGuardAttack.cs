using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedGuardAttack : MonoBehaviour
{
    EnemyStats enemy;


    private void Start()
    {
        enemy = GetComponentInParent<EnemyStats>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {

        var player = collision.GetComponent<PlayerStats>();

        if(player)
            enemy.primary.Cast();
    }
}
