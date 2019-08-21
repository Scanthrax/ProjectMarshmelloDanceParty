//Author:   Ron Weeden
//Modified: 6/11/2019

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

/// <summary>
/// Base trial script
/// </summary>
[System.Serializable]
public class Trial : MonoBehaviour
{

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
    [HideInInspector]
    public bool started = false;




    public bool debug;



    public delegate void TrialEndEvent(bool success);
    public event TrialEndEvent trialEndEvent;


    /// <summary>
    /// Gives a notification that the trial has ended; the result of the trial is recieved/sent as a parameter
    /// </summary>
    /// <param name="win">Was the trial a success or a failure?</param>
    /// <returns></returns>
    public virtual void NotifyTrialComplete(bool success)
    {
        if (started && !trialCompleted)
        {
            Debug.Log("The trial " + trialName + " was a " + (success ? "success!" : "failure"));

            // determine which cutscene to play based on whether the trial was a success
            // there is not yet a Failure cutscene, so use the Success cutscene for now
            var cutscene = success ? RoomManager.instance.successCutscene : RoomManager.instance.failureCutscene;
            cutscene.Play();

            // we have completed the trial
            trialCompleted = true;

            trialEndEvent?.Invoke(success);

        }
        else
            Debug.Log("The trial is attempting to complete while the game has not started OR the trial has already been completed.");

        // I'm not sure if we will need a return type?
        //return success;
    }

    /// <summary>
    /// The Update method that the monobehaviour calls on every frame; different from the trial's Update logic method
    /// </summary>
    public virtual void Update()
    {
        // if the trial has started AND the trial has not been completed, we perform the Update logic of the trial
        if (!trialCompleted && started)
        {
            // perform the trial's update logic
            UpdateLogic();
        }

    }

    /// <summary>
    /// The trial's Update logic; by default, the round's timer ticks down
    /// </summary>
    public virtual void UpdateLogic()
    {
        // decrease the timer
        timer -= Time.deltaTime;
        // display timer in UI
        RoomManager.instance.timerText.text = Mathf.CeilToInt(timer).ToString();

        // if the timer reaches 0, the trial is a failure
        if (timer <= 0f)
            NotifyTrialComplete(false);

        if (debug)
        {
            if (Input.GetKeyDown(KeyCode.O))
                NotifyTrialComplete(true);
            if (Input.GetKeyDown(KeyCode.P))
                NotifyTrialComplete(false);
        }
    }

    /// <summary>
    /// Monobehaviour Start method
    /// </summary>
    public virtual void Start()
    {

        // set timer to the duration of the trial
        timer = duration;
        // display timer in UI
        //roomManager.timerText.text = Mathf.CeilToInt(timer).ToString();

        foreach (var spawner in RoomManager.instance.listOfSpawners)
        {
            trialEndEvent += spawner.DisableSpawnerOnWin;
        }

    }

    /// <summary>
    /// Starts the trial by playing the Intro cutscene
    /// </summary>
    public virtual void StartTrial()
    {
        print("Starting Trial now: " + trialName);
        // only start the trial if it has not already been started
        if (!started)
            RoomManager.instance.introCutscene.Play();
        else
            Debug.Log("Attempting to start the trial when it has already started");
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
        }
    }

}
