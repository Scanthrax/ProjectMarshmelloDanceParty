using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatStateManager : StateManager
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
