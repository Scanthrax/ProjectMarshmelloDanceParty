using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum AbilityType { Primary, Secondary, Ultimate}

public class Ability : MonoBehaviour
{
    /// <summary>
    /// The float that determines how far along we are on the cooldown
    /// </summary>
    [HideInInspector]
    public float timer;

    /// <summary>
    /// The duration of the cooldown; i.e. how long should this ability be on cooldown?
    /// </summary>
    [Header("Base Attributes")]
    public float cooldownDuration;

    /// <summary>
    /// Should this ability be able to trigger when it comes off of cooldown & the ability input is held, or do we await a button press?
    /// </summary>
    public bool activateOnHold;

    /// <summary>
    /// The amount of base damage this ability deals
    /// </summary>
    public int baseDamage;

    /// <summary>
    /// Tells us whether this ability is on cooldown
    /// </summary>
    public bool onCooldown { get { return timer < cooldownDuration; } }

    /// <summary>
    /// Gives us the percentage of completion of the cooldown
    /// </summary>
    public float percentage { get { return timer / cooldownDuration; } }

    public float remainingTime { get { return cooldownDuration - timer;} }


    /// <summary>
    /// The movement component of the character
    /// </summary>
    public BaseMovement characterMovement;


    public Sprite abilityIcon;

    public Action cooldownHUD;

    /// <summary>
    /// The audio clip that will play on cast
    /// </summary>
    public AudioClip abilitySoundClip;

    /// <summary>
    /// Do we want to play a sound upon casting the ability?
    /// </summary>
    public bool playSoundOnCast;

    /// <summary>
    /// The audio source that will play the ability sound clip
    /// </summary>
    AudioSource abilitySoundSource;

    public bool performInAir;


    public int ultGain;

    public bool passive;

    public List<BuffStruct> buffs;

    public virtual void Start()
    {
        timer = cooldownDuration;
        abilitySoundSource = characterMovement.character.abilitySource;
        buffs = new List<BuffStruct>();
    }

    public virtual void ImmediateCast()
    {

    }


    public virtual void Cast()
    {
        // Start the ability cooldown
        StartCoroutine(StartCooldown());

        
        if (playSoundOnCast && abilitySoundClip && abilitySoundSource)
        {
            // load the source with the ability sound clip and play
            abilitySoundSource.clip = abilitySoundClip;
            abilitySoundSource.Play();
        }

        if (cooldownHUD != null) cooldownHUD.Invoke();
    }



    public IEnumerator StartCooldown()
    {
        // reset the timer
        timer = 0f;

        // increase the timer and wait a frame while on cooldown
        while(onCooldown)
        {
            timer += Time.deltaTime;
            yield return null;
        }
        // once we get past the loop, the cooldown has finished

        // set the timer as the cooldownDuration so that the percentage = 100% complete
        timer = cooldownDuration;
        // call the cooldown finished method
        CooldownFinished();

        //print("cooldown is done!");
    }



    public void DealDamage(BaseCharacter character)
    {
        foreach (var debuff in buffs)
        {
            print("SHOULD APPLY POISON");
            var debuffVar = character.gameObject.AddComponent(debuff.buffType) as BaseBuff;
            debuffVar.affector = debuff.caller;
        }


        var playableChar = characterMovement.character as PlayableCharacter;
        if (playableChar)
            playableChar.RecieveUltCharge(ultGain);

        character.RecieveDamage(baseDamage);
    }

    public void CooldownFinished()
    {

    }

    public struct BuffStruct
    {
        public Type buffType;
        public Ability caller;

        public BuffStruct(Type type, Ability abilty)
        {
            buffType = type;
            caller = abilty;
        }
    }

}
