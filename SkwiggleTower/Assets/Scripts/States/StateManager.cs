using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManager : MonoBehaviour
{
    public BaseState currentState;
    public EnemyInput input;
    public Vector2 visionBoxPos;
    public Vector2 visionBoxSize;


    public Collider2D[] visionColliders;
    int visionResult;

    public bool checkAggroEntry;

    IEnumerator CheckAggro;



    private void Start()
    {
        

        visionColliders = new Collider2D[4];

        foreach (var item in GetComponents<BaseState>())
        {
            item.input = input;
            item.stateManager = this;
        }

        GoToState(typeof(IdleState));

        CheckAggro = VisionCheck();

        checkAggroEntry = true;
        StartAggro();
    }

    public void Update()
    {

        currentState.StateUpdate();

    }


    public virtual void GoToState(System.Type state)
    {
        BaseState nextState = null;

        foreach (var item in GetComponents<BaseState>())
        {
            if (item.GetType() == state)
            {
                nextState = item;
                break;
            }
        }
        if (nextState)
        {
            if (currentState)
                currentState.StateExit();
            currentState = nextState;
            currentState.StateStart();
        }
        else
            Debug.LogWarning("The desired state does not exist!", this);
    }



    public IEnumerator VisionCheck()
    {
        //Debug.Log("Start detecting!");
        while (true)
        {
            var amt = Physics2D.OverlapBoxNonAlloc((Vector2)transform.position + visionBoxPos * input.faceDirection, visionBoxSize, 0, visionColliders, LayerMask.GetMask("Player"));

            if (checkAggroEntry)
            {
                if (amt > 0)
                {
                    GoToState(typeof(PursuitState));
                }
            }
            else
            {
                if (amt <= 0)
                {
                    checkAggroEntry = true;
                    GoToState(typeof(IdleState));
                }
            }
            yield return null;
        }

    }

    public void StartAggro()
    {
        StartCoroutine(CheckAggro);
    }




    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube((Vector2)transform.position + visionBoxPos * input.faceDirection, visionBoxSize);
        
    }







    //public IEnumerator Walking()
    //{
    //    result = 0;
    //    print("start walk raycast");
    //    while (result == 0)
    //    {

    //        result = Physics2D.RaycastNonAlloc(transform.position + transform.up * raycastInfo.y, transform.right * input.faceDirection, results, raycastInfo.x,mask);
    //        yield return null;
    //    }
    //    print("end walk raycast");
    //    GoToState(typeof(IdleState));
    //}




}
