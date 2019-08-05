using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BaseCharacter : MonoBehaviour
{
    /// <summary>
    /// The maximum amount of health this character has
    /// </summary>
    [SerializeField] protected int maxHealth;

    /// <summary>
    /// The amount of health the character has at the moment
    /// </summary>
    [SerializeField] protected int currentHealth;

    /// <summary>
    /// Returns the unit interval (percentage) of this character's health
    /// </summary>
    public float percentHealth { get { return (float)currentHealth / maxHealth; } }


    public Ability melee;
    public Ability primary;
    public Ability secondary;
    public Ability ultimate;




    #region Audio Sources
    /// <summary>
    /// The audio source that plays a sound when they get hit by an attack (e.g. a rock impact sound on armor)
    /// </summary>
    public AudioSource hitSource;

    /// <summary>
    /// The audio source that plays a grunt when damage is recieved
    /// </summary>
    public AudioSource gruntSource;

    /// <summary>
    /// The audio source responsible for playing footsteps
    /// </summary>
    public AudioSource footstepSource;

    public AudioSource abilitySource;
    #endregion


    public AudioClip landingClip;

    public SpriteRenderer characterRenderer;









    public void PlayFootstep()
    {
        AudioManager.instance.PlaySoundpool(footstepSource, Sounds.AsphaltFootsteps);
    }


    public void SlingStretch()
    {
    }

    public void RecieveDamage(int damage)
    {
        gruntSource.Play();
    }

}
