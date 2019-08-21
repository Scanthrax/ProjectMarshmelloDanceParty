using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Buff { Poison, Speed}

public class BuffManager : MonoBehaviour
{
    [System.Serializable]
    public struct BuffStruct
    {
        public Buff buffType;
        public BaseBuff buff;
    }
    public List<BuffStruct> buffList;
    public Dictionary<Buff, BaseBuff> buffDictionary;


    public static BuffManager instance;

    private void Awake()
    {
        instance = this;


        buffDictionary = new Dictionary<Buff, BaseBuff>();
    }

}
