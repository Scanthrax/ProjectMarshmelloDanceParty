using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;




public class BaseCharacter : MonoBehaviour, IPooledObject
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


    public Transform audioSources;


    public Transform root;
    public Transform properties;

    public BaseMovement characterMovement;

    public delegate void DeathHandler(BaseCharacter character);
    public event DeathHandler DeathEvent;

    public delegate void StartHandler(BaseCharacter character);
    public event StartHandler StartEvent;
    



    public void Start()
    {

        OnObjectSpawn();

        DeathEvent += ObjectPoolManager.instance.KillEnemy;
    }

    public void PlayFootstep()
    {
        AudioManager.instance.PlaySoundpool(footstepSource, Sounds.AsphaltFootsteps);
    }


    public void SlingStretch()
    {
    }

    public virtual void RecieveDamage(int damage)
    {
        gruntSource.Play();
        hitSource.Play();

        currentHealth -= damage;

        if (currentHealth <= 0)
            OnDeath();

    }

    [ContextMenu("Death")]
    public virtual void OnDeath()
    {
        print("I DIED");
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



    public void OnObjectSpawn()
    {
        StartEvent?.Invoke(this);

        print("FULL HEALTH");
        currentHealth = maxHealth;

        melee.timer = melee.cooldownDuration;
        
    }

    public void OnObjectDespawn()
    {
        
    }




    //public void MoveGameObjects()
    //{
    //    StartCoroutine(AttachGameObjects());
    //}

    public IEnumerator AttachGameObjects()
    {
        ObjectPoolManager.instance.SpawnFromPool("DeathParticles", transform.position).GetComponent<ParticleSystem>().Play();
        properties.gameObject.SetActive(false);
        yield return new WaitForSeconds(1f);
        print("THIS SHOULD BE GETTING CALLED");

        properties.gameObject.SetActive(true);
    }
}
