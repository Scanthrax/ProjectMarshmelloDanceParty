using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RogueAnimController : MonoBehaviour
{
    Rigidbody2D rb;
    Animator anim;
    PlayerMovement pm;

    AudioSource footstepSource, impactSource;

    public Rigidbody2D rock;

    public float impulse;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        pm = GetComponent<PlayerMovement>();
        anim = GetComponent<Animator>();
        footstepSource = AudioManager.instance.AddSource(gameObject, Sounds.AsphaltFootsteps, SoundChannels.Footsteps);
        impactSource = AudioManager.instance.AddSource(gameObject, Sounds.GroundImpact, 1, SoundChannels.GroundImpact);
    }

    // Update is called once per frame
    void Update()
    {

        anim.SetBool("isMoving", Mathf.Abs(rb.velocity.x) > 0.1f);

        anim.SetBool("inAir", !pm.isOnGround);

        if (Input.GetMouseButtonDown(1))
        {
            if (pm.isOnGround && !anim.GetBool("usingAbility"))
            {
                anim.SetTrigger("Secondary");
            }
        }

        if (Input.GetMouseButtonDown(0))
        {
            anim.SetBool("primaryHold", true);

            //if (pm.isOnGround && !anim.GetBool("usingAbility"))
            //{
                anim.SetTrigger("Primary");
            //}
        }

        if(Input.GetMouseButtonUp(0))
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


    public void SetUsingAbility(int i)
    {
        anim.SetBool("usingAbility", i == 0);
    }

    public void GetFootstep()
    {
        AudioManager.instance.PlaySoundpool(footstepSource, Sounds.AsphaltFootsteps);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        var impact = collision.relativeVelocity.magnitude;

        print(impact);

        if (impact > 10f)
        {
            impactSource.Play();
        }
    }


    public void ThrowRock()
    {
        var rockRb = Instantiate(rock,this.transform.position + new Vector3(pm.GetDirection() * 0.6f,0.1f),Quaternion.identity);
        rockRb.AddForce(Vector2.right * pm.GetDirection() * impulse);
    }
}
