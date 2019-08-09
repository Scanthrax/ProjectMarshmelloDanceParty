using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseBuff : MonoBehaviour
{
    public BaseCharacter character;
    public Ability affector;

    public void Start()
    {
        character = GetComponent<BaseCharacter>();
    }

}
