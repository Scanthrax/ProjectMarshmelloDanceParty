using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackManager : MonoBehaviour
{
    #region Singleton
    public static AttackManager instance;

    void Awake()
    {
        instance = this;
    }
    #endregion

    public SkinnedMeshRenderer targetMesh;
    AttackModifier[] currentAttack;
    SkinnedMeshRenderer[] currentMeshes;

    public delegate void OnAttackChanged(AttackModifier newAttack, AttackModifier defaultAttack);
    public OnAttackChanged onAttackChanged;

    public int numAttacks; //number of different attacks available

    public void Start()
    {
        currentAttack = new AttackModifier[numAttacks]; //instantiates attacks
        currentMeshes = new SkinnedMeshRenderer[numAttacks];
    }

    public void Equip(AttackModifier newAttack)
    {
        int slotIndex = newAttack.equipSlot; //places attack in first slot in in-game ui

        AttackModifier defaultAttack = null;

        if (onAttackChanged != null)
        {
            onAttackChanged.Invoke(newAttack, defaultAttack);
        }

        currentAttack[slotIndex] = newAttack; //changes from default to special
        SkinnedMeshRenderer newMesh = Instantiate<SkinnedMeshRenderer>(newAttack.mesh); //Creates mesh on player
        newMesh.transform.parent = targetMesh.transform; //Player Mesh

        newMesh.bones = targetMesh.bones;
        newMesh.rootBone = targetMesh.rootBone;
        currentMeshes[slotIndex] = newMesh;
    }

    public void Unequip(int slotIndex)
    {
        if (currentMeshes[slotIndex] != null)
        {
            Destroy(currentMeshes[slotIndex].gameObject);
        }
        AttackModifier defaultAttack = currentAttack[slotIndex];

        //set to a default attack

        if (onAttackChanged != null) //Communicates that the attack has changed
        {
            onAttackChanged.Invoke(null, defaultAttack);
        }
    }
}
