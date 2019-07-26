using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.LWRP;

/// <summary>
/// Valarie Script: UML created 
/// </summary>
public class PlayerStats : CharacterStats
{
    //Inherits from generic CharacterStats 

    // Start is called before the first frame update

    public int currentUlt, maxUlt;

    public float percentUlt { get { return (float)currentUlt / maxUlt; } }

    public int poisonStacks;

    public Light2D poisonBlade;

    public AudioSource UltSound;

    public bool fullUltCharge { get { return currentUlt >= maxUlt; } }

    void Start()
    {
        AttackManager.instance.onAttackChanged += onAttackChanged; //for multiple types of attacks
    }

    void onAttackChanged(AttackModifier newAttack, AttackModifier defaultAttack)
    {
        if (newAttack != null)
        {
            armor.AddModifier(newAttack.armorModifier);
            damage.AddModifier(newAttack.damageModifier);
        }

        if (defaultAttack != null)
        {
            armor.RemoveModifier(defaultAttack.armorModifier);
            damage.RemoveModifier(defaultAttack.damageModifier);
        }
    }

    public override void Die()
    {
        base.Die();
        //Kill Player
        //Play Death Animation
        //Restart Scene 
        //PlayerManager.instance.KillPlayer();
        GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);
        RoomManager.instance.trial.NotifyTrialComplete(false);
    }


    public void PlayUltSound()
    {
        UltSound.Play();
    }
}
