using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{

    public int amtOfActiveSpawns;

    public EnemyStats spawn;

    bool canSpawnEnemy;

    int amtOfEnemies;

    float timeBetweenSpawns;

    bool canSpawn { get { return amtOfEnemies < amtOfActiveSpawns; } }

    private void Start()
    {
        SpawnEnemy();
    }


    private void Update()
    {
        
    }


    public void SpawnEnemy()
    {
        var enemy = Instantiate(spawn, transform.transform.position, Quaternion.identity);
        enemy.notifySpawner += SpawnEnemy;
        amtOfEnemies++;
    }

}
