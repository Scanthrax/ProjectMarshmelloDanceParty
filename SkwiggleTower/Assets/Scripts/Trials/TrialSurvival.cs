using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TrialSurvival : Trial
{
    // Matt Thompson
    // Last Modified: 6/10/19

    public override void Start()
    {
        base.Start();

        // you MUST place logic after the base method, since important things such as the roomManager reference are established there

        // May need to add a variable to ensure the player is actually alive at the end of the trial
    }

    // Method one of checking if all enemies defeated, essentially just checks if the number of enemies killed 
    public override void UpdateLogic()
    {
        // decrease the timer
        timer -= Time.deltaTime;
        // display timer in UI
        RoomManagerinstance.timerText.text = Mathf.CeilToInt(timer).ToString();

        // if the player survives until time runs out the trial is succesful
        if (timer <= 0f)
            NotifyTrialComplete(true);   
    }
}
