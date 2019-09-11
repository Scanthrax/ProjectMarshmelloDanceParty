using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkullStateManager : StateManager
{

    public new void Start()
    {
        base.Start();
        GetState(typeof(IdleState)).StartStateEvent += delegate { (input.movement as SkullMovement).SetJump(false); };
        GetState(typeof(PursuitState)).StartStateEvent += delegate { (input.movement as SkullMovement).SetJump(true); };
    }



    public new void Update()
    {
        currentState.StateUpdate();

        if(currentState.GetType() == typeof(IdleState))
        {
            if (target)
                GoToState(typeof(PursuitState));
        }
        else if (currentState.GetType() == typeof(PursuitState))
        {
            if (!target)
                GoToState(typeof(IdleState));
        }



        if (target)
        {
            if (Vector2.Distance(transform.position, target.transform.position) < 0.6f)
            {
                JoinSkeleton();
            }
        }

    }



    public void JoinSkeleton()
    {
        var body = target.GetComponent<EnemyCharacter>();

        RoomManager.instance.listOfSkeletonBodies.Remove(body);
        RoomManager.instance.listOfSkeletonHeads.Remove(input.character as EnemyCharacter);

        ObjectPoolManager.instance.BackToPool(input.character.root, false);
        ObjectPoolManager.instance.BackToPool(body.root, false);
        ObjectPoolManager.instance.SpawnFromPool(RoomManager.instance.skeletonPrefab, (Vector2)transform.position + Vector2.up * 0.4f, Quaternion.identity);

        RoomManager.instance.CalculateClosestBody();
    }
}
