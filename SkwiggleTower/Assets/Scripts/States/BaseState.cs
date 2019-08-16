using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseState : MonoBehaviour
{
    public EnemyInput input;
    public StateManager stateManager;

    //public void Start()
    //{

    //}


    public virtual void StateStart()
    {

    }

    public virtual void StateUpdate()
    {

    }

    public virtual void StateExit()
    {

    }


}
