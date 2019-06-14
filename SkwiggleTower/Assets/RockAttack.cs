using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockAttack : MonoBehaviour
{
    private void Start()
    {
        Destroy(gameObject,3f);
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

                Destroy(gameObject);
            }
            else
                print("can't be damaged!");
        }
        else
            print("not correct layer!");
    }
}
