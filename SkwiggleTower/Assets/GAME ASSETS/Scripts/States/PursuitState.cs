using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PursuitState : BaseState
{
    public float moveSpeed;
    

    float thisX, targetX;

    RaycastHit2D[] results;
    int result;

    public float meleeDistance;

    public bool targetToRight;

    public void Start()
    {
        results = new RaycastHit2D[1];

    }


    public override void StateStart()
    {
        base.StateStart();
        //print("starting pursuit!");
        input.movement.movementSpeed = moveSpeed;
        input.horizontal = input.faceDirection;
        


        StartCoroutine(stateManager.ShowMarker(true));
        
    }


    public override void StateUpdate()
    {
        base.StateUpdate();

        if (stateManager.target)
        {
            if (input.movement.IsToRight(transform, stateManager.target.transform) == input.faceRight)
            {
                print("CHANGE DIRECTION");
                input.ChangeDirection(input.faceDirection != 1);
            }
        }




        result = Physics2D.RaycastNonAlloc(transform.position, transform.right * input.faceDirection, results, meleeDistance, LayerMask.GetMask("Player"));

        if (result > 0)
        {
            input.basicAttack = true;
        }
        else
            input.basicAttack = false;







    }


    public override void StateExit()
    {
        base.StateExit();
        input.basicAttack = false;
        StartCoroutine(stateManager.ShowMarker(false));
    }




    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, transform.position + transform.right * meleeDistance * (input ? input.faceDirection : 1));
    }
}
