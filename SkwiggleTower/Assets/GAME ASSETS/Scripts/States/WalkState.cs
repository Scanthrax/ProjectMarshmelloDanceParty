using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkState : BaseState
{


    public Vector2 raycastLengthMinMax;

    public Vector2 raycastInfo;
    RaycastHit2D[] results;
    int result;
    public LayerMask mask;

    public float moveSpeed;

    public Vector2 checkForGapPoint;




    public override void StateStart()
    {
        base.StateStart();

        input.ChangeDirection();

        input.horizontal = 1f * input.faceDirection;

        input.movement.movementSpeed = moveSpeed;

        results = new RaycastHit2D[1];
        result = 0;

        raycastInfo.x = Random.Range(raycastLengthMinMax.x, raycastLengthMinMax.y);


        stateManager.wallInFront = false;
        stateManager.ledgeGap = false;
    }


    public override void StateUpdate()
    {

        #region Detect wall
        result = Physics2D.RaycastNonAlloc(transform.position + transform.up * raycastInfo.y, transform.right * input.faceDirection, results, raycastInfo.x, mask);
        if (result != 0)
        {
            stateManager.wallInFront = true;
        }
        #endregion

        #region Detect gap
        if (!Physics2D.OverlapCircle((Vector2)transform.position + checkForGapPoint * new Vector2(input ? input.faceDirection : 1, 1),0.1f, mask))
        {
            stateManager.ledgeGap = true;
        }
        #endregion

    }




    public override void StateExit()
    {
        base.StateExit();
        //Debug.Log("Exiting Walk!");
    }


    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position + transform.up * raycastInfo.y, transform.position + transform.right * raycastInfo.x * (input ? input.faceDirection : 1) + transform.up * raycastInfo.y);
        Gizmos.DrawWireSphere((Vector2)transform.position + checkForGapPoint * new Vector2((input ? input.faceDirection : 1), 1), 0.1f);
    }

}
