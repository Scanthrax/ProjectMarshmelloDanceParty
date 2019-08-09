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

    new void Start()
    {
        base.Start();

        totalCharges = 0;
    }

    public override void ImmediateCast()
    {
        base.ImmediateCast();
        Cast();
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
            print("APPLY POISON");
            ability.buffs.Add(new BuffStruct(typeof(PoisonDebuff),this));
        }
        totalCharges += chargesToApply;
    }
}
