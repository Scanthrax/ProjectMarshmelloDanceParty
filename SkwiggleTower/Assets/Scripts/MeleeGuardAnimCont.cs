using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeGuardAnimCont : MonoBehaviour
{
    Rigidbody2D rb;
    Animator anim;

    public Transform rogue;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

    }

    private void Update()
    {
        anim.SetBool("isWalking",Mathf.Abs(rb.velocity.x) > 0.12f);


        if (Mathf.Abs(rogue.position.x - transform.position.x) < 1f)
            anim.SetTrigger("attack");
    }
}
