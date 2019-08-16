using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityShield : Ability
{
    public float moveSpeedMult;

    float baseMovespeed;


    public new void Start()
    {
        base.Start();
        baseMovespeed = characterMovement.movementSpeed;
    }

    public override void Cast()
    {
        base.Cast();

        characterMovement.movementSpeed = baseMovespeed * moveSpeedMult;

    }






    public void UnCast()
    {
        characterMovement.movementSpeed = baseMovespeed;
    }
}
