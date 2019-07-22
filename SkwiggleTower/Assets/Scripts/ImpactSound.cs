using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImpactSound : MonoBehaviour
{
    public AudioClip defaultSound;
    public AudioClip rockSound;
    public AudioClip arrowtSound;
    public AudioClip fruitSound;


    public AudioClip GetSound(string projectile)
    {
        if (!defaultSound)
            Debug.LogWarning("This object does not have a default impact sound!", gameObject);

        var tempSound = projectile == "rock"    ? rockSound :
                        projectile == "arrow"   ? arrowtSound :
                        projectile == "fruit"   ? fruitSound : defaultSound;

        if (!tempSound)
            tempSound = defaultSound;

        return tempSound;
    }
}
