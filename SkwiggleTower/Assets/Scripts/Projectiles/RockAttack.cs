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

    public AudioClip whooshSound, impactSound, armorHitSound; 


    public int damage;

    private void Start()
    {
        Destroy(gameObject,3f);

        impactSound = AudioManager.instance.soundDictionary[category][0];

        opposingLayer = thisLayer == "Player" ? "Enemy" : "Player";


        rockImpact.clip = whooshSound;
        rockImpact.Play();

        //Physics2D.IgnoreCollision(ignoreCollider, GetComponent<Collider2D>());

        rb = GetComponent<Rigidbody2D>();
    }




    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (LayerMask.LayerToName(collision.gameObject.layer) == opposingLayer)
        {
            var temp = collision.gameObject.GetComponent<CharacterStats>();
            if (temp)
            {
                temp.TakeDamage(damage);
                print(opposingLayer + " hit!");


                rockImpact.clip = impactSound;
                rockImpact.Play();
                temp.PlayImpactSound("rock");
                Destroy(gameObject);


            }
            else
                print("can't be damaged!");
        }
        else
        {
                if (collision.relativeVelocity.magnitude > 10f)
                {
                    var x = AudioManager.instance.soundDictionary[category];


                    if (x.Count > 1)
                        rockImpact.clip = x[Random.Range(0, x.Count)];

                rockImpact.clip = impactSound;
                rockImpact.Play();

                }


                gameObject.layer = LayerMask.NameToLayer("Debris");
                if(rb)rb.gravityScale = 1f;
            }
    }

    public void SetLayer(string layer, Collider2D col)
    {
        thisLayer = layer;
        ignoreCollider = col;
    }

}
