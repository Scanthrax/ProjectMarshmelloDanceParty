using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImpactTest : MonoBehaviour
{

    AudioSource source;
    // the index determines which impact sound is played from the array of sounds
    public int index;

    private void Start()
    {
        source = AudioManager.instance.AddSource(gameObject,Sounds.GroundImpact, index,SoundChannels.GroundImpact);
    }


    void OnCollisionEnter2D(Collision2D col)
    {
        // make an impact sound when the magnitude is greater than a threshold
        if (col.relativeVelocity.magnitude > 4)
        {
            //print("impact!");
            source.Play();
        }
    }
}
