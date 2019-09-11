using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolStateManager : StateManager
{
    public new void Start()
    {
        base.Start();
    }




    public new void Update()
    {
        currentState.StateUpdate();

        if (currentState.GetType() == typeof(IdleState))
        {

            if (timer >= duration)
                GoToState(typeof(WalkState));

            if (target)
                GoToState(typeof(PursuitState));

        }
        else if (currentState.GetType() == typeof(WalkState))
        {

            if (ledgeGap || wallInFront)
                GoToState(typeof(IdleState));

            if (target)
                GoToState(typeof(PursuitState));

        }
        else if (currentState.GetType() == typeof(PursuitState))
        {

            if (!target)
                GoToState(typeof(IdleState));

        }



    }


}
