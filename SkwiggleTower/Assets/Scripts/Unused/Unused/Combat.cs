using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Valarie Script: holds all the scripts associated with Attacks, Attack delay. UML created 6.12
/// </summary>
[RequireComponent(typeof(CharacterStats))]
public class Combat : MonoBehaviour
{
    CharacterStats myStats;
    public float attackSpeed = 1f;
    private float attackCooldown = 0f;
    const float combatCooldown = 5f; //if character hasn't performed combat in this number of secs.it's no longer in combat
    float lastAttackTime;

    private float animDelay = .6f;
    public bool InCombat { get; private set; }
    public event System.Action OnAttack; //Creates a Delegate to Manage our events

    private void Start()
    {
        myStats = GetComponent<CharacterStats>(); //refers to saved values in Character stats
    }

    void Update()
    {
        attackCooldown -= Time.deltaTime;
        if (Time.time - lastAttackTime > combatCooldown)
        {
            InCombat = false;
        }

    }

    public void Attack(CharacterStats targetStats)
    {
        if (targetStats != null)
        {
            if (attackCooldown != 0f) //Checks to see if we can attack after cooldown
            {
                StartCoroutine(DoDamage(targetStats, animDelay));

                OnAttack?.Invoke(); //If not subscribed to the delegate it subscribes it to the OnAttack delegate

                attackCooldown = 1f / attackSpeed;
                InCombat = true;
                lastAttackTime = Time.time;
            }
        }
    }

    IEnumerator DoDamage(CharacterStats stats, float delay)
    {
        if (stats != null)
        {
            yield return new WaitForSeconds(delay);
            stats.TakeDamage(myStats.damage.GetValue());//finds out what the players stats are with modifiers
        }
        else
        {
            //if(stats.currentHealth <= 0)

            InCombat = false;
        }
    }
}
