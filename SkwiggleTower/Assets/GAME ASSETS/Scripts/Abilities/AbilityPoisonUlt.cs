using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityPoisonUlt : Ability
{
    [Space(30)]


    [Tooltip("How many applications of the poison?")]
    public int chargesToApply;

    public int amtOfTicks;
    public float tickDuration;


    public int totalCharges;

    public List<Ability> poisonAbilities;

    public bool outOfCharges { get { return totalCharges <= 0; } }


    new void Start()
    {
        base.Start();

        totalCharges = 0;


    }


    public override void Cast()
    {
        base.Cast();
        ApplyPoison();
        characterMovement.animator.SetBool("AbilityActive", false);

    }

    public void ApplyPoison()
    {
        foreach (var ability in poisonAbilities)
        {
            ability.buffs.Add(Buff.Poison);
            ability.abilityDamageEvent += RemoveCharge;
        }
        totalCharges += chargesToApply;
    }

    public void RemovePoison()
    {
        foreach (var ability in poisonAbilities)
        {
            print("Removing poision");
            ability.buffs.Remove(Buff.Poison);
            ability.abilityDamageEvent -= RemoveCharge;
        }
    }

    public void RemoveCharge()
    {
        totalCharges--;
        if (totalCharges <= 0)
            RemovePoison();
    }
}
