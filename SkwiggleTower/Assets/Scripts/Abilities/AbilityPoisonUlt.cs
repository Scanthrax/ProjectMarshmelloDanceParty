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


    BuffStruct buffStruct;

    Dictionary<Ability, BuffStruct> dict;

    List<Ability> tempList;

    new void Start()
    {
        base.Start();

        totalCharges = 0;

        dict = new Dictionary<Ability, BuffStruct>();

        buffStruct = new BuffStruct(typeof(PoisonDebuff),this);
    }


    public override void Cast()
    {
        base.Cast();
        ApplyPoison();
        characterMovement.animator.SetBool("AbilityActive", false);

    }

    public void ApplyPoison()
    {
        //foreach (var ability in poisonAbilities)
        //{
        //    ability.buffs.Add(buffStruct);
        //}
        totalCharges += chargesToApply;
    }

    public void RemovePoison()
    {
        foreach (var ability in poisonAbilities)
        {
            ability.buffs.Clear();
        }
    }

    public void RemoveCharge()
    {
        totalCharges--;
    }
}
