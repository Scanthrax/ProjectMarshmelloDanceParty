using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{

    public int amtOfActiveEnemies;
    public int maxAmtOfEnemies;


    bool canSpawnEnemy;



    bool canSpawn { get { return amtOfActiveEnemies < maxAmtOfEnemies; } }


    public Color color;


    public float spawnerRate;
    public float minTime, maxTime;
    public float timer, timeBetweenSpawns;

    [Header("Start Spawn")]
    public int startSpawnAmt;




    public delegate void OnSpawnEnemyStart(BaseCharacter character);
    public event OnSpawnEnemyStart OnSpawnStart;

    public delegate void OnSpawnEnemyDeath(BaseCharacter character);
    public event OnSpawnEnemyDeath onSpawnEnemyDeath;


    private void Start()
    {
        timer = 0f;
        timeBetweenSpawns = UnityEngine.Random.Range(minTime, maxTime);
        timeBetweenSpawns *= spawnerRate;

        for (int i = 0; i < startSpawnAmt; i++)
        {
            SpawnEnemy();
        }
    }


    private void Update()
    {
        if(canSpawn)
        {
            timer += Time.deltaTime;
            if(timer >= timeBetweenSpawns)
            {
                SpawnEnemy();
                timer = 0f;
                timeBetweenSpawns = UnityEngine.Random.Range(minTime, maxTime);
                timeBetweenSpawns *= spawnerRate;
            }
        }
    }


    public void SpawnEnemy()
    {
        if (!canSpawn) return;
        var enemy = ObjectPoolManager.instance.SpawnFromPool("Enemy", transform.position);
        if (!enemy)
        {
            Debug.LogWarning("cannot spawn enemy?");
            return;
        }


        var character = enemy.GetComponentInChildren<BaseCharacter>();

        if (!character)
        {
            Debug.LogWarning("no character?");
            return;
        }

        if(onSpawnEnemyDeath!=null)
            character.DeathEvent += onSpawnEnemyDeath.Invoke;

        //character.StartEvent += ColorEnemy;
        amtOfActiveEnemies++;
        character.DeathEvent += ReduceEnemyCounter;
    }


    public void ColorEnemy(BaseCharacter character)
    {
        character.characterRenderer.color = color;
    }


    public void ReduceEnemyCounter(BaseCharacter character)
    {
        amtOfActiveEnemies--;
    }


    //public void AddDeathEvents(BaseCharacter character)
    //{

    //}

}
