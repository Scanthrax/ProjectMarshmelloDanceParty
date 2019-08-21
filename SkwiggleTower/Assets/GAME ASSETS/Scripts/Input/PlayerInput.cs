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

    public void Update()
    {
        // do not progress if the controller is disabled
        if (!controllerEnabled) return;


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







}
