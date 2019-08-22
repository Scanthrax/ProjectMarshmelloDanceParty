using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum Buff { Poison, Speed}

public class BuffManager : MonoBehaviour
{

    public Dictionary<Buff, Type> buffDictionary;


    public static BuffManager instance;

    private void Awake()
    {
        instance = this;


        buffDictionary = new Dictionary<Buff, Type>();


        buffDictionary.Add(Buff.Poison, typeof(PoisonDebuff));
        buffDictionary.Add(Buff.Speed, typeof(SpeedBuff));
    }



    public Type GetBuff(Buff buff)
    {
        return buffDictionary[buff];
    }

}
