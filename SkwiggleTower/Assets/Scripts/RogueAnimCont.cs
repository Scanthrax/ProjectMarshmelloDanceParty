using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RogueAnimCont : MonoBehaviour
{
    public Rigidbody2D rb;
    public Animator anim;
    public PlayerMovement pm;

    public GameObject rock;

    private void Update()
    {
        if(rb.velocity.x != 0)
        {
            anim.SetBool("isWalking", true);
        }
        else
        {
            anim.SetBool("isWalking", false);
        }


        if(pm.isOnGround)
        {
            anim.SetBool("inAir", false);
        }
        else
        {
            anim.SetBool("inAir", true);
        }


        if (pm.isOnGround)
        {
            if (Input.GetMouseButtonDown(0))
            {
                anim.SetTrigger("meleeAttack");
            }

            if (Input.GetMouseButtonDown(1))
            {
                anim.SetTrigger("rangedAttack");
            }
        }


    }



    public void SpawnRock()
    {
        Instantiate(rock, transform.position + Vector3.right * pm.GetDirection() * 0.6f, Quaternion.identity);
    }
}
