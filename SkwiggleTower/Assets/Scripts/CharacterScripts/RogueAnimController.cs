//Author:   Ron Weeden
//Modified: 6/20/2019

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RogueAnimController : MonoBehaviour
{
    Rigidbody2D rb;
    [HideInInspector]
    public Animator anim;
    PlayerMovement pm;
    RoomManager RM;
    PlayerManager PManager;
    [HideInInspector]
    public CharacterStats CS;

    public AudioSource footstepSource, impactSource, slingshotStretch, slingshotShoot, secondarySource;

    public Rigidbody2D rock;

    public float impulse;

    public PlayerInput PI;








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
        //PInput.actions = RM.inputMaster;

        CS = GetComponent<CharacterStats>();

    }

    // Update is called once per frame
    void Update()
    {

        anim.SetBool("isMoving", PI.horizontal != 0f);

        anim.SetBool("inAir", !pm.isOnGround);

        //if (PI.secondaryAttackPressed)
        //{
        //    anim.SetTrigger("Secondary");
        //}


        //if (PI.primaryAttackPressed)
        //{
        //    anim.SetBool("primaryHold", true);

        //    if (!CS.primary.onCooldown)
        //    {
        //        anim.SetTrigger("Primary");
        //        CS.primary.timer = 0f;
        //    }
        //}
        //else
        //{
        //    anim.SetBool("primaryHold", false);
        //}

        if(PI.ultPressed)
        {
            CS.ultimate.Cast();
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




    public void SlingStretch()
    {
        slingshotStretch.Play();
    }


    public void SecondarySound()
    {
        secondarySource.Play();
    }






    public void CastPrimary()
    {
        if (CS.primary)
            CS.primary.Cast();
    }

    public void CastSecondary()
    {
        if (CS.secondary)
            CS.secondary.Cast();
    }

    public void FinishPrimary()
    {
        anim.SetBool("PrimaryActive", false);
    }
}
