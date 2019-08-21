using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathParticles : MonoBehaviour, IPooledObject
{
    public ParticleSystem ps;

    public void OnObjectSpawn()
    {
        ps.Play();
    }

    public void OnObjectDespawn()
    {
        
    }
}
