//Jacob Hreshchyshyn
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoguePoisonUlt : MonoBehaviour
{
    public int damage = 1;
    public int timer = 180;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer--;
        if (timer > 0 && timer % 15 == 0)
        {
            GetComponent<CharacterStats>().TakeDamage(damage);
            Debug.Log("Taking damage" + damage);
        }
        else if(timer <= 0)
        {
           DestroyScriptInstance();
        }
    }
    void DestroyScriptInstance()
    {
        Destroy(this);
    }
}
