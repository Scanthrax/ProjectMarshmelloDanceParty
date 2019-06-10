﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Valarie Script: contains the scripts to equip and remove different attacks treated like items 
/// defines is a scriptable object 
/// </summary>
[CreateAssetMenu(fileName = "New Attack PU", menuName = "AttackMod")]
public class AttackModifier : Item
{
    public int equipSlot = 0; //UI slot filled

    public int damageModifier; //damage the attack will add to player
    public int armorModifier; //armor for player

    public override void Use()
    {
        base.Use();

        //Equip Attack
        AttackManager.instance.Equip(this);

        //Remove from inventory
        RemoveFromInventory();
    }
}
