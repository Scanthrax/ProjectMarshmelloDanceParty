using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TrialEscape : Trial
{
    public override void UpdateLogic()
    {
        // if the timer reaches 0, the trial is a failure
        if (timer <= 0f)
            NotifyTrialComplete(false);

        base.UpdateLogic();
    }
}
