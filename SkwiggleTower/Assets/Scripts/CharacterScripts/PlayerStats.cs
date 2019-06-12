using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Valarie Script: 
/// </summary>
public class PlayerStats : CharacterStats
{
    //Inherits from generic CharacterStats 

    // Start is called before the first frame update
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
        PlayerManager.instance.KillPlayer();
    }
}
