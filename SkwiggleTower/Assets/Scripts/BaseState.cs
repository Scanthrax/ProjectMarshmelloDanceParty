using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseState : StateMachineBehaviour
{
    protected Animator anim;
    protected EnemyAI enemy;
    protected Rigidbody2D rb;
    protected EnemyStats stats;
    protected MeleeGuardAI enemy2;

    private bool initialized;
    protected bool canUpdate;


    public void Initialize(Animator anim)
    {
        if (!initialized)
        {
            initialized = true;
            this.anim = anim;

            // 
            enemy = anim.GetComponent<EnemyAI>();
            enemy2 = anim.GetComponent<MeleeGuardAI>();

            rb = anim.GetComponent<Rigidbody2D>();
            stats = anim.GetComponent<EnemyStats>();
        }

        canUpdate = true;
    }


    public void CheckAggro()
    {
        if(enemy)
            anim.SetBool("PlayersInRange", enemy.arePlayersInRange);
        else
            anim.SetBool("PlayersInRange", enemy2.arePlayersInRange);
    }

}
