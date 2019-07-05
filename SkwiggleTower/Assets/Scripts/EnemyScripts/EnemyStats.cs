using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Val Script 7/1/19
/// </summary>
public class EnemyStats : CharacterStats
{
    //Amount of health and xp the player gains by killing them 
    int healthGained = 20;
    int xpGained = 100;

    public override void Die()
    {
        base.Die();

        //Reward Player
        PlayerManager.instance.charStats.Heal(healthGained); //references character stats in the Playermanager Health
        PlayerManager.instance.charStats.gainXP(xpGained); //references character stats in the Playermanager XP;
        Debug.Log("Enemy Died");
        //Play Animation 
        Destroy(gameObject);
    }
}
