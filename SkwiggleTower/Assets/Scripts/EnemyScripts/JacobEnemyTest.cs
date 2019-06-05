using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterStats))]
public class JacobEnemyTest : Interactable
{
    PlayerManager playerManager; //References player
    CharacterStats myStats; //References Enemy Stats
    public int health;

    void Start()
    {
        playerManager = PlayerManager.instance; //uses the singleton instance to reference the player manager
        myStats = GetComponent<CharacterStats>();
        xpGained = 100;
    }

    void Update()
    {
        if(health <= 0)
        {
            Destroy(gameObject);
        }
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

    public void takeDamage(int damage)
    {
        health -= damage;
        Debug.Log("'Tis but a flesh wound");
    }

}