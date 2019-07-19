using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardWalking : BaseState
{

    public float impulse;
    public float distToReachWaypoint;

    Vector3 waypointPos;

    //OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Initialize(animator);


        enemy.NextIndex();
        enemy.SetWaypoint();


        enemy.canPathfind = true;
        canUpdate = true;

        waypointPos = enemy.waypointPosition;

        anim.SetBool("isWalking", true);

    }

    //OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (!canUpdate) return;

        if (enemy)
        {
            if (Vector2.Distance(enemy.transform.position, enemy.destination) < 1.25f)
            {
                anim.SetTrigger("ReachedWaypoint");
                canUpdate = false;
            }
        }
        else
        {
            if (Vector2.Distance(enemy2.transform.position, enemy2.destination) < 1.25f)
            {
                anim.SetTrigger("ReachedWaypoint");
                canUpdate = false;
            }
        }



        CheckAggro();
    }

    //OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
    }


}
