using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseBuff : MonoBehaviour
{
    public BaseCharacter character;
    public Ability affector;


    public virtual void StartBuff()
    {
        character = GetComponent<BaseCharacter>();
    }

    public virtual void Init()
    {
        
    }

    public virtual void EndBuff()
    {

    }


    public void DestroyScriptInstance()
    {
        Destroy(this);
    }

}
