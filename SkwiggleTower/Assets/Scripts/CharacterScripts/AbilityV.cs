using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Val Scriptable Object Version of Ability System
/// Abstract class that should be overriden
/// Never exists in the hierachy
/// </summary>
public abstract class AbilityV : ScriptableObject
{
    public string aName = "New Ability";
    public Sprite aSprite;
    public AudioClip aSound;
    public float aBaseCoolDown;

    public abstract void Initialize(GameObject obj); //replaces Awake or Start
    public abstract void TriggerAbility();


}
