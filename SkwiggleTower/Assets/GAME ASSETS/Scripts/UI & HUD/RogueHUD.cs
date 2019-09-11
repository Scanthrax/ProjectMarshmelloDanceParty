using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RogueHUD : PlayerHUD
{
    public TextMeshPro poisonCharges;


    protected new void Start()
    {
        if(character)
        {
            character.ultimate.AbilityCastEvent += delegate { poisonCharges.text = (character.ultimate as AbilityPoisonUlt).totalCharges.ToString(); };
            foreach (var ability in (character.ultimate as AbilityPoisonUlt).poisonAbilities)
            {
                ability.BuffApplicationEvent += delegate { poisonCharges.text = ((character.ultimate as AbilityPoisonUlt).totalCharges-1).ToString(); };
            }
        }
    }

}
