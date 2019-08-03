using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.LWRP;

public class Candle : MonoBehaviour
{
    public List<Light2D> lights;
    public float intensity;
    public float offset;
    public float speed;
    List<float> noiseOffset;

    private void Start()
    {
        noiseOffset = new List<float>();
        for (int i = 0; i < lights.Count; i++)
        {
            noiseOffset.Add(Random.value * 1000f);
        }
    }

    private void Update()
    {
        for (int i = 0; i < lights.Count; i++)
        {
            var light = lights[i];
            light.intensity = intensity + Mathf.PerlinNoise(noiseOffset[i] + Time.time * speed,0f) * offset;
        }
    }
}
