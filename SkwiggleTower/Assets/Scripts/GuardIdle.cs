using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardIdle : BaseState
{


    float timer;
    public float duration;


    //OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Initialize(animator);

        rb.velocity = Vector2.zero;


        timer = 0f;

        enemy.idle = true;
        anim.SetBool("isWalking", false);

    }

    //OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (!canUpdate) return;

        timer += Time.deltaTime;


        if (timer >= duration)
        {
            anim.SetTrigger("GoToWaypoint");
            canUpdate = false;
        }

        CheckAggro();

    }

    //OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        enemy.idle = false;
    }

}
