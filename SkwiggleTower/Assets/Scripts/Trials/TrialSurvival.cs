using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TrialSurvival : Trial
{
    // Matt Thompson
    // Last Modified: 8/9/19

    public override void Start()
    {
        base.Start();

        // doubles the max amount of enemies that can be in a room at a single time, so as to increase difficulty
        // may need to be changed or balanced
        //RoomManager.instance.maxAmount *= 2;

        // should also adjust enemy spawn rates, so they spawn more quickly/often, not sure what variable should be adjusted to achieve this

        // you MUST place logic after the base method, since important things such as the roomManager reference are established there

        // May need to add a variable to ensure the player is actually alive at the end of the trial
    }

    // Method one of checking if all enemies defeated, essentially just checks if the number of enemies killed 
    public override void UpdateLogic()
    {
        // decrease the timer
        timer -= Time.deltaTime;
        // display timer in UI
        RoomManager.instance.timerText.text = Mathf.CeilToInt(timer).ToString();

        // if the player survives until time runs out the trial is succesful, should not occur if player(s) is/are dead
        if (timer <= 0f)
        {
            //RoomManager.instance.maxAmount /= 2;
            NotifyTrialComplete(true);
        }
    }
}
