//Jacob Hreshchyshyn
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoguePoisonUlt : MonoBehaviour
{
    public int damage;
    public float tickInterval;

    public int tick, maxTicks;
    float timer;

    CharacterStats agent;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<CharacterStats>();
        GetComponent<SpriteRenderer>().color = Color.green;
        timer = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= tickInterval)
        {
            //Changes sprite color to green
            agent.TakeDamage(damage);
            Debug.Log("Taking damage" + damage);
            timer = 0f;
            tick++;

            if(tick >= maxTicks)
            {
                GetComponent<SpriteRenderer>().color = new Color(255, 255, 255);
                DestroyScriptInstance();
            }


        }
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
