using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{


    public int maxAmtOfEnemies;

    float spawnerRate;
    public float minTime, maxTime;
    

    [Header("Start Spawn")]
    public int startSpawnAmt;

    public new bool enabled;

    public float spawnRange;


    [Header("Independent variables")]
    public int amtOfActiveEnemies;

    public Transform enemyToSpawn;
    bool canSpawn { get { return amtOfActiveEnemies < maxAmtOfEnemies; } }
    public float timer, timeBetweenSpawns;

    private void Start()
    {
        spawnerRate = 1f;
        timer = 0f;
        timeBetweenSpawns = UnityEngine.Random.Range(minTime, maxTime);
        timeBetweenSpawns *= spawnerRate;

        for (int i = 0; i < startSpawnAmt; i++)
        {
            SpawnEnemy();
        }

        enabled = true;
    }


    private void Update()
    {
        if(canSpawn && enabled)
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
        if (!canSpawn || !enabled) return;

        var enemy = ObjectPoolManager.instance.SpawnFromPool(enemyToSpawn, (Vector2)transform.position + UnityEngine.Random.insideUnitCircle * spawnRange, Quaternion.identity);

        if (!enemy)
        {
            Debug.LogWarning("did not spawn valid enemy?");
            return;
        }

        var character = enemy.GetComponentInChildren<BaseCharacter>();

        if (!character)
        {
            Debug.LogWarning("not an enemy?");
            return;
        }


        amtOfActiveEnemies++;

        character.DeathEvent += ReduceEnemyCounter;
    }






    public void ReduceEnemyCounter(BaseCharacter character)
    {
        amtOfActiveEnemies--;
    }


    public void DisableSpawnerOnWin(bool b)
    {
        print("trial ended. calling spawner event");

        if (b)
            enabled = false;
    }


    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, spawnRange);
    }

}
