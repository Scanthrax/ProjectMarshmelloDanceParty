using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Valarie Script: Scriptable base object for Attack Modifiers, Items, and possibly classes UML created 6.12.19
/// </summary>
[CreateAssetMenu(fileName = "New Item", menuName = "Items")]
public class Item : ScriptableObject
{
    // Blueprint that all other scriptable object will inherit from

    new public string name = "New Item";
    public Sprite icon = null;
    public bool isDefaultItem = false;

    public virtual void Use()
    {
        //uses item, something happens

        Debug.Log("Using " + name);
    }

    public void RemoveFromInventory()
    {
        Inventory.instance.Remove(this); //Calls the Remove method from inventory script
    }
}
