using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TrialKillEnemies : Trial
{
    /// <summary>
    /// Enemies killed so far in current trial
    /// </summary>
    public int enemiesKilled;

    /// <summary>
    /// Boolean flag checking if all enemies have been killed
    /// </summary>
    public bool allEnemiesKilled;
   
    public override void Start()
    {
        base.Start();

        // you MUST place logic after the base method, since important things such as the roomManager reference are established there

        // resets kill counter and all enemies killed flag on start (so as to not carry progress to other trials)
        enemiesKilled = 0;
        allEnemiesKilled = false;
    }

    // Method one of checking if all enemies defeated, essentially just checks if the number of enemies killed 
    public override void UpdateLogic()
    {
        base.UpdateLogic();

        // Increase enemies killed with K (For testing), only if enemies killed is fewer than / equal to max amount of enemies 
        if (Input.GetKeyDown(KeyCode.K) && enemiesKilled <= RoomManager.instance.maxAmount)
        {
            enemiesKilled++;
            Debug.Log("Enemies Killed: " + enemiesKilled.ToString());
        }

        // If number of enemies killed is greater than / equal to max number of enemies (i.e. all enemies have been defeated.
        if (enemiesKilled >= RoomManager.instance.maxAmount)
        {
            allEnemiesKilled = true;
            NotifyTrialComplete(allEnemiesKilled);
        }

        
        
    }

    /* Alternative method of checking if all enemies have been defeated, may be preferable for utility it adds with number of enemies being counted
        // This is essentially how I kept track of enemies remaining in my pirate game so it should work here as well, 
        // only potential problem is that it will count all enemies in the scene, so if multiple trials are in the same scene I don't think this will work for our needs
     
    public void LateUpdate()
    { 
        // Looks for enemies with "Enemy" tag and adds them to a list
        // then checks length of list to see how many enemies are alive, which could also be useful elsewhere (i.e. "Enemies remaining" counter)

        GameObject[] m_enemiesalive = GameObject.FindGameObjectsWithTag("Enemy");

        /// <summary>
        /// Integer that counts how many enemies remain in a trialby adding all the enemies to a list and checking its length
        /// </summary>
        int enemiesAlive = m_enemiesalive.Length;

        // if number of enemies alive is 0 (or fewer) and number of enemies killed is >= max amount

        if (enemiesAlive <= 0 && enemiesKilled >= GameObject.Find("Manager").GetComponent<RoomManager>().maxAmount)
        {
            // Trial Complete / Successful
            allEnemiesKilled = true;
            NotifyTrialComplete(allEnemiesKilled);
        }
    }*/

    //public override void UpdateLogic()
    //{
    //    base.UpdateLogic();

    //    // update logic can be placed either before or after the base method
    //    // it is good practice to place your logic after the base in order to keep things consistent with the Start base method

    //    /* Something along the lines of:
    //    if (RoomManager.enemiesKilled >= RoomManager.maxAmount && timer > 0)
    //    {
    //        RoomManager.instance.trial.NotifyTrialComplete(true);
    //    }
    //    */

    //    // once you set the win condition, use this function to notify the room manager that the trial is a success
    //    if (false)
    //        NotifyTrialComplete(true);
    //}
}
