using UnityEngine;

/// <summary>
/// Valarie Script : UML created
/// </summary>
//We first ensure this script runs before all other player scripts to prevent laggy
//inputs
//[DefaultExecutionOrder(-100)]
[RequireComponent(typeof(BaseCharacter))]
[RequireComponent(typeof(AnimatorController))]
public class PlayerInput : BaseInput
{



    /// <summary>
    /// An identification that is given to each playable character determines which controller will control the player
    /// </summary>
    public int playerID;

    /// <summary>
    /// The prefix that determines which mappings to read from
    /// </summary>
    public string inputPrefix;





    private void Start()
    {
        faceRight = true;
    }

    public new void Update()
    {
        base.Update();


        ProcessInputs();



        RelayInfo();

    }


    void ProcessInputs()
    {
        //obtain the horizontal axis input
        horizontal = Input.GetAxis(inputPrefix + "Movement");

        // Ability Inputs
        var pHold = Input.GetAxis(inputPrefix + "Primary")      > 0.5f || Input.GetButton(inputPrefix + "Primary");
        var sHold = Input.GetAxis(inputPrefix + "Secondary")    > 0.5f || Input.GetButton(inputPrefix + "Secondary");
        var uHold = Input.GetAxis(inputPrefix + "Ult")          > 0.5f || Input.GetButton(inputPrefix + "Ult");
        var jHold = Input.GetAxis(inputPrefix + "Jump")         > 0.5f || Input.GetButton(inputPrefix + "Jump");
        var mHold = Input.GetAxis(inputPrefix + "Melee")        > 0.5f || Input.GetButton(inputPrefix + "Melee");

        // determine whether the triggers were pressed on this frame
        var pPressed = pHold != primaryHold     && primaryHold == false;
        var sPressed = sHold != secondaryHold   && secondaryHold == false;
        var uPressed = uHold != ultHold         && ultHold == false;
        var jPressed = jHold != jumpHold        && jumpHold == false;
        var mPressed = mHold != meleeHold       && meleeHold == false;
        // determine whether the triggers were released on this frame
        var pReleased = pHold != primaryHold    && primaryHold == true;
        var sReleased = sHold != secondaryHold  && secondaryHold == true;
        var uReleased = uHold != ultHold        && ultHold == true;
        var jReleased = jHold != jumpHold       && jumpHold == true;
        var mReleased = mHold != meleeHold      && meleeHold == true;




        SetAnimatorValues(character.melee, "Melee", mHold, mPressed, mReleased);
        SetAnimatorValues(character.primary, "Primary", pHold, pPressed, pReleased);
        SetAnimatorValues(character.secondary, "Secondary", sHold, sPressed, sReleased);
        SetAnimatorValues(character.ultimate, "Ult", uHold, uPressed, uReleased);




        if (jPressed)
            movement.Jump();


        // record all of the temporary variables to compare on next update
        primaryHold = pHold;
        secondaryHold = sHold;
        ultHold = uHold;
        jumpHold = jHold;
        meleeHold = mHold;

    }




    public void SetMappings(int id, bool gamepad)
    {
        playerID = id + 1;

        // generate the prefix by determining which player is trying to connect
        inputPrefix = !gamepad ? "KB" : "P" + playerID.ToString();

        print("enabling " + inputPrefix);
        controllerEnabled = true;
    }



    public void StartAbility()
    {
        animator.SetBool("AbilityActive", true);
    }
    public void EndAbility()
    {
        animator.SetBool("AbilityActive", false);
    }

    public void StartMelee()
    {
        character.melee.Cast();
    }
    public void StartPrimary()
    {
        character.primary.Cast();
    }
    public void StartSecondary()
    {
        character.secondary.Cast();
    }


    public void SetAnimatorValues(Ability ability, string abilityType, bool hold, bool press, bool release)
    {
        if (!animator)
        {
            Debug.LogWarning("The player input has not detected an animator!", this);
            return;
        }


        if (!hold && !release)
            return;

        if (!ability)
        {
            Debug.LogWarning("An attempt was made to set animator values of a null " + abilityType + " ability", this);
            return;
        }




        if (ability.activateOnHold)
        {
            if (hold)
            {
                if (!ability.onCooldown && !animator.GetBool("AbilityActive"))
                {
                    animator.SetTrigger(abilityType + "Trigger");
                    animator.SetBool("AbilityActive", true);
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
                    animator.SetTrigger(abilityType + "Trigger");
                    animator.SetBool("AbilityActive", true);
                }
            }
            else if (release)
            {
                if (animator.GetBool("AbilityActive"))
                    animator.SetTrigger(abilityType + "Release");
            }
        }

    }


    public void EnableControls()
    {
        controllerEnabled = true;
    }
    public void DisableControls()
    {
        movement.horizontalAxis = 0f;
        controllerEnabled = false;
    }



}
