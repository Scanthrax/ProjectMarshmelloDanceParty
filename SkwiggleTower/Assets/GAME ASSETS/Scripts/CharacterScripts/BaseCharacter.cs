using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;




public class BaseCharacter : MonoBehaviour, IPooledObject
{
    [Header("Health")]
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


    [Header("Ult")]
    /// <summary>
    /// The maximum amount of health this character has
    /// </summary>
    [SerializeField] protected int maxUltCharge;

    /// <summary>
    /// The amount of health the character has at the moment
    /// </summary>
    [SerializeField] protected int currentUltCharge;

    /// <summary>
    /// Returns the unit interval (percentage) of this character's health
    /// </summary>
    public float percentUltCharge { get { return (float)currentUltCharge / maxUltCharge; } }


    public bool fullUltCharge { get { return currentUltCharge >= maxUltCharge; } }



    #region Abilities
    [Header("Abilities")]
    public Ability melee;
    public Ability primary;
    public Ability secondary;
    public Ability ultimate;
    #endregion


    #region Audio Sources
    [Header("Audio")]
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

    public AudioSource spawnDeathSource;
    [Space(20)]
    #endregion

    #region Audio Sources
    public AudioClip spawnSound;
    public AudioClip deathSound;
    public AudioClip landingClip;
    [Space(30)]
    #endregion

    public SpriteRenderer characterRenderer;


    public Transform audioSources;

    public AnimatorController animController;

    public Transform root;
    public Transform properties;


    #region Events
    public delegate void DeathHandler(BaseCharacter character);
    public event DeathHandler DeathEvent;

    public delegate void SpawnHandler(BaseCharacter character);
    public event SpawnHandler SpawnEvent;
    #endregion

    public string poolTag;


    public BaseMovement characterMovement;
    public StateManager stateManager;





    public virtual void RecieveDamage(int damage, bool playHit)
    {
        gruntSource.Play();

        if(playHit)
            hitSource.Play();

        currentHealth -= damage;

        if (currentHealth <= 0)
            Die();

    }

    [ContextMenu("Death")]
    public virtual void Die()
    {
        DeathEvent?.Invoke(this);
    }

    public virtual void OnDeath(bool b)
    {
        if(b)
            DeathEvent?.Invoke(this);
    }


    public int RecursiveDamage(int damage, ref int source)
    {
        source -= damage;
        return source;
    }



    public virtual void OnObjectSpawn()
    {
        

        currentHealth = maxHealth;

        melee.timer = melee.cooldownDuration;

        spawnDeathSource.clip = spawnSound;
        spawnDeathSource.Play();


        DeathEvent += ObjectPoolManager.instance.KillEnemy;
        DeathEvent += SetAndPlayDeathSound;

        SpawnEvent += animController.SetAnimatorLayer;

        // make sure this is the last line in the method
        SpawnEvent?.Invoke(this);
    }

    public virtual void OnObjectDespawn()
    {
        DeathEvent = null;
        SpawnEvent = null;
        foreach (BaseBuff buff in GetComponents<BaseBuff>())
        {
            buff.EndBuff();
        }
    }



    public void SetAndPlaySpawnSound(BaseCharacter x)
    {
        spawnDeathSource.clip = spawnSound;
        spawnDeathSource.Play();
    }
    public void SetAndPlayDeathSound(BaseCharacter x)
    {
        spawnDeathSource.clip = deathSound;
        spawnDeathSource.Play();
    }

    public void PlayFootstep()
    {
        AudioManager.instance.PlaySoundpool(footstepSource, Sounds.AsphaltFootsteps);
    }

}
