using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestImpactSound : MonoBehaviour
{
    public AudioSource impactSource;
    public AudioSource groanSource;

    public void PlayImpact()
    {
        impactSource.Play();
        AudioManager.instance.PlaySoundpool(groanSource, Sounds.Groans);
    }

}
