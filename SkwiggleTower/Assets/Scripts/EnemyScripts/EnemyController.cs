using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float lookRadius = 10f;

    Transform target;
    Combat enemyCombat;
    float stoppingDistance;

    // Start is called before the first frame update
    void Start()
    {
        stoppingDistance = GetComponent<Interactable>().radius;
        target = PlayerManager.instance.player.transform; //Creates reference to the players location
        enemyCombat = GetComponent<Combat>();
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector2.Distance(target.position, transform.position);

        if (distance <= lookRadius)
        {

            if (distance <= stoppingDistance)
            {
                //Attack
                CharacterStats targetStats = target.GetComponent<CharacterStats>();
                if (targetStats != null)
                {
                    enemyCombat.Attack(targetStats);
                }
                FaceTarget(); //Face Target
            }
        }
    }

    void FaceTarget()
    {
        Vector2 direction = (target.position - transform.position).normalized; //gets postion of target and subtracts our position from it 
    }

    private void OnDrawGizmosSelected()
    {
        //Shows in Unity inspect the range of vision of the Enemy.
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }
}
