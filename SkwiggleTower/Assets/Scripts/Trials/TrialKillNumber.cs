using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TrialKillNumber : Trial
{
    // Matt Thompson
    // Last Modified: 6/10/2019


    /// <summary>
    /// Number of enemy kills needed to win trial
    /// </summary>
    public int killsNeeded;
   
    public override void Start()
    {
        base.Start();

        // you MUST place logic after the base method, since important things such as the roomManager reference are established there

        if (killsNeeded == 1)
        {
            base.trialName = ("KILL " + killsNeeded.ToString() + " ENEMY!");
        }
        else
        {
            base.trialName = ("KILL " + killsNeeded.ToString() + " ENEMIES!");
        }


    }

    // Method one of checking if all enemies defeated, essentially just checks if the number of enemies killed 
    public override void UpdateLogic()
    {
        base.UpdateLogic();

        // Increase enemies killed with K (For testing), only if enemies killed is fewer than / equal the amount of kills needed
        if (Input.GetKeyDown(KeyCode.K) && RoomManager.instance.amtOfEnemiesKilled <= killsNeeded)
        {
            RoomManager.instance.amtOfEnemiesKilled++;
            Debug.Log("Enemies Killed: " + RoomManager.instance.amtOfEnemiesKilled.ToString());
        }

        // If the number of kills needed to win is greater than the maxAMount of enemies possible, increase maxAmount to accomodate
        if (killsNeeded > RoomManager.instance.maxAmountOfEnemies)
        {
            RoomManager.instance.maxAmountOfEnemies = killsNeeded; 
        }

        // If number of enemies killed is greater than / equal to max number of enemies (i.e. all enemies have been defeated.
        if (RoomManager.instance.amtOfEnemiesKilled >= killsNeeded)
        {
            NotifyTrialComplete(true);
        }
    }
}
