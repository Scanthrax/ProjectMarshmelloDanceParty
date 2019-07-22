using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityPoisonUlt : Ability
{
    [Space(30)]


    [Tooltip("How many applications of the poison?")]
    public int charges;

    public float ticksPerSecond ;


    CharacterStats characterStats;


    new void Start()
    {
        base.Start();

        characterStats = GetComponent<CharacterStats>();
    }


    public override void Cast()
    {
        if (onCooldown) return;
        base.Cast();
        ApplyPoison();
        timer = 0f;

    }

    public void ApplyPoison()
    {
        (characterStats as PlayerStats).poisonStacks = charges;
        RoomManager.instance.poisonCharges.text = (character as PlayerStats).poisonStacks.ToString();
        (characterStats as PlayerStats).poisonBlade.gameObject.SetActive(true);
    }
}
