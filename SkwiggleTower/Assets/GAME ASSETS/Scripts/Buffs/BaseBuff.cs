using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseBuff : MonoBehaviour
{
    public BaseCharacter character;
    public Ability applicant;
    public Ability affector;

    public delegate void BuffTickHandler();
    public event BuffTickHandler BuffTickEvent;

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

    public void TickEvent()
    {
        BuffTickEvent?.Invoke();
    }

    public void DestroyScriptInstance()
    {
        Destroy(this);
    }

}
