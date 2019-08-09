using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonDebuff : BaseBuff
{
    public int damage;
    public float tickInterval;

    public int tick, maxTicks;
    float timer;


    private new void Start()
    {
        base.Start();
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
                character.RecieveDamage(damage);
                timer = 0f;
                tick++;
            }
            yield return null;
        }

        character.characterRenderer.color = Color.white;
        DestroyScriptInstance();

    }


    void DestroyScriptInstance()
    {
        Destroy(this);
    }

    public void Init(int damage, int amtOfTicks, float tickDuration)
    {
        this.damage = damage;

        maxTicks = amtOfTicks;

        tickInterval = tickDuration;
    }
}
