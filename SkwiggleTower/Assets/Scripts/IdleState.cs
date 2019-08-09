using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : BaseState
{
    public float duration;

    private IEnumerator coroutine;

    public override void StateStart()
    {
        base.StateStart();
        //print("starting idle!");

        input.horizontal = 0f;

        coroutine = Idle();

        StartCoroutine(coroutine);
        
    }


    public IEnumerator Idle()
    {
        yield return new WaitForSeconds(duration);
        input.GoToState(typeof(WalkState));
    }


    public override void StateExit()
    {
        StopCoroutine(coroutine);

        //Debug.Log("Exiting Idle!");
    }
}
