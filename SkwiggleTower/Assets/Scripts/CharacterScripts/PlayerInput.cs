using UnityEngine;

/// <summary>
/// Valarie Script : UML created
/// </summary>
//We first ensure this script runs before all other player scripts to prevent laggy
//inputs
//[DefaultExecutionOrder(-100)]
[RequireComponent(typeof(BaseCharacter))]
public class PlayerInput : MonoBehaviour
{

    [HideInInspector] public float horizontal;      //Float that stores horizontal input
    [HideInInspector] public bool jumpHold;         //Bool that stores jump pressed

    [HideInInspector] public bool primaryHold;            //Bool that stores attack  held 

    [HideInInspector] public bool secondaryHold;            //Bool that stores attack  held 

    [HideInInspector] public bool ultHold;            //Bool that stores attack  held 

    bool readyToClear;                              //Bool used to keep input in sync

    public int playerID;

    /// <summary>
    /// The prefix that determines which mappings to read from
    /// </summary>
    public string inputPrefix;

    /// <summary>
    /// used to check if a player controller is enabled
    /// </summary>
    public bool enabled;

    Animator animator;


    BaseCharacter character;
    public BaseMovement movement;

    private void Start()
    {
    }

    void Update()
    {
        if (!enabled) return;

        ProcessInputs();


        movement.horizontalAxis = horizontal;
    }


    void ProcessInputs()
    {
        //Accumulate horizontal axis input
        horizontal = Input.GetAxis(inputPrefix + "Movement");

        // Ability Inputs
        var pHold = Input.GetAxis(inputPrefix + "Primary")      > 0.5f || Input.GetButton(inputPrefix + "Primary");
        var sHold = Input.GetAxis(inputPrefix + "Secondary")    > 0.5f || Input.GetButton(inputPrefix + "Secondary");
        var uHold = Input.GetAxis(inputPrefix + "Ult")          > 0.5f || Input.GetButton(inputPrefix + "Ult");
        var jHold = Input.GetAxis(inputPrefix + "Jump")         > 0.5f || Input.GetButton(inputPrefix + "Jump");

        // determine whether the triggers were pressed on this frame
        var pPressed = pHold != primaryHold     && primaryHold == false;
        var sPressed = sHold != secondaryHold   && secondaryHold == false;
        var uPressed = uHold != ultHold         && ultHold == false;
        var jPressed = jHold != jumpHold        && jumpHold == false;
        // determine whether the triggers were released on this frame
        var pReleased = pHold != primaryHold    && primaryHold == true;
        var sReleased = sHold != secondaryHold  && secondaryHold == true;
        var uReleased = uHold != ultHold        && ultHold == true;
        var jReleased = jHold != jumpHold       && jumpHold == true;


        if (animator)
        {
            if (pPressed)
            {
                animator.SetBool("PrimaryHold", pHold);
                if (!character.primary.onCooldown)
                {
                    animator.SetTrigger("PrimaryTrigger");
                }
            }
            if (sPressed)
            {
                animator.SetBool("SecondaryHold", sHold);
                if (!character.secondary.onCooldown)
                {
                    animator.SetTrigger("SecondaryTrigger");
                }
            }
            if (uPressed)
            {
                animator.SetBool("UltimateHold", uHold);
                if (!character.ultimate.onCooldown)
                {
                    animator.SetTrigger("UltimateTrigger");
                }
            }
        }



        if (jPressed)
            movement.Jump();

        primaryHold = pHold;
        secondaryHold = sHold;
        ultHold = uHold;
        jumpHold = jHold;
    }




    public void SetMappings(int id, bool gamepad)
    {
        playerID = id + 1;

        inputPrefix = !gamepad ? "KB" : "P" + playerID.ToString();

        print("enabling " + inputPrefix);
        enabled = true;
    }

}
