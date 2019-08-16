//Jacob Hreshchyshyn
//A test script that, when p is pressed, activates/deactivates Rogue's poison ult
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RogueUltTestActivation : MonoBehaviour
{
    public int poisonTicks;
    private int startPoisonTicks;
    public bool active;
    private int activeCheck;
    // Start is called before the first frame update
    void Start()
    {
        activeCheck = 1;
        active = false;
        startPoisonTicks = poisonTicks;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("p") && activeCheck == 1)
        {
            active = true;
            Debug.Log("Ult activated");
            activeCheck = 0;
        }
        if(active && poisonTicks > 0)
        {
            poisonTicks--;
        }
        else if(poisonTicks <= 0 && activeCheck == 0)
        {
            active = false;
            poisonTicks = startPoisonTicks;
            Debug.Log("Ult deactivated");
            activeCheck = 1;
        }
    }
}
