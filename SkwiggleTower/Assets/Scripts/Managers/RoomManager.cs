//Author:   Ron Weeden
//Modified: 6/20/2019

using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System;
using TMPro;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class RoomManager : MonoBehaviour
{
    /// <summary>
    /// Singleton instance
    /// </summary>
    public static RoomManager instance;




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



    public int amtOfPlayers;


    bool isKeyboardDetected;
    bool[] isGamepadDetected;

    public List<PlayerInput> playerInputs;
    public int amtOfCurrentPlayers;


    public TextMeshPro primaryCtr, secondaryCtr, ultCtr;
    public TextMeshPro poisonCharges;


    public void Awake()
    {
        instance = this;


        isKeyboardDetected = false;
    }

    private void Start()
    {
        isKeyboardDetected = false;
        isGamepadDetected = new bool[4];
        amtOfCurrentPlayers = 0;

        // find all spawn gameobjects with the Respawn label & store them
        listOfSpawners = GameObject.FindGameObjectsWithTag("Respawn");


        
        amtOfPlayers = 0;

        //// the trial component will be dragged onto this gameobject through the inspector
        //trial = GetComponent<Trial>();

        //// if a trial exists, set the UI
        //if (trial)
        //{
        //    trialTextIntro.text = trial.trialName;
        //    trialTextUI.text = trial.trialName;

        //    trial.StartTrial();

        //}
        //else
        //{
            var listOfTrials = trials.GetComponents<Trial>();
            trial = listOfTrials[UnityEngine.Random.Range(0, listOfTrials.Length)];

            ///Debug.LogWarning("There is no trial set for this room!");
        //}


        //poisonCharges.text = 0.ToString();

        trialTextIntro.text = trial.trialName;
        trialTextUI.text = trial.trialName;
        trial.StartTrial();

        print("A & D to move cube");
        print("W to jump");
        print("Q to start trial");
        print("O & P to spawn objects");
        print("Trial: " + trial.trialName);



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


        if (amtOfCurrentPlayers <= 4)
        {
            #region Check for Keyboard player
            // if we haven't detected a keyboard
            if (!isKeyboardDetected)
            {
                // check for an action button
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    // we have now detected a keyboard
                    isKeyboardDetected = true;
                    // set the mappings of the player
                    playerInputs[amtOfCurrentPlayers].SetMappings(amtOfCurrentPlayers, false);
                    // we now have one more player
                    amtOfCurrentPlayers++;
                }
            }
            #endregion
            #region Check for Gamepad Players
            // check for 4 gamepads
            for (int i = 0; i < isGamepadDetected.Length; i++)
            {
                // if the gamepad is detected, continue through the loop
                if (isGamepadDetected[i]) continue;

                // check for the gamepad's action button
                if (Input.GetKeyDown("joystick " + (i + 1) + " button 0"))
                {
                    print("HERE I AM: " + (i + 1));
                    // set the mappings of the player
                    playerInputs[amtOfCurrentPlayers].SetMappings(i, true);
                    // we now have one more player
                    amtOfCurrentPlayers++;
                    isGamepadDetected[i] = true;
                }

            }
            #endregion
        }


        //primaryCtr.text = (playerInputs[0].GetComponent<CharacterStats>().primary.duration - playerInputs[0].GetComponent<CharacterStats>().primary.timer).ToString("F1");
        //secondaryCtr.text = (playerInputs[0].GetComponent<CharacterStats>().secondary.duration - playerInputs[0].GetComponent<CharacterStats>().secondary.timer).ToString("F1");
        //ultCtr.text = Mathf.RoundToInt(playerInputs[0].GetComponent<CharacterStats>().ultimate.duration - playerInputs[0].GetComponent<CharacterStats>().ultimate.timer).ToString();
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

    public void RestartScene()
    {
        SceneManager.LoadScene("PrototypeLevel_v1.1");
    }



}
