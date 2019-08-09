using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkState : BaseState
{

    private IEnumerator coroutine;

    public Vector2 raycastInfo;

    RaycastHit2D[] results;
    int result;

    public float moveSpeed;


    public override void StateStart()
    {
        base.StateStart();

        input.ChangeDirection();

        input.horizontal = 1f * input.faceDirection;
        print("starting walk!");

        input.movement.movementSpeed = moveSpeed;

        results = new RaycastHit2D[1];

        coroutine = Walking();

        StartCoroutine(coroutine);

        
    }

    public IEnumerator Walking()
    {
        result = 0;

        while (result == 0)
        {
            result = Physics2D.RaycastNonAlloc(transform.position + transform.up * raycastInfo.y, transform.right * input.faceDirection, results, raycastInfo.x);
            yield return null;
        }

        input.GoToState(typeof(IdleState));
    }


    public override void StateExit()
    {
        base.StateExit();
        StopCoroutine(coroutine);
        Debug.Log("Exiting Walk!");
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position + transform.up * raycastInfo.y, transform.position + transform.right * raycastInfo.x * (input ? input.faceDirection : 1) + transform.up * raycastInfo.y);
    }
}
