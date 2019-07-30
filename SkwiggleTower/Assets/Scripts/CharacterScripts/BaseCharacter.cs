using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BaseCharacter : MonoBehaviour
{
    /// <summary>
    /// The maximum amount of health this character has
    /// </summary>
    [SerializeField]
    protected int maxHealth;

    /// <summary>
    /// The amount of health the character has at the moment
    /// </summary>
    [SerializeField]
    protected int currentHealth;

    /// <summary>
    /// Returns the unit interval (percentage) of this character's health
    /// </summary>
    public float percentHealth { get { return (float)currentHealth / maxHealth; } }


    public Stat damage;
    public Stat armor;

    public event System.Action<int, int> OnHealthChanged; //Delegate On Health Changed

    public Action onHeathChanged; 



    public Ability primary;
    public Ability secondary;
    public Ability ultimate;





    /// <summary>
    /// The audio source that plays a sound when they get hit by an attack (e.g. a rock impact sound on armor)
    /// </summary>
    public AudioSource hitSource;

    /// <summary>
    /// The audio source that plays a grunt when damage is recieved
    /// </summary>
    public AudioSource gruntSource;


    public SpriteRenderer characterRenderer;

    #region Invincibility flashing
    [Header("Invincibility flashing")]
    [Range(5f,20f)]
    public float flashSpeed;
    [Range(0f,1f)]
    public float minAlpha, maxAplha;
    #endregion



    private void Start()
    {
       
    }

    private void Update()
    {

    }

    void FlashCharacter()
    {
        var diff = maxAplha - minAlpha;
        var topDiff = 1 - maxAplha;

        characterRenderer.color = new Color(characterRenderer.color.r, characterRenderer.color.g, characterRenderer.color.b,
            (Mathf.Sin(Time.time * flashSpeed)
            * 0.5f * diff)
            + (1 - diff * 0.5f) - topDiff);
    }
}
