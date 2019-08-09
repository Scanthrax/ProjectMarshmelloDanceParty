using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyInput : BaseInput
{
    public BaseState currentState;
    public Transform statesContainer;



    public Vector2 visionBoxPos, visionBoxSize;


    public Collider2D[] visionColliders;
    int visionResult;

    public bool checkAggroEntry;

    IEnumerator CheckAggro;


    public bool meleeAttack;

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

        CheckAggro = VisionCheck();

        checkAggroEntry = true;
        StartAggro();
    }

    public void Update()
    {
        // do not progress if the controller is disabled
        if (!controllerEnabled) return;

        currentState.StateUpdate();


        SetAnimatorValues(character.melee, "Melee", meleeAttack, false, false);


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
        Debug.Log("Start detecting!");
        while (true)
        {
            var amt = Physics2D.OverlapBoxNonAlloc((Vector2)transform.position + visionBoxPos * faceDirection, visionBoxSize, 0, visionColliders,LayerMask.GetMask("Player"));

            if(checkAggroEntry)
            {
                if (amt > 0)
                {
                    Debug.Log("I detect someone");
                    GoToState(typeof(PursuitState));
                }
            }
            else
            {
                if (amt <= 0)
                {
                    Debug.Log("I can no longer see anyone!");
                    checkAggroEntry = true;
                    GoToState(typeof(IdleState));
                }
            }
            yield return null;
        }

    }

    public void StartAggro()
    {
        Debug.Log("Start Aggro detection!");
        StartCoroutine(CheckAggro);
    }
    //public void EndAggro()
    //{
    //    Debug.Log("Stopping Aggro detection!");
    //    StartCoroutine(CheckAggro);
    //}


    public void SetHorizontalAxis()
    {
        horizontal = 1f * faceDirection;
    }


    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube((Vector2)transform.position + visionBoxPos * faceDirection, visionBoxSize);
    }

}
