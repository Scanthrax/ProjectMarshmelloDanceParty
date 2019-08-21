using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPooledObject
{
    //event EventHandler OnObjectSpawn;
    //event EventHandler OnObjectDeSpawn;

    void OnObjectSpawn();
    void OnObjectDespawn();
}

public class ObjectPoolManager : MonoBehaviour
{


    [Serializable]
    public class Pool
    {
        public string tag;
        public Transform obj;
        public Transform container;
        public int count;
        public Queue<Transform> queue;
    }

    public static ObjectPoolManager instance;

    public List<Pool> pools;

    public Dictionary<string, Pool> poolDictionary;

    private void Awake()
    {
        instance = this;


        poolDictionary = new Dictionary<string, Pool>();


    }



    public Transform SpawnFromPool(Transform obj, Vector2 pos, Quaternion rot)
    {
        // check if the pool dictionary contains a pool for the object being passed in
        // create a new pool if necessary
        CheckForPool(obj);

        // we are either going to get an object from a pool or instantiate it for the 1st time
        Transform objToSpawn = null;

        // if there is an object in the queue, we grab the object & remove from queue
        if (poolDictionary[obj.name].queue.Count > 0)
            objToSpawn = poolDictionary[obj.name].queue.Dequeue();
        // otherwise we instantiate it & change the name so that it matches the pool's name (i.e. get rid of the (Clone) suffix)
        else
        {
            objToSpawn = Instantiate(obj, poolDictionary[obj.name].container);  // put the instantiated object in the container
            objToSpawn.name = obj.name;
        }

        // check if the object isn't valid
        if (!objToSpawn)
        {
            Debug.LogWarning("Null GameObject??");
            return null;
        }

        // initialize the object by activating it & setting pos & rotation
        objToSpawn.gameObject.SetActive(true);
        objToSpawn.position = pos;
        objToSpawn.rotation = rot;

        // call the item's Spawn method
        objToSpawn.GetComponentInChildren<IPooledObject>()?.OnObjectSpawn();

        // we're done; return the object
        return objToSpawn;
    }

    
    public void CheckForPool(Transform obj)
    {
        if (!poolDictionary.ContainsKey(obj.name))
        {
            Debug.LogWarning("Tag " + obj.name + " does not exist; creating new pool...");
            CreateNewPool(obj);
        }
    }

    public void BackToPool(Transform obj, bool active)
    {
        // check if the object is not valid
        if (!obj)
        {
            Debug.LogWarning("Attempting to put an invalid object in pool! Returning...");
            return;
        }

        CheckForPool(obj);



        obj.gameObject.SetActive(active);
        obj.GetComponentInChildren<IPooledObject>()?.OnObjectDespawn();

        poolDictionary[obj.name].queue.Enqueue(obj);
        print(poolDictionary[obj.name].queue.Count + poolDictionary[obj.name].tag);
        

        //PrintQueue(poolDictionary[tag]);

    }


    public void PrintQueue(Queue<Transform> q)
    {
        foreach (var item in q)
        {
            print(item.name);
        }
        print("Total: " + q.Count);
    }


    public void KillEnemy(BaseCharacter character)
    {
        StartCoroutine(KillEnemyCoroutine(character));
    }

    public IEnumerator KillEnemyCoroutine(BaseCharacter character)
    {
        var particles = SpawnFromPool(RoomManager.instance.deathParticles.transform, character.transform.position, Quaternion.identity);
        character.properties.gameObject.SetActive(false);
        character.characterMovement.rigidBody.isKinematic = true;
        yield return new WaitForSeconds(1f);
        character.properties.gameObject.SetActive(true);
        character.characterMovement.rigidBody.isKinematic = false;
        BackToPool(character.transform.parent,false);
        BackToPool(particles,false);
    }





    public void CreateNewPool(Transform objToPool)
    {
        Pool newPool = new Pool();
        newPool.obj = objToPool;
        newPool.tag = objToPool.name;
        newPool.queue = new Queue<Transform>();

        var container = new GameObject().transform;
        container.parent = transform;
        container.name = objToPool.name;

        newPool.container = container;

        

        poolDictionary.Add(newPool.tag,newPool);

        Debug.Log("New pool with tag " + newPool.tag + " created");
    }

    //public Transform AddNewObjToPool(Transform obj)
    //{
    //    return Instantiate(obj, container);
    //}

}
