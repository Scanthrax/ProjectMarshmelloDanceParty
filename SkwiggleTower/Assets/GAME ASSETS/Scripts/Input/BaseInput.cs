using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseInput : MonoBehaviour
{
    /// <summary>
    /// The horizontal axis that influences the player's left/right movement
    /// </summary>
    [HideInInspector] public float horizontal;

    /// <summary>
    /// Stores whether the jump button is held
    /// </summary>
    [HideInInspector] public bool jumpHold;


    /// <summary>
    /// Stores whether the primary button is held
    /// </summary>
    [HideInInspector] public bool meleeHold;

    /// <summary>
    /// Stores whether the primary button is held
    /// </summary>
    [HideInInspector] public bool primaryHold;

    /// <summary>
    /// Stores whether the secondary button is held
    /// </summary>
    [HideInInspector] public bool secondaryHold;

    /// <summary>
    /// Stores whether the ult button is held
    /// </summary>
    [HideInInspector] public bool ultHold;



    /// <summary>
    /// The animator that controls the character
    /// </summary>
    public Animator animator;

    /// <summary>
    /// The character that we will feeding inputs to
    /// </summary>
    public BaseCharacter character;

    /// <summary>
    /// The character's movement controller that we will feeding inputs to
    /// </summary>
    public BaseMovement movement;


    public AnimatorController animController;


    /// <summary>
    /// used to check if a player controller is enabled
    /// </summary>
    public bool controllerEnabled;


    /// <summary>
    /// returns the sign of the horizontal axis
    /// </summary>
    public int horizontalSign { get { return horizontal != 0 ? (int)Mathf.Sign(horizontal) : 0; } }


    /// <summary>
    /// Is the character facing right or left?
    /// </summary>
    public bool faceRight;

    /// <summary>
    /// returns 1 when the character is facing right; -1 when facing left
    /// </summary>
    public int faceDirection { get { return faceRight ? 1 : -1; } }


    /// <summary>
    /// Changes the direction that this character is facing
    /// </summary>
    public void ChangeDirection() { faceRight = !faceRight; FlipCharacter(); }
    /// <summary>
    /// Changes the direction that this character is facing
    /// </summary>
    public void ChangeDirection(bool right) { faceRight = right; FlipCharacter(); }

    /// <summary>
    /// Flips the character's x-scale
    /// </summary>
    public void FlipCharacter()
    {
        transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x) * faceDirection, transform.localScale.y, transform.localScale.z);
    }




    public void RelayInfo()
    {
        // relay the horizontal axis to the character's movement controller
        movement.horizontalAxis = horizontal;

        // if we're moving and the movement direction doesn't match what's recorded...
        if (horizontalSign != 0 && horizontalSign != faceDirection)
            // change the direction of the character to match the movement
            ChangeDirection(horizontalSign == 1);

        movement.faceDirection = faceDirection;
    }


    public void StartAbility()
    {
        //print("starting ability");
        animator.SetBool("AbilityActive", true);
    }
    public void EndAbility()
    {
        //print("ending ability");
        animator.SetBool("AbilityActive", false);
    }

    public void StartMelee()
    {
        //print("start melee");
        character.melee.Cast();
    }
    public void EndMelee()
    {
        character.melee.End();
    }

    public void StartPrimary()
    {
        character.primary.Cast();
    }
    public void StartSecondary()
    {
        character.secondary.Cast();
    }


    public void EnableControls()
    {
        controllerEnabled = true;
    }
    public void DisableControls()
    {
        //print("Disable Controls");

        controllerEnabled = false;
        horizontal = 0f;
        movement.horizontalAxis = horizontal;
    }


    public void SetAnimatorValues(Ability ability, string abilityType, bool hold, bool press, bool release)
    {
        
        if (!animator)
        {
            Debug.LogWarning("The input has not detected an animator!", this);
            return;
        }


        if (!hold && !release)
            return;

        if (!ability)
        {
            Debug.LogWarning("An attempt was made to set animator values of a null " + abilityType + " ability", this);
            return;
        }


        //print(abilityType);

        if (ability.activateOnHold)
        {
            if (hold)
            {
                if (!ability.onCooldown && !animator.GetBool("AbilityActive"))
                {
                    SetTrigger(ability, abilityType);
                }
            }
            else if (release)
            {
                if (animator.GetBool("AbilityActive"))
                {
                    animator.SetTrigger(abilityType + "Release");
                }
            }
        }
        else
        {
            if (press)
            {
                if (!ability.onCooldown && !animator.GetBool("AbilityActive"))
                {
                    SetTrigger(ability, abilityType);
                }
            }
            else if (release)
            {
                if (animator.GetBool("AbilityActive"))
                    animator.SetTrigger(abilityType + "Release");
            }
        }

    }


    public void SetTrigger(Ability ability, string abilityStr)
    {
        if (!ability.performInAir && !movement.isOnGround) return;


        if (abilityStr == "Ult")
        {
            var player = character as PlayableCharacter;
            if (player)
            {
                if (!player.fullUltCharge)
                    return;
                else
                    player.ResetUltCharge();
            }
        }

        animator.SetTrigger(abilityStr + "Trigger");
        
        if (!ability.passive)
        {
            StartAbility();
        }
        else
        {
            ability.ImmediateCast();
        }


    }


}
