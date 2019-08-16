using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TrialKillVIP : Trial
{
    // Matt Thompson
    // Last Modified: 6/10/19

    /// <summary>
    /// VIPs killed so far in current trial
    /// </summary>
    public int VIPsKilled;

    /// <summary>
    /// VIPs currently alive in trial
    /// </summary>
    public int VIPsRemaining;

    /// <summary>
    /// max number of VIPs in the trial, should likely be set in inspector, may need to edit spawners to spawn VIPs
    /// </summary>
    public int VIPsMax;

    /// <summary>
    /// Boolean flag checking if all enemies have been killed
    /// </summary>
    public bool allVIPsKilled;

    public BaseCharacter baseCharacter;

    public Color vipColor;

    public override void Start()
    {
        base.Start();

        // you MUST place logic after the base method, since important things such as the roomManager reference are established there

        // resets kill counter and all enemies killed flag on start (so as to not carry progress to other trials)
        VIPsKilled = 0;
        allVIPsKilled = false;

        VIPsMax = 3;

        //var enemy = Instantiate(baseCharacter,Vector3.zero,Quaternion.identity);
        //enemy.DeathEvent += testEvent;

        var rand = Random.Range(0, RoomManager.instance.listOfSpawners.Length - 1);
        var spawnerVIPs = RoomManager.instance.listOfSpawners[rand];

        spawnerVIPs.onSpawnEnemyDeath += VIPtick;

        spawnerVIPs.color = Color.red;

    }

    public void testEvent(BaseCharacter character)
    {
        Debug.Log("VIP should be destroyed!");
        VIPsRemaining--;
        Destroy(character.gameObject);
    }

    // Method one of checking if all enemies defeated, essentially just checks if the number of enemies killed 
    public override void UpdateLogic()
    {
        base.UpdateLogic();

        // Increase enemies killed with K (For testing), only if enemies killed is fewer than / equal to max amount of enemies 
        if (Input.GetKeyDown(KeyCode.K) && VIPsKilled <= VIPsMax)
        {
            VIPsKilled++;
            Debug.Log("Enemies Killed: " + VIPsKilled.ToString());
        }
       
        
    }

    public void VIPtick (BaseCharacter character)
    {
        VIPsKilled++;

        VIPsRemaining = VIPsMax - VIPsKilled;

        // If number of enemies killed is greater than / equal to max number of enemies (i.e. all enemies have been defeated.
        if (VIPsRemaining <= 0)
        {
            allVIPsKilled = true;
            NotifyTrialComplete(allVIPsKilled);
        }

    }

}
