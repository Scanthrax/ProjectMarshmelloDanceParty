using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonDebuff : BaseBuff
{
    public float tickInterval = 1f;
    public int tick;
    public int maxTicks = 3;
    float timer;


    public override void StartBuff()
    {
        base.StartBuff();

        BuffTickEvent += PoisonTick;


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
                TickEvent();
            }
            yield return null;
        }

        EndBuff();

    }

    public void PoisonTick()
    {
        applicant.DealDamage(character);
        timer = 0f;
        tick++;
    }


    public override void Init()
    {
        //damage = affector.baseDamage;

        //maxTicks = (affector as AbilityPoisonUlt).amtOfTicks;

        //tickInterval = (affector as AbilityPoisonUlt).tickDuration;


        maxTicks = 3;

        tickInterval = 1;
    }

    public override void EndBuff()
    {
        character.characterRenderer.color = Color.white;
        DestroyScriptInstance();
    }
}
