using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Valarie Script  this is the Script that specfies how the player interacts with the enemy
/// </summary>
[RequireComponent(typeof(CharacterStats))]
public class Enemy : Interactable
{
    PlayerManager playerManager; //References player
    CharacterStats myStats; //References Enemy Stats

    void Start()
    {
        playerManager = PlayerManager.instance; //uses the singleton instance to reference the player manager
        myStats = GetComponent<CharacterStats>();
        xpGained = 100;
    }

    public override void Interact()
    {
        base.Interact();

        Combat playerCombat = playerManager.player.GetComponent<Combat>(); //Attack enemy
        if (playerCombat != null)
        {
            playerCombat.Attack(myStats); //Gets reference to damage modifier to attack 
        }
    }

}
