using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Valarie Interactable, OnFocused and OnDefocused.
/// </summary>
public class Interactable : MonoBehaviour
{
    public float radius = 3f;
    public Transform interactionTransform;
    protected int healthGained = 10;
    protected int xpGained = 10;

   // bool isFocus = false;
    Transform player;

    bool hasInteracted = false;

    public virtual void Interact()
    {
        //method meant to be overriddden
    }

    private void Update()
    {
        if (!hasInteracted)
        {
            float distance = Vector3.Distance(player.position, interactionTransform.position);
            if (distance <= radius)
            {
                //Debug.Log("INTERACT");
                Interact();
                hasInteracted = true;
            }
        }

    }

    /// <summary>
    /// play will go to item or enemy it clicks on
    /// </summary>
    /// <param name="playerTransform"></param>
    public void OnFocused(Transform playerTransform)
    {
        //isFocus = true;
        player = playerTransform;
        hasInteracted = false;
    }

    //Stops focusing on object
    public void OnDefocused()
    {
        //isFocus = false;
        player = null;
        hasInteracted = false;
    }
}
