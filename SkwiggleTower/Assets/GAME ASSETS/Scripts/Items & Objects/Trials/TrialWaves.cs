using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TrialWaves : Trial
{
    // Matt Thompson
    // Last Modified: 8/9/2019

    /// <summary>
    /// Enemies killed so far in current wave
    /// </summary>
    public int enemiesKilledThisWave;

    /// <summary>
    /// Number of waves of enemies that must be defeated to win
    /// </summary>
    public int maxNumWaves;

    /// <summary>
    /// Current wave player is on, defaults to one
    /// </summary>
    public int currentWave = 1;

    /// <summary>
    /// Number of enemy kills needed to win trial, should be set in inspector, defaulted to 3 (?)
    /// </summary>
    public int killsNeededThisWave = 0;
   
    public override void Start()
    {
        base.Start();

        // you MUST place logic after the base method, since important things such as the roomManager reference are established there

        // resets kill counter and all enemies killed flag on start (so as to not carry progress to other trials)
        enemiesKilledThisWave = 0;
        if (maxNumWaves == 1)
        {
            base.trialName = ("DEFEAT " + maxNumWaves.ToString() + " WAVES!");
        }
        else
        {
            base.trialName = ("DEFEAT " + maxNumWaves.ToString() + " WAVES!");
        }
        
    }

    // Method one of checking if all enemies defeated, essentially just checks if the number of enemies killed 
    public override void UpdateLogic()
    {
        base.UpdateLogic();

        // Increase enemies killed with K (For testing), only if enemies killed is fewer than / equal the amount of kills needed
        if (Input.GetKeyDown(KeyCode.K) && enemiesKilledThisWave <= killsNeededThisWave)
        {
            enemiesKilledThisWave++;
            Debug.Log("Enemies Killed: " + enemiesKilledThisWave.ToString());
        }

        //// If the number of kills needed to win is greater than the maxAMount of enemies possible, increase maxAmount to accomodate
        //if (killsNeededThisWave > RoomManager.instance.maxAmount)
        //{
        //    RoomManager.instance.maxAmount = killsNeededThisWave;
        //}

        // Script for completing a wave
        // If number of enemies killed is greater than / equal to max number of enemies (i.e. all enemies have been defeated)
        if (enemiesKilledThisWave >= killsNeededThisWave)
        {
            // wave is completed, kills reset
            currentWave++;
            enemiesKilledThisWave = 0;

            // adds 1-3 enemies to the next wave, could be changed to a set amount or randoms changed later
            killsNeededThisWave += Random.Range(1, 3);
        }

        // player completing final wave, ends trial
        if ((enemiesKilledThisWave >= killsNeededThisWave) && currentWave == maxNumWaves) 
        {
            NotifyTrialComplete(true);
        }
    }
}
