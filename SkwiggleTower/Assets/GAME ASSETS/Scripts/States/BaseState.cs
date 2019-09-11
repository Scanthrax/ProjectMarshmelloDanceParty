using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseState : MonoBehaviour
{
    public EnemyInput input;
    public StateManager stateManager;


    public delegate void StartStateHandler();
    public event StartStateHandler StartStateEvent;

    public delegate void ExitStateHandler();
    public event ExitStateHandler ExitStateEvent;

    public virtual void StateStart()
    {
        StartEvent();
    }

    public virtual void StateUpdate()
    {

    }

    public virtual void StateExit()
    {
        ExitStateEvent?.Invoke();
    }

    public void StartEvent()
    {
        StartStateEvent?.Invoke();
    }

}
