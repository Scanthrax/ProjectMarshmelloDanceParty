using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Valarie Script: UML created 6.12
/// </summary>
public class AttackManager : MonoBehaviour
{
    #region Singleton
    public static AttackManager instance;

    void Awake()
    {
        instance = this;
    }
    #endregion

    AttackModifier[] currentAttack;

    public delegate void OnAttackChanged(AttackModifier newAttack, AttackModifier defaultAttack);
    public OnAttackChanged onAttackChanged;

    public int numAttacks; //number of different attacks available

    void Start()
    {
        currentAttack = new AttackModifier[numAttacks]; //instantiates attacks

    }

    public void Equip(AttackModifier newAttack)
    {
        int slotIndex = newAttack.equipSlot; //places attack in first slot in in-game ui

        AttackModifier defaultAttack = null;

        if (onAttackChanged != null)
        {
            onAttackChanged.Invoke(newAttack, defaultAttack);
        }

        currentAttack[slotIndex] = newAttack; //changes from default to special
    }

    public void Unequip(int slotIndex)
    {

        AttackModifier defaultAttack = currentAttack[slotIndex];

        //set to a default attack

        if (onAttackChanged != null) //Communicates that the attack has changed
        {
            onAttackChanged.Invoke(null, defaultAttack);
        }
    }
}
