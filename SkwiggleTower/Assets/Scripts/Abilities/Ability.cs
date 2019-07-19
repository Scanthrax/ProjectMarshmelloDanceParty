using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ability : MonoBehaviour
{
    [HideInInspector]
    public float timer;

    [Header("Base Attributes")]
    public float duration;

    public bool activateOnHold;

    public int damage;

    public bool onCooldown { get { return timer < duration; } }

    public float percentage { get { return timer / duration; } }

    protected CharacterStats character;

    public virtual void Start()
    {
        timer = duration;
        character = GetComponent<CharacterStats>();
    }

    public virtual void Cast()
    {
        if(!onCooldown)
        {
            timer = 0f;
        }
    }

    public virtual void Update()
    {
        if(onCooldown)
        {
            timer += Time.deltaTime;

            if (!onCooldown)
            {
                print("cooldown is done!");
                timer = duration;
            }
        }

    }

}
