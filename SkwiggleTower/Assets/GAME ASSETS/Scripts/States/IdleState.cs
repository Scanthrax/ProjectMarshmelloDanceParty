using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : BaseState
{
    public Vector2 durationMinMax;

    public float duration;



    public override void StateStart()
    {
        base.StateStart();
        //Debug.Log("starting idle!", gameObject);

        input.horizontal = 0f;

        stateManager.duration = Random.Range(durationMinMax.x, durationMinMax.y);

        stateManager.timer = 0f;

        StartEvent();
    }


    public override void StateUpdate()
    {
        if (durationMinMax == Vector2.zero) return;

        stateManager.timer += Time.deltaTime;

    }





    public override void StateExit()
    {
        stateManager.timer = 0f;
        //Debug.Log("Exiting Idle!");
    }
}
