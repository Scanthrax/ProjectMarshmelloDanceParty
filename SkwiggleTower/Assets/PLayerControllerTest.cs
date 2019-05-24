using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PLayerControllerTest : MonoBehaviour
{
    Rigidbody2D rb;

    AudioSource source;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        source = AudioManager.instance.AddSource(gameObject, Sounds.AsphaltFootsteps,SoundChannels.SFX);
    }

    void Update()
    {
        // move left
        if (Input.GetKey(KeyCode.A))
            rb.AddForce(Vector2.left * 5f + Vector2.up);

        // move right
        if (Input.GetKey(KeyCode.D))
            rb.AddForce(Vector2.right * 5f + Vector2.up);

        // jump
        if (Input.GetKeyDown(KeyCode.W))
            rb.AddForce(Vector2.up * 250f);

        // play footstep sound when we're moving left/right and not in the process of jumping
        if (rb.velocity.x != 0 && rb.velocity.y < 1f && rb.velocity.y > -1f)
            // activate sound every 30th frame
            if(Time.frameCount%30 == 0)
                AudioManager.instance.PlaySoundpool(source,Sounds.AsphaltFootsteps);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Finish"))
        {
            RoomManager.instance.trial.NotifyTrialComplete(true);
        }
    }
}
