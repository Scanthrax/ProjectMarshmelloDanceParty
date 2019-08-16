using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Parent : MonoBehaviour
{
    [Header("Enemy AI Attributes")]
    // Not sure how the AI will function, I put in "deadzone" and "activezone" for AI detection later
    public float deadzone;
    public float activezone;

    [Header("Enemy Abilities")]
    // public Ability primaryAbility;
    // public Ability secondaryAbility;
    // public Ability passiveAbility;

    [Header("Enemy Movement Data")]
    public float verticalSpeed;
    public float horizontalSpeed;
    public float jumpSpeed; // maybe not needed?
    public float jumpGravity; // maybe not needed?

    [Header("Enemy Sound Data")]
    public AudioClip walkSound;
    public float walkVolume;

    [Header("Melee Attack Data")]
    public float m_attackFrequency;
    public int m_attackDamage;

    [Header("Ranged Attack Data")]
    public float r_attackFrequency;
    public int r_attackDamage;


    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
