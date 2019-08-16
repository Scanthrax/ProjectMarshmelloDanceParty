using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCharacter : BaseCharacter
{


    private new void Start()
    {
        base.Start();
        DeathEvent += IncreaseKillCounter;

        RoomManager.instance.trial.trialEndEvent += OnDeath;

    }


    public void IncreaseKillCounter(BaseCharacter character)
    {
        RoomManager.instance.amtOfEnemiesKilled++;
    }
}
