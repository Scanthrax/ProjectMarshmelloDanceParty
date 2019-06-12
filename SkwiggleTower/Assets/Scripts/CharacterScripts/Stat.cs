using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Valarie Script: 
/// </summary>
[System.Serializable]
public class Stat 
{
    [SerializeField]
    public int baseValue; //Starting value

    //List of modifiers that change the baseValue
    private List<int> modifiers = new List<int>();

    //Get the final value after applying modifiers
    public int GetValue()
    {
        int finalValue = baseValue;
        modifiers.ForEach(x => finalValue += x);
        return finalValue;
    }

    //Add new modifier to List
    public void AddModifier(int modifier)
    {
        if (modifier != 0)
        {
            modifiers.Add(modifier);
        }
    }

    //Remove modifier from List
    public void RemoveModifier(int modifier)
    {
        if (modifier != 0)
        {
            modifiers.Remove(modifier);
        }
    }
}
