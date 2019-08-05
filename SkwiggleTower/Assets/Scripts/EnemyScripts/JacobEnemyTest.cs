using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterStats))]
public class JacobEnemyTest : Interactable
{
    //PlayerManager playerManager; //References player
    //CharacterStats myStats; //References Enemy Stats
    ////This public int health is a test variable set up for testing player melee attack
    //public int health;

    //void Start()
    //{
    //    playerManager = PlayerManager.instance; //uses the singleton instance to reference the player manager
    //    myStats = GetComponent<CharacterStats>();
    //    xpGained = 100;
    //}

    //void Update()
    //{
    //    //If health is less than or equal to 0, enemy dies
    //    if(health <= 0)
    //    {
    //        Destroy(gameObject);
    //    }
    //}

    //public override void Interact()
    //{
    //    base.Interact();

    //    Combat playerCombat = playerManager.player.GetComponent<Combat>(); //Attack enemy
    //    if (playerCombat != null)
    //    {
    //        playerCombat.Attack(myStats); //Gets reference to damage modifier to attack 
    //    }
    //}
    ///*I'm commenting this method since there may be multiple methods for the enemy to take damage
    //public void takeDamage(int damage)
    //{
    //    health -= damage;
    //    Debug.Log("'Tis but a flesh wound");
    //}*/

}