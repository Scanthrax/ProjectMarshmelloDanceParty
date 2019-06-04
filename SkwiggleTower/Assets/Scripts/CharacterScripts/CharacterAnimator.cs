using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimator : MonoBehaviour
{
    const float animSmoothTime = .1f; //variable to smooth out animations 

    public AnimationClip replaceableAttackAnim;
    public AnimationClip[] defaultAttackAnimSet; //Allows us to create an array of animations to swap through
    protected AnimationClip[] currentAttackAnimSet;

    protected Animator animator;
    protected Combat charCombat;
    protected AnimatorOverrideController overrideController;

    // Start is called before the first frame update

    protected virtual void Start() //Alows different object to inherit from this animator script
    {
        animator = GetComponentInChildren<Animator>(); //reference to animator
        charCombat = GetComponent<Combat>(); //refrence to combat scripts
        charCombat.OnAttack += OnAttack; //Subscribes it to OnAttack Delegate

        overrideController = new AnimatorOverrideController(animator.runtimeAnimatorController); //Allows us to swap out clips for other clips
        animator.runtimeAnimatorController = overrideController;

        currentAttackAnimSet = defaultAttackAnimSet;

    }

    // Update is called once per frame

    protected virtual void Update()
    {
       // assign movement speed float speedPercent 
    }

    protected virtual void OnAttack()
    {
        animator.SetTrigger("IsAttack"); 
        int attackIndex = Random.Range(0, currentAttackAnimSet.Length); //selects randomly among the different attack animations
        overrideController[replaceableAttackAnim.name] = currentAttackAnimSet[attackIndex]; //overrides the basic animation
    }
}
