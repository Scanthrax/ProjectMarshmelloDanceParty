//Author:   Ron Weeden
//Modified: 6/20/2019

using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System;
using TMPro;
using UnityEngine.Playables;

public class RoomManager : MonoBehaviour
{
    public static RoomManager instance;

    public void Awake()
    {
        instance = this;
    }


    /// <summary>
    /// Contains all of the spawners located in the room
    /// </summary>
    GameObject[] listOfSpawners;

    /// <summary>
    /// test objects to spawn
    /// </summary>
    public GameObject testEnemyPrefab;

    /// <summary>
    /// Current amount of enemies in the room
    /// </summary>
    public int amountOfEnemies;

    /// <summary>
    /// Maximum amount of enemies allowed to be in the room at a given time
    /// </summary>
    public int maxAmount;

    /// <summary>
    /// The trial for the current room
    /// </summary>
    [HideInInspector]
    public Trial trial;

    /// <summary>
    /// The text that displays the remaining time of the trial
    /// </summary>
    public TextMeshPro timerText;


    public int amtOfEnemiesKilled;




    public PlayableDirector introCutscene;
    public PlayableDirector successCutscene;
    public PlayableDirector failureCutscene;


    public TextMeshPro roundTextIntro, trialTextIntro;
    public TextMeshPro roundTextUI, trialTextUI;
    public TextMeshPro roundTextCompleted, trialTextCompleted;


    public GameObject trials;


    private void Start()
    {
        // find all spawn gameobjects with the Respawn label & store them
        listOfSpawners = GameObject.FindGameObjectsWithTag("Respawn");





        // the trial component will be dragged onto this gameobject through the inspector
        trial = GetComponent<Trial>();

        // if a trial exists, set the UI
        if (trial)
        {
            trialTextIntro.text = trial.trialName;
            trialTextUI.text = trial.trialName;

            trial.StartTrial();

        }
        else
        {
            var listOfTrials = trials.GetComponents<Trial>();
            trial = listOfTrials[UnityEngine.Random.Range(0, listOfTrials.Length)];

            ///Debug.LogWarning("There is no trial set for this room!");
        }


        print("A & D to move cube");
        print("W to jump");
        print("Q to start trial");
        print("O & P to spawn objects");


    }


    private void Update()
    {
        // Spawn one type of object with O
        if (Input.GetKeyDown(KeyCode.K))
            SpawnEnemy();

        // Spawn another type of object with P
        if (Input.GetKeyDown(KeyCode.L))
            SpawnEnemy();

        if (trial && Input.GetKeyDown(KeyCode.Q))
        {
            trial.StartTrial();
        }


    }

    public void SpawnEnemy()
    {
        // if we cannot spawn anymore enemies, exit the method
        if (amountOfEnemies >= maxAmount)
            return;



        // randomly obtain the position of one of the spawners
        Vector3 position = listOfSpawners[UnityEngine.Random.Range(0, listOfSpawners.Length)].transform.position;

        // instantiate the gameobject at the position
        Instantiate(testEnemyPrefab, position, Quaternion.identity);

        // increase the count of enemies in the room
        amountOfEnemies++;
    }

    /// <summary>
    /// Used by a signal in the intro cutscene to begin the trial
    /// </summary>
    public void BeginTrial()
    {
        trial.Begin();
    }


    public void OnEnemyDeath()
    {
        print("Room manager increase amt of enemies killed");
        amtOfEnemiesKilled++;
        amountOfEnemies--;
    }
}
