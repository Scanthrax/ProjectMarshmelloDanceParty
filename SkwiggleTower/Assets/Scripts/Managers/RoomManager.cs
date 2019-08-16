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
    public Spawner[] listOfSpawners;


    /// <summary>
    /// Current amount of enemies in the room
    /// </summary>
    public int amountOfEnemies;

    /// <summary>
    /// Maximum amount of enemies allowed to be in the room at a given time
    /// </summary>
    public int maxAmountOfEnemies;

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
    public int cumulativeKills;



    public PlayableDirector introCutscene;
    public PlayableDirector successCutscene;
    public PlayableDirector failureCutscene;


    public TextMeshPro roundTextIntro, trialTextIntro;
    public TextMeshPro roundTextUI, trialTextUI;
    public TextMeshPro roundTextCompleted, trialTextCompleted;






    public TextMeshPro primaryCtr, secondaryCtr, ultCtr;
    public TextMeshPro poisonCharges;

    public GameObject trials;


    public BaseCharacter enemyPrefab;



    public ParticleSystem deathParticles;

    public void Awake()
    {
        instance = this;
    }

    private void Start()
    {


        // find all spawn gameobjects with the Respawn label & store them
        listOfSpawners = FindObjectsOfType<Spawner>();


        
        

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

        if (trial)
        {
            trialTextIntro.text = trial.trialName;
            if(trialTextUI) trialTextUI.text = trial.trialName;
            trial.StartTrial();
        }



    }


    private void Update()
    {


        //primaryCtr.text = (playerInputs[0].GetComponent<CharacterStats>().primary.duration - playerInputs[0].GetComponent<CharacterStats>().primary.timer).ToString("F1");
        //secondaryCtr.text = (playerInputs[0].GetComponent<CharacterStats>().secondary.duration - playerInputs[0].GetComponent<CharacterStats>().secondary.timer).ToString("F1");
        //ultCtr.text = Mathf.RoundToInt(playerInputs[0].GetComponent<CharacterStats>().ultimate.duration - playerInputs[0].GetComponent<CharacterStats>().ultimate.timer).ToString();
    }

    public void SpawnEnemy()
    {
        // if we cannot spawn anymore enemies, exit the method
        if (amountOfEnemies >= maxAmountOfEnemies)
            return;



        // randomly obtain the position of one of the spawners
        Vector3 position = listOfSpawners[UnityEngine.Random.Range(0, listOfSpawners.Length)].transform.position;

        // instantiate the gameobject at the position
        Instantiate(enemyPrefab, position, Quaternion.identity);

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
