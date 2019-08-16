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
        public int size;
    }


    public Queue<Transform> poolQueue;

    public static ObjectPoolManager instance;

    public List<Pool> pools;

    public Dictionary<string, Queue<Transform>> poolDictionary;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        poolDictionary = new Dictionary<string, Queue<Transform>>();

        foreach (var pool in pools)
        {
            Queue<Transform> objectPool = new Queue<Transform>();

            var container = new GameObject().transform;
            container.parent = transform;
            container.name = pool.tag;

            for (int i = 0; i < pool.size; i++)
            {
                var obj = Instantiate(pool.obj,container);
                obj.gameObject.SetActive(false);
                objectPool.Enqueue(obj);
            }
            poolDictionary.Add(pool.tag, objectPool);
        }
    }


    public Transform SpawnFromPool(string tag, Vector2 pos)
    {
        if(!poolDictionary.ContainsKey(tag))
        {
            Debug.LogWarning("tag " + tag + " does not exist!");
            return null;
        }

        var objToSpawn = poolDictionary[tag].Count > 0 ? poolDictionary[tag].Dequeue() : null;

        if (!objToSpawn)
        {
            Debug.LogWarning("there are no more objects in the queue: " + tag);
            return null;
        }

        objToSpawn.gameObject.SetActive(true);
        objToSpawn.position = pos;
        objToSpawn.rotation = Quaternion.identity;

        //// fix this soon? have a separate queue for active vs inactive
        //poolDictionary[tag].activeQueue.Enqueue(objToSpawn);

        var pooledObj = objToSpawn.GetComponentInChildren<IPooledObject>();
        if(pooledObj != null)
        {
            pooledObj.OnObjectSpawn();
        }

        return objToSpawn;
    }

    public Transform SpawnFromPool(string tag, Vector2 pos, Quaternion rot)
    {
        if (!poolDictionary.ContainsKey(tag))
        {
            Debug.LogWarning("tag " + tag + " does not exist!");
            return null;
        }

        var objToSpawn = poolDictionary[tag].Count > 0 ? poolDictionary[tag].Dequeue() : null;

        if (!objToSpawn)
        {
            Debug.LogWarning("there are no more objects in the queue: " + tag);
            return null;
        }

        objToSpawn.gameObject.SetActive(true);
        objToSpawn.position = pos;
        objToSpawn.rotation = rot;

        //// fix this soon? have a separate queue for active vs inactive
        //poolDictionary[tag].activeQueue.Enqueue(objToSpawn);

        var pooledObj = objToSpawn.GetComponentInChildren<IPooledObject>();
        if (pooledObj != null)
        {
            pooledObj.OnObjectSpawn();
        }

        PrintQueue(poolDictionary[tag]);

        return objToSpawn;
    }



    public void BackToPool(string tag, Transform obj, bool active)
    {
        if (!poolDictionary.ContainsKey(tag))
        {
            Debug.LogWarning("tag " + tag + " does not exist!");
            return;
        }

        if(!obj)
        {
            Debug.LogWarning("Attempting to put an invalid object in pool: " + tag);
            return;
        }

        obj.gameObject.SetActive(active);

        poolDictionary[tag].Enqueue(obj);
        

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
        var particles = SpawnFromPool("DeathParticles", character.transform.position);
        character.properties.gameObject.SetActive(false);
        character.characterMovement.rigidBody.isKinematic = true;
        yield return new WaitForSeconds(1f);
        character.properties.gameObject.SetActive(true);
        character.characterMovement.rigidBody.isKinematic = false;
        BackToPool("Enemy", character.root,false);
        BackToPool("DeathParticles", particles,false);
    }


}
