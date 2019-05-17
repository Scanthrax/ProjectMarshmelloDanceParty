using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
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


    private void Start()
    {
        // find all spawn gameobjects with the Respawn label & store them
        listOfSpawners = GameObject.FindGameObjectsWithTag("Respawn");
    }


    private void Update()
    {
        // Spawn one type of object with O
        if (Input.GetKeyDown(KeyCode.O))
            SpawnEnemy(1);

        // Spawn another type of object with P
        if (Input.GetKeyDown(KeyCode.P))
            SpawnEnemy(2);
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
        Vector3 position = listOfSpawners[Random.Range(0, listOfSpawners.Length)].transform.position;

        // instantiate the gameobject at the position
        Instantiate(temp, position, Quaternion.identity);

        // increase the count of enemies in the room
        amountOfEnemies++;
    }
}
