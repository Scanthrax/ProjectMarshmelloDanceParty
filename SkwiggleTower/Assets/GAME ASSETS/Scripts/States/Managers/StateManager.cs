using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class StateManager : MonoBehaviour
{
    public BaseState currentState;
    public EnemyInput input;

    public BaseState startState;

    int visionResult;


    public List<Collider2D> listOfPlayersInTrigger;
    public List<Collider2D> listOfPlayersInSight;

    public BaseCharacter target;
    public Vector3 targetPosQueue;

    public SpriteRenderer marker;

    public float timer;
    public float duration;

    public bool ledgeGap, wallInFront;


    public void Start()
    {

        foreach (var item in GetComponents<BaseState>())
        {
            item.input = input;
            item.stateManager = this;
        }

        marker.gameObject.SetActive(false);
    }

    public void Update()
    {

        currentState.StateUpdate();

    }


    public virtual void GoToState(Type state)
    {
        BaseState nextState = GetState(state);

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

    public BaseState GetState(Type state)
    {
        BaseState getState = null;

        foreach (var item in GetComponents<BaseState>())
        {
            if (item.GetType() == state)
            {
                getState = item;
                break;
            }
        }

        return getState;
    }

    public void StateManagerInit()
    {
        currentState = startState;
        currentState.StateStart();
    }





    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.GetComponent<PlayableCharacter>()) return;

        if (!listOfPlayersInTrigger.Contains(collision))
            listOfPlayersInTrigger.Add(collision);

           
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!collision.GetComponent<PlayableCharacter>()) return;

        foreach (var player in listOfPlayersInTrigger)
        {
            if (!Physics2D.Linecast(transform.position, player.transform.position, LayerMask.GetMask("Platforms")))
            {
                if (!listOfPlayersInSight.Contains(collision))
                    listOfPlayersInSight.Add(collision);
            }
            else
            {
                if (listOfPlayersInSight.Contains(collision))
                    listOfPlayersInSight.Remove(collision);
            }
        }

        if (listOfPlayersInSight.Count == 0)
            target = null;
        else
            target = listOfPlayersInSight[0].GetComponent<BaseCharacter>();
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.GetComponent<PlayableCharacter>()) return;

        if (listOfPlayersInTrigger.Contains(collision))
            listOfPlayersInTrigger.Remove(collision);


        if (listOfPlayersInSight.Contains(collision))
            listOfPlayersInSight.Remove(collision);


        if (listOfPlayersInSight.Count == 0)
            target = null;



    }



    public IEnumerator ShowMarker(bool exc)
    {
        var RM = RoomManager.instance;
        marker.sprite = exc? RM.exclamation : RM.question;
        marker.gameObject.SetActive(true);
        yield return new WaitForSeconds(1f);
        marker.gameObject.SetActive(false);
    }

    private void OnDrawGizmos()
    {
        if(target)
        {
            Gizmos.DrawLine(transform.position, target.transform.position);
        }
    }

}
