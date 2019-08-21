using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : BaseState
{
    public Vector2 durationMinMax;

    public float duration;

    private IEnumerator coroutine;


    private void Awake()
    {
        //coroutine = Idle();
    }


    public override void StateStart()
    {
        base.StateStart();
        //print("starting idle!");

        input.horizontal = 0f;

        duration = Random.Range(durationMinMax.x, durationMinMax.y);

        StartCoroutine(Idle());
        
    }


    public IEnumerator Idle()
    {
        yield return new WaitForSeconds(duration);
        stateManager.GoToState(typeof(WalkState));
    }


    public override void StateExit()
    {
        StopCoroutine(Idle());

        //Debug.Log("Exiting Idle!");
    }
}
