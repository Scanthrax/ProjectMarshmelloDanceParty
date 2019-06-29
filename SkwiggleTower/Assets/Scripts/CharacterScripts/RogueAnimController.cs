//Author:   Ron Weeden
//Modified: 6/20/2019

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RogueAnimController : MonoBehaviour
{
    Rigidbody2D rb;
    Animator anim;
    PlayerMovement pm;
    RoomManager RM;
    PlayerManager PManager;

    public AudioSource footstepSource, impactSource, slingshotStretch, slingshotShoot, secondarySource;

    public Rigidbody2D rock;

    public float impulse;

    public PlayerInput PI;


    public int playerIndex;





    private void Awake()
    {


    }



    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        pm = GetComponent<PlayerMovement>();
        anim = GetComponent<Animator>();
        PI = GetComponent<PlayerInput>();
        //PInput = GetComponent<UnityEngine.InputSystem.Plugins.PlayerInput.PlayerInput>();
        
        PManager = PlayerManager.instance;

        RM = RoomManager.instance;
        if (playerIndex == -1) Destroy(gameObject);
        //PInput.actions = RM.inputMaster;

    }

    // Update is called once per frame
    void Update()
    {

        anim.SetBool("isMoving", PI.horizontal != 0f);

        anim.SetBool("inAir", !pm.isOnGround);

        if (PI.secondaryAttackPressed)
        {
            anim.SetTrigger("Secondary");
        }


        if (PI.primaryAttackPressed)
        {
            anim.SetBool("primaryHold", true);
            anim.SetTrigger("Primary");
        }
        else
        {
            anim.SetBool("primaryHold", false);
        }

    }

    private void FixedUpdate()
    {
        //if (anim.GetBool("usingAbility"))
        //{
        //    rb.velocity = Vector2.zero;
        //}
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Finish"))
        {
            RoomManager.instance.trial.NotifyTrialComplete(true);
        }
    }




    public void GetFootstep()
    {
        AudioManager.instance.PlaySoundpool(footstepSource, Sounds.AsphaltFootsteps);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        var impact = collision.relativeVelocity.magnitude;

        //print(impact);

        if (impact > 10f)
        {
            impactSource.Play();
        }
    }


    public void ThrowRock()
    {
        var rockRb = Instantiate(rock,this.transform.position + new Vector3(pm.GetDirection() * 0.6f,0.1f),Quaternion.identity);
        rockRb.AddForce(Vector2.right * pm.GetDirection() * impulse);
        slingshotShoot.Play();
    }

    public void SlingStretch()
    {
        slingshotStretch.Play();
    }


    public void SecondarySound()
    {
        secondarySource.Play();
    }
}
