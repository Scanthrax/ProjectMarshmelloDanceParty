using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AggroTrigger : MonoBehaviour
{
    EnemyAI enemy;

    private void Start()
    {
        enemy = GetComponentInParent<EnemyAI>();
    }


    private void OnTriggerStay2D(Collider2D collision)
    {



        var player = collision.GetComponent<PlayerStats>();
        if (player)
        {
            if (!enemy.playersInRange.Contains(player))
            {
                RaycastHit2D hit = Physics2D.Raycast(transform.position, -transform.position + player.transform.position);

                // If it hits something...
                if (hit.collider)
                {
                    var x = hit.collider.GetComponent<PlayerStats>();

                    if (x == player)
                    {
                        print("I see a player!");
                        enemy.playersInRange.Add(player);
                    }
                }

            }
            else
            {
                RaycastHit2D hit = Physics2D.Raycast(transform.position, -transform.position + player.transform.position);

                // If it hits something...
                if (hit.collider)
                {
                    var x = hit.collider.GetComponent<PlayerStats>();

                    if (x != player)
                    {
                        print("the player is out of my sight!");
                        enemy.playersInRange.Remove(player);
                    }
                }
            }
            
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        var player = collision.GetComponent<PlayerStats>();
        if(enemy.playersInRange.Contains(player))
            enemy.playersInRange.Remove(player);
    }
}

