using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonDebuff : BaseBuff
{
    public int damage;
    public float tickInterval = 1f;

    public int tick;
    public int maxTicks = 3;
    float timer;


    public override void StartBuff()
    {
        base.StartBuff();
        StartCoroutine(Poison());
    }

    public IEnumerator Poison()
    {
        timer = 0f;
        tick = 0;
        character.characterRenderer.color = Color.green;

        while (tick < maxTicks)
        {
            if (timer < tickInterval)
            {
                timer += Time.deltaTime;
            }
            else
            {
                character.RecieveDamage(damage,false);
                timer = 0f;
                tick++;
            }
            yield return null;
        }

        EndBuff();

    }




    public override void Init()
    {
        //damage = affector.baseDamage;

        //maxTicks = (affector as AbilityPoisonUlt).amtOfTicks;

        //tickInterval = (affector as AbilityPoisonUlt).tickDuration;

        damage = 1;

        maxTicks = 3;

        tickInterval = 1;
    }

    public override void EndBuff()
    {
        character.characterRenderer.color = Color.white;
        DestroyScriptInstance();
    }
}
