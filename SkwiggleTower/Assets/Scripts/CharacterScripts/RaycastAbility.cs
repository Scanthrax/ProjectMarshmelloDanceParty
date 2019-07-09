using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Valarie Script for ability system using scriptable objects
/// </summary>
[CreateAssetMenu(menuName ="Abilities")]
public class RaycastAbility : Ability
{
    public int damage = 1;
    public float range = 50f;
    public float hitForce = 100f;
    public Color laserColor = Color.white;

    private RaycastShootTriggerable rcShoot;

    public override void Initialize(GameObject obj)
    {
        rcShoot = obj.GetComponent<RaycastShootTriggerable>();
        rcShoot.Initialize();

        rcShoot.gunDamage = damage;
        rcShoot.weaponRange = range;
        rcShoot.hitForce = hitForce;
        rcShoot.laserLine.material = new Material(Shader.Find("Unlit/Color"));
        rcShoot.laserLine.material.color = laserColor;

    }

    public override void TriggerAbility()
    {
        rcShoot.Fire();
    }

}
