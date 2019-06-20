using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestImpactSound : MonoBehaviour
{
    public AudioSource source;

    public void PlayImpact()
    {
        source.Play();
    }

}
