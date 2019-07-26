using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityPoisonUlt : Ability
{
    [Space(30)]


    [Tooltip("How many applications of the poison?")]
    public int charges;

    public int amtOfTicks;
    public float tickDuration;


    CharacterStats characterStats;


    new void Start()
    {
        base.Start();

        characterStats = GetComponent<CharacterStats>();
    }


    public override void Cast()
    {
        GetComponent<Animator>().SetBool("UltActive", false);
        if (onCooldown) return;
        if (!GetComponent<PlayerStats>().fullUltCharge) return;
        base.Cast();
        ApplyPoison();
        (characterStats as PlayerStats).PlayUltSound();
        
        GetComponent<PlayerStats>().currentUlt = 0;
        timer = 0f;

    }

    public void ApplyPoison()
    {
        (characterStats as PlayerStats).poisonStacks = charges;
        RoomManager.instance.poisonCharges.text = (character as PlayerStats).poisonStacks.ToString();
        (characterStats as PlayerStats).poisonBlade.gameObject.SetActive(true);
    }
}
