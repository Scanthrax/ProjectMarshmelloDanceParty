using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockAttack : MonoBehaviour
{

    public AudioSource rockImpact;

    private void Start()
    {
        Destroy(gameObject,3f);

        Physics2D.IgnoreCollision(PlayerManager.instance.player.GetComponent<Collider2D>(), GetComponent<Collider2D>());
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (LayerMask.LayerToName(collision.gameObject.layer) == "Enemy")
        {
            var temp = collision.gameObject.GetComponent<CharacterStats>();
            if (temp)
            {
                temp.TakeDamage(1);
                print("enemy hit!");

                var testImpact = collision.gameObject.GetComponent<TestImpactSound>();
                if (testImpact)
                    testImpact.PlayImpact();

                Destroy(gameObject);
            }
            else
                print("can't be damaged!");
        }
        else
        {
            //print("rock impact: " + collision.relativeVelocity.magnitude);


            if (collision.relativeVelocity.magnitude > 10f)
            {
                
                rockImpact.Play();

            }
                
        }
    }
}
