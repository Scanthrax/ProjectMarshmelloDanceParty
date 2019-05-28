using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TrialEscape : Trial
{
    public override void Start()
    {
        base.Start();

        // you MUST place logic after the base method, since important things such as the roomManager reference are established there
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();

        // update logic can be placed either before or after the base method
        // it is good practice to place your logic after the base in order to keep things consistent with the Start base method

        // once you set the win condition, use this function to notify the room manager that the trial is a success
        if (false)
            NotifyTrialComplete(true);
    }
}
