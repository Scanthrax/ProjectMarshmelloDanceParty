using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorController : MonoBehaviour
{
    public Animator animator;

    public string animLayer;


    public void SetAnimatorLayer()
    {
        for (int i = 0; i < animator.layerCount; i++)
        {
            animator.SetLayerWeight(i, 0f);
        }
        animator.SetLayerWeight(animator.GetLayerIndex(animLayer), 1f);
    }

}
