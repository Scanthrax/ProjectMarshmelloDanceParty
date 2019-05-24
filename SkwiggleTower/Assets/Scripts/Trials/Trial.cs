using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[System.Serializable]
[DisallowMultipleComponent]
public class Trial : MonoBehaviour
{


    /// <summary>
    /// Gives a notification that the trial has ended; the result of the trial is recieved/sent as a parameter
    /// </summary>
    /// <param name="win">Was the trial a success or a failure?</param>
    /// <returns></returns>
    public virtual bool NotifyTrialComplete(bool success)
    {
        if (started && !trialCompleted)
        {
            Debug.Log("The trial " + trialName + " was a " + (success ? "success!" : "failure"));

            trialCompleted = true;
            return success;
        }
        return false;
    }


    public virtual void Update()
    {
        // if the trial has started AND the trial has not been completed
        if (!trialCompleted && started)
        {
            // perform the trial's update logic
            UpdateLogic();
        }
    }

    /// <summary>
    /// The trial's Update logic
    /// </summary>
    public virtual void UpdateLogic()
    {
        timer -= Time.deltaTime;
        roomManager.timerText.text = Mathf.CeilToInt(timer).ToString();
        
    }


    public virtual void Start()
    {
        Debug.Log("Trial name: " + trialName);
        timer = duration;
        roomManager.timerText.text = Mathf.CeilToInt(timer).ToString();
    }

    /// <summary>
    /// This method occurs when the trial actually begins, as opposed to the Start() method
    /// </summary>
    public virtual void Begin()
    {
        if (!trialCompleted && !started)
        {
            Debug.Log("Trial name " + trialName + " has begun!");
            // the trial has now started; the Update can occur now
            started = true;

            // the timer will start at the duration & tick down from there
            timer = duration;
        }
    }

    /// <summary>
    /// The duration of the trial, in seconds
    /// </summary>
    public float duration;
    /// <summary>
    /// The timer ticks down to 0
    /// </summary>
    protected float timer;

    /// <summary>
    /// The name of the trial
    /// </summary>
    public string trialName;

    /// <summary>
    /// Has the trial been completed?
    /// </summary>
    protected bool trialCompleted = false;

    /// <summary>
    /// Has the trial started?
    /// </summary>
    protected bool started = false;

    /// <summary>
    /// Reference to the room manager
    /// </summary>
    public RoomManager roomManager;
}
