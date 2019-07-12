using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardWalking : StateMachineBehaviour
{

    float timer;
    public float duration;


    //OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.GetComponent<EnemyAI>().NextIndex();
        timer = 0f;


        animator.GetComponent<EnemyAI>().canPathfind = true;
    }

    //OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timer += Time.deltaTime;


        if (timer >= duration)
        {
            animator.SetTrigger("ReachedWaypoint");
            timer = 0f;
        }
    }

    //OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }


}
