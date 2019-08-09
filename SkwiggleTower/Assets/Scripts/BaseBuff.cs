using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseBuff : MonoBehaviour
{
    protected BaseCharacter character;

    public void Start()
    {
        character = GetComponent<BaseCharacter>();
    }

}
