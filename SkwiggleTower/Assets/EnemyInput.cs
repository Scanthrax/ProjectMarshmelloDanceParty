using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyInput : BaseInput
{
    public BaseState currentState;
    public Transform statesContainer;



    public Vector2 visionBoxPos, visionBoxSize;


    Collider2D[] visionColliders;
    int visionResult;

    private void Start()
    {
        animator.SetLayerWeight(animator.GetLayerIndex("Rogue Animation"), 0f);
        animator.SetLayerWeight(animator.GetLayerIndex("Warrior Animation"), 1f);

        visionColliders = new Collider2D[4];

        foreach (var item in statesContainer.GetComponents<BaseState>())
        {
            item.input = this;
        }

        GoToState(typeof(IdleState));
    }

    public new void Update()
    {
        base.Update();

        RelayInfo();
    }


    public virtual void GoToState(System.Type state)
    {
        BaseState nextState = null;

        foreach (var item in statesContainer.GetComponents<BaseState>())
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
        while (true)
        {
            Physics2D.OverlapBoxNonAlloc((Vector2)transform.position + visionBoxPos, visionBoxSize, 0, visionColliders);

            yield return null;
        }
    }


    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube((Vector2)transform.position + visionBoxPos, visionBoxSize);
    }

}
