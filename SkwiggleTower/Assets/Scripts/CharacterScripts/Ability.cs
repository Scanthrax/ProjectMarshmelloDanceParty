using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ability : MonoBehaviour
{
    public float timer;
    public float duration;

    public bool onCooldown { get { return timer < duration; } }

    public float percentage { get { return timer / duration; } }

    public void Cast()
    {
        if(!onCooldown)
        {
            timer = 0f;
        }
    }

    public void Update()
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
