using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardIdle : StateMachineBehaviour
{

    Animator anim;

    float timer;
    public float duration;


    //OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (anim == null) anim = animator;

        timer = 0;
    }

    //OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timer += Time.deltaTime;


        if (timer >= duration)
            anim.SetTrigger("GoToWaypoint");
    }

    //OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }

}
