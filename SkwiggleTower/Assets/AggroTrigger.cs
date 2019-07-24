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
        // get PlayerStats component to check if it is a pleyer
        var player = collision.GetComponent<PlayerStats>();

        // if the component is valid...
        if (player)
        {
            // the agent hasn't seen the player yet
            if (!enemy.playersInRange.Contains(player))
            {
                RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, -transform.position + player.transform.position, Vector2.Distance(transform.position, player.transform.position));

                foreach (var hit in hits)
                {
                    // we can see through enemies
                    if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Enemy")) continue;


                    var x = hit.collider.GetComponent<PlayerStats>();
                    if (x == player)
                    {
                        print("I see a player!");
                        enemy.playersInRange.Add(player);
                    }
                    else
                        break;


                }

            }
            else
            {
                RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, -transform.position + player.transform.position, Vector2.Distance(transform.position, player.transform.position));

                bool obstruction = false;

                foreach (var hit in hits)
                {
                    // we can see through enemies
                    if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Enemy")) continue;


                    var x = hit.collider.GetComponent<PlayerStats>();
                    if (!x)
                    {
                        obstruction = true;
                    }

                }

                if (obstruction)
                {
                    print("the player is out of my sight!");
                    enemy.playersInRange.Remove(player);
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

