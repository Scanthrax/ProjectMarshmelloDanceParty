using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyInput : BaseInput
{
    public bool meleeAttack;


    public delegate void EndMeleeEvent();
    public event EndMeleeEvent endMeleeEvent;

    private void Start()
    {
        character.StartEvent += SetAnimator;

        SetAnimator(null);
    }

    public void Update()
    {
        // do not progress if the controller is disabled
        if (!controllerEnabled) return;

        SetAnimatorValues(character.melee, "Melee", meleeAttack, false, false);


        RelayInfo();
    }



    public void SetHorizontalAxis()
    {
        horizontal = 1f * faceDirection;
    }

    public void EndMeleeAttack()
    {
        if(endMeleeEvent != null)
            endMeleeEvent();
    }

    public void SetAnimator(BaseCharacter x)
    {
        animator.SetLayerWeight(animator.GetLayerIndex("Rogue Animation"), 0f);
        animator.SetLayerWeight(animator.GetLayerIndex("Warrior Animation"), 1f);
    }

}
