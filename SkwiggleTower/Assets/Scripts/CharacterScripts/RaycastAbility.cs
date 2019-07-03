using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Valarie Script for ability system using scriptable objects
/// </summary>
[CreateAssetMenu(menuName ="Abilities")]
public class RaycastAbility : Ability
{
    public int aDamage = 1;
    public float aRange = 50f;
    public float hitForce = 100f;
    public Color aColor = Color.white;

   // private RayCastShootTriggerable rcShoot;

    public override void Initialize(GameObject obj)
    {
       // rcShoot = obj.GetComponent<RayCastShootTriggerable>();
       //set damage, range, and hitforce
       // set new material 
       //set color of material

        throw new System.NotImplementedException();
    }

    public override void TriggerAbility()
    {
        //call Fire in Ability object
    }
}
