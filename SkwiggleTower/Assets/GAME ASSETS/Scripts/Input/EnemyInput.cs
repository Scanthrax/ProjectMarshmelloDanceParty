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



}
