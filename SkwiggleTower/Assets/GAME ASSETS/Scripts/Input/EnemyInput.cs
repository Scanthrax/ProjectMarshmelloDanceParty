using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyInput : BaseInput
{
    public bool basicAttack;


    public delegate void EndMeleeEvent();
    public event EndMeleeEvent endMeleeEvent;

    private void Start()
    {
    }

    public void Update()
    {
        // do not progress if the controller is disabled
        if (!controllerEnabled) return;

        if(animator)
            SetAnimatorValues(character.basicAttack, "Melee", basicAttack, false, false);


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

    public override void ChangeDirection(bool right)
    {
        base.ChangeDirection(right);
        horizontal *= -1;
    }



}
