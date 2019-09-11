using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCharacter : BaseCharacter, IPooledObject
{

    public StateManager stateManager;

    public new void Start()
    {
        InitEvent += stateManager.StateManagerInit;

        // have the enemies vaporize upon death?
        //RoomManager.instance.trial.trialEndEvent += OnDeath;

        base.Start();
    }


    


    public override void OnObjectSpawn()
    {
        base.OnObjectSpawn();
        print("ENEMY ONSPAWN");

        

        

    }


    public void IncreaseKillCounter(BaseCharacter character)
    {
        RoomManager.instance.amtOfEnemiesKilled++;
    }


}
