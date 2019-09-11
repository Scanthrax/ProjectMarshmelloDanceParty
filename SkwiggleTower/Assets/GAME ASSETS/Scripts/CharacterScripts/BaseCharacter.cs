using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;




public class BaseCharacter : MonoBehaviour, IPooledObject
{
    #region Health
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

    public bool dead { get { return currentHealth <= 0; } }
    #endregion

    #region Ult
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
    #endregion

    #region Abilities
    [Header("Abilities")]
    public Ability basicAttack;
    public Ability primary;
    public Ability secondary;
    public Ability ultimate;
    #endregion

    #region Components
    [Header("Components")]
    public SpriteRenderer characterRenderer;
    public Collider2D characterCollider;
    public Animator animator;
    public Rigidbody2D rigidBody;
    #endregion

    #region Audio Sources
    [Header("Audio")]
    /// <summary>
    /// The audio source that plays a sound when they get hit by an attack (e.g. a rock impact sound on armor)
    /// </summary>
    public AudioSource impactSource;

    /// <summary>
    /// The audio source that plays a grunt when damage is recieved
    /// </summary>
    public AudioSource mouthSource;

    /// <summary>
    /// The audio source responsible for playing footsteps
    /// </summary>
    public AudioSource footSource;

    public AudioSource abilitySource;

    public AudioSource spawnDeathSource;
    [Space(20)]
    #endregion

    #region Audio Clips
    public AudioClip deathSound;
    public AudioClip landingClip;
    #endregion

    #region Events
    public delegate void DeathHandler();
    public event DeathHandler DeathEvent;

    public delegate void InitializationHandler();
    public event InitializationHandler InitEvent;
    #endregion

    #region Scripts
    [Header("Scripts")]
    public BaseMovement movement;
    public BaseInput input;
    public AnimatorController animController;
    #endregion

    #region Containers
    [Header("Containers")]
    public Transform root;
    public Transform properties;
    #endregion


    public void Start()
    {
        OnObjectSpawn();
    }


    public virtual bool RecieveDamage(int damage, bool playImpact)
    {
        var dies = false;

        if(playImpact)
            impactSource.Play();

        currentHealth -= damage;

        if (dead)
        {
            mouthSource.clip = deathSound;
            Die();
            dies = true;
        }

        mouthSource.Play();


        return dies;
    }



    [ContextMenu("Death")]
    public virtual void Die()
    {
        DeathEvent?.Invoke();
    }



    public virtual void Init()
    {
        InitEvent += delegate { currentHealth = maxHealth; };
        InitEvent += delegate { currentUltCharge = 0; };

        InitEvent += delegate { basicAttack.timer = basicAttack.cooldownDuration; };
        if (primary)
            InitEvent += delegate { primary.timer = primary.cooldownDuration; };
        if (secondary)
            InitEvent += delegate { secondary.timer = secondary.cooldownDuration; };
        if (ultimate)
            InitEvent += delegate { ultimate.timer = ultimate.cooldownDuration; };

        InitEvent += SetAndPlaySpawnSound;

        if (animator)
            InitEvent += animController.SetAnimatorLayer;


        DeathEvent += KillEnemy;
        DeathEvent += SetAndPlayDeathSound;
    }



    public virtual void OnObjectSpawn()
    {
        Init();
        // make sure this is the last line in the method
        InitEvent?.Invoke();
    }

    public virtual void OnObjectDespawn()
    {
        DeathEvent = null;
        InitEvent = null;
        foreach (BaseBuff buff in GetComponents<BaseBuff>())
        {
            buff.EndBuff();
        }
    }



    public void SetAndPlaySpawnSound()
    {
        spawnDeathSource.clip = RoomManager.instance.spawnSound;
        spawnDeathSource.Play();
    }
    public void SetAndPlayDeathSound()
    {
        spawnDeathSource.clip = RoomManager.instance.despawnSound;
        spawnDeathSource.Play();
    }






    public void PlayFootstep()
    {
        AudioManager.instance.PlaySoundpool(footSource, Sounds.AsphaltFootsteps);
    }


    public void KillEnemy()
    {
        RoomManager.instance.StartCoroutine(KillEnemyCoroutine());
    }

    public IEnumerator KillEnemyCoroutine()
    {
        var particles = ObjectPoolManager.instance.SpawnFromPool(RoomManager.instance.deathParticles.transform, transform.position, Quaternion.identity);
        properties.gameObject.SetActive(false);
        movement.rigidBody.isKinematic = true;
        yield return new WaitForSeconds(1f);
        properties.gameObject.SetActive(true);
        movement.rigidBody.isKinematic = false;
        ObjectPoolManager.instance.BackToPool(root, false);
        ObjectPoolManager.instance.BackToPool(particles, false);
    }

}
