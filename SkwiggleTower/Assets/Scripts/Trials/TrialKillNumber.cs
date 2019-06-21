using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TrialKillNumber : Trial
{
    // Matt Thompson
    // Last Modified: 6/10/2019

    /// <summary>
    /// Enemies killed so far in current trial
    /// </summary>
    public int enemiesKilled;

    /// <summary>
    /// Number of enemy kills needed to win trial
    /// </summary>
    public int killsNeeded = 0;
   
    public override void Start()
    {
        base.Start();

        // you MUST place logic after the base method, since important things such as the roomManager reference are established there

        // resets kill counter and all enemies killed flag on start (so as to not carry progress to other trials)
        enemiesKilled = 0;
        base.trialName = ("KILL " + killsNeeded.ToString() + " ENEMIES!");
    }

    // Method one of checking if all enemies defeated, essentially just checks if the number of enemies killed 
    public override void UpdateLogic()
    {
        base.UpdateLogic();

        // Increase enemies killed with K (For testing), only if enemies killed is fewer than / equal the amount of kills needed
        if (Input.GetKeyDown(KeyCode.K) && enemiesKilled <= killsNeeded)
        {
            enemiesKilled++;
            Debug.Log("Enemies Killed: " + enemiesKilled.ToString());
        }

        // If the number of kills needed to win is greater than the maxAMount of enemies possible, increase maxAmount to accomodate
        if (killsNeeded > RoomManager.instance.maxAmount)
        {
            RoomManager.instance.maxAmount = killsNeeded; 
        }

        // If number of enemies killed is greater than / equal to max number of enemies (i.e. all enemies have been defeated.
        if (enemiesKilled >= killsNeeded)
        {
            NotifyTrialComplete(true);
        }
    }
}
