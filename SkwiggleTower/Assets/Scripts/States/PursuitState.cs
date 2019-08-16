using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PursuitState : BaseState
{
    public float moveSpeed;
    public Transform target;

    protected float thisX, targetX;

    RaycastHit2D[] results;
    int result;

    public float meleeDistance;



    public void Start()
    {
        results = new RaycastHit2D[1];

        input.endMeleeEvent += FaceMelee;
    }


    public override void StateStart()
    {
        base.StateStart();
        //print("starting pursuit!");
        input.movement.movementSpeed = moveSpeed;
        input.horizontal = 1f * input.faceDirection;
        target = stateManager.visionColliders[0].transform;
        stateManager.checkAggroEntry = false;
        
    }


    public override void StateUpdate()
    {
        base.StateUpdate();

        thisX = transform.position.x;
        targetX = target.position.x;

        if (input.faceRight != thisX < targetX && Mathf.Abs(thisX - targetX) > 0.7f && !input.animator.GetBool("AbilityActive"))
        {
            input.ChangeDirection();
            input.horizontal = 1f * input.faceDirection;
        }



        result = Physics2D.RaycastNonAlloc(transform.position , transform.right * input.faceDirection, results, meleeDistance,LayerMask.GetMask("Player"));
        
        if(result > 0)
        {
            input.meleeAttack = true;
        }
        else
            input.meleeAttack = false;


        if(Physics2D.RaycastNonAlloc(transform.position, transform.right * input.faceDirection, results, meleeDistance, LayerMask.GetMask("Platform")) > 0)
        {
        }

    }


    public override void StateExit()
    {
        base.StateExit();
        input.meleeAttack = false;
        Debug.Log("Exiting pursuit!");
    }


    public void FaceMelee()
    {
        input.ChangeDirection(input.faceRight != thisX < targetX);
        input.horizontal = 1f * input.faceDirection;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, transform.position + transform.right * meleeDistance * (input ? input.faceDirection : 1));
    }
}
