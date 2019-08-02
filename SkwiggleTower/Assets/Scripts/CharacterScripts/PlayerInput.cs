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
    /// An identification that is given to each playable character determines which controller will control the player
    /// </summary>
    public int playerID;

    /// <summary>
    /// The prefix that determines which mappings to read from
    /// </summary>
    public string inputPrefix;

    /// <summary>
    /// used to check if a player controller is enabled
    /// </summary>
    public bool controllerEnabled;

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

    /// <summary>
    /// returns the sign of the horizontal axis
    /// </summary>
    int horizontalSign { get { return horizontal != 0 ?(int)Mathf.Sign(horizontal) : 0; } }


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



    private void Start()
    {
        faceRight = true;
    }

    void Update()
    {
        // do not progress if the controller is disabled
        if (!controllerEnabled) return;


        ProcessInputs();


        // relay the horizontal axis to the character's movement controller
        movement.horizontalAxis = horizontal;

        // if we're moving and the movement direction doesn't match what's recorded...
        if (horizontalSign != 0 && horizontalSign != faceDirection)
            // change the direction of the character to match the movement
            ChangeDirection(horizontalSign == 1);

        movement.faceDirection = faceDirection;
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



        // if the animator is valid
        if (animator)
        {
            SetAnimatorValues(character.primary, "Primary", pHold, pPressed, pReleased);
            SetAnimatorValues(character.secondary, "Secondary", sHold, sPressed, sReleased);
            SetAnimatorValues(character.ultimate, "Ult", uHold, uPressed, uReleased);
        }




        if (jPressed)
            movement.Jump();


        // record all of the temporary variables to compare on next update
        primaryHold = pHold;
        secondaryHold = sHold;
        ultHold = uHold;
        jumpHold = jHold;

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

        if (ability)
        {
            if (ability.activateOnHold)
            {
                if (hold)
                {
                    if (!ability.onCooldown && !animator.GetBool("AbilityActive"))
                    {
                        animator.SetTrigger(abilityType+"Trigger");
                    }
                }
                else if (release)
                {
                    if (animator.GetBool("AbilityActive"))
                        animator.SetTrigger(abilityType + "Release");
                }
            }
            else
            {
                if (press)
                {
                    if (!ability.onCooldown && !animator.GetBool("AbilityActive"))
                    {
                        animator.SetTrigger(abilityType + "Trigger");
                    }
                }
                else if (release)
                {
                    if (animator.GetBool("AbilityActive"))
                        animator.SetTrigger(abilityType + "Release");
                }
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
