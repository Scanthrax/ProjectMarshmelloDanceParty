using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockAttack : MonoBehaviour
{

    public Sounds category;

    public AudioSource rockImpact;

    string thisLayer, opposingLayer;

    Collider2D ignoreCollider;

    Rigidbody2D rb;

    private void Start()
    {
        Destroy(gameObject,3f);

        rockImpact.clip = AudioManager.instance.soundDictionary[category][0];


        opposingLayer = thisLayer == "Player" ? "Enemy" : "Player";

        Physics2D.IgnoreCollision(ignoreCollider, GetComponent<Collider2D>());

        rb = GetComponent<Rigidbody2D>();
    }




    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (LayerMask.LayerToName(collision.gameObject.layer) == opposingLayer)
        {
            var temp = collision.gameObject.GetComponent<CharacterStats>();
            if (temp)
            {
                temp.TakeDamage(1);
                print(thisLayer + " hit!");

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
                var x = AudioManager.instance.soundDictionary[category];


                if (x.Count > 1)
                    rockImpact.clip = x[Random.Range(0, x.Count)];

                rockImpact.Play();

            }


            gameObject.layer = LayerMask.NameToLayer("Debris");
        }
    }

    public void SetLayer(string layer, Collider2D col)
    {
        thisLayer = layer;
        ignoreCollider = col;
    }

}
