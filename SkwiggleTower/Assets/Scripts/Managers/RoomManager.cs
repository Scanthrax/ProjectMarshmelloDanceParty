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
    public GameObject test1, test2;

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


    public PlayableDirector introCutscene;
    public PlayableDirector successCutscene;
    public PlayableDirector failureCutscene;


    public TextMeshPro roundTextIntro, trialTextIntro;
    public TextMeshPro roundTextUI, trialTextUI;
    public TextMeshPro roundTextCompleted, trialTextCompleted;


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
        }
        else
            Debug.LogWarning("There is no trial set for this room!");


        print("A & D to move cube");
        print("W to jump");
        print("Q to start trial");
        print("O & P to spawn objects");


    }


    private void Update()
    {
        // Spawn one type of object with O
        if (Input.GetKeyDown(KeyCode.O))
            SpawnEnemy(1);

        // Spawn another type of object with P
        if (Input.GetKeyDown(KeyCode.P))
            SpawnEnemy(2);

        if (trial && Input.GetKeyDown(KeyCode.Q))
        {
            trial.StartTrial();
        }


    }

    public void SpawnEnemy(int type)
    {
        // if we cannot spawn anymore enemies, exit the method
        if (amountOfEnemies >= maxAmount)
            return;

        // store a temporary gameobject that will be instantiated
        // the type of object is determined by the input parameter
        GameObject temp;

        // if the type is 1, spawn one type of object
        if (type == 1)
            temp = test1;
        // otherwise, spawn the other type of object
        else
            temp = test2;

        // randomly obtain the position of one of the spawners
        Vector3 position = listOfSpawners[UnityEngine.Random.Range(0, listOfSpawners.Length)].transform.position;

        // instantiate the gameobject at the position
        Instantiate(temp, position, Quaternion.identity);

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
}
