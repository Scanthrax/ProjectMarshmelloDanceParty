using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCharacter : BaseCharacter
{





    public override void OnObjectSpawn()
    {
        base.OnObjectSpawn();
        print("ENEMY ONSPAWN");

        RoomManager.instance.trial.trialEndEvent += OnDeath;

        SpawnEvent += stateManager.StateManagerInit;


        DeathEvent += IncreaseKillCounter;

    }


    public void IncreaseKillCounter(BaseCharacter character)
    {
        RoomManager.instance.amtOfEnemiesKilled++;
    }
}
