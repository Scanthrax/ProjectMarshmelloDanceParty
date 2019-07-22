using UnityEngine;

/// <summary>
/// Valarie Script : UML created
/// </summary>
//We first ensure this script runs before all other player scripts to prevent laggy
//inputs
[DefaultExecutionOrder(-100)]
public class PlayerInput : MonoBehaviour
{
   // public bool testTouchControlsInEditor = false;  //Should touch controls be tested?
  // public float verticalDPadThreshold = .5f;       //Threshold touch pad inputs
  // public Thumbstick thumbstick;                   //Reference to Thumbstick
  //  public TouchButton jumpButton;                  //Reference to jump TouchButton

    [HideInInspector] public float horizontal;      //Float that stores horizontal input
    [HideInInspector] public bool jumpHeld;         //Bool that stores jump pressed
    [HideInInspector] public bool jumpPressed;      //Bool that stores jump held
    [HideInInspector] public bool crouchHeld;       //Bool that stores crouch held
    [HideInInspector] public bool crouchPressed;    //Bool that stores crouch pressed

    [HideInInspector] public bool primaryPressed;         //Bool that stores attack pressed
    [HideInInspector] public bool primaryHold;            //Bool that stores attack  held 
    [HideInInspector] public bool secondaryPressed;      //Bool that stores secondary attack  held
    [HideInInspector] public bool secondaryHold;            //Bool that stores attack  held 
    [HideInInspector] public bool ultPressed;      //Bool that stores secondary attack  held
    [HideInInspector] public bool ultHold;            //Bool that stores attack  held 

    bool dPadCrouchPrev;                            //Previous values of touch Thumbstick
    bool readyToClear;                              //Bool used to keep input in sync

    public int playerID;

    public string prefix;

    bool enabled;

    RogueAnimController RAC;

    private void Start()
    {
        enabled = false;
        RAC = GetComponent<RogueAnimController>();

    }

    void Update()
    {
        //Clear out existing input values
        ClearInput();

        //If the Game Manager says the game is over, exit
      //  if (GameManager.IsGameOver())
      //      return;

        //Process keyboard, mouse, gamepad (etc) inputs
        if(enabled)
            ProcessInputs();
        //Process mobile (touch) inputs
       // ProcessTouchInputs();

        //Clamp the horizontal input to be between -1 and 1
        horizontal = Mathf.Clamp(horizontal, -1f, 1f);
    }

    void FixedUpdate()
    {
        //In FixedUpdate() we set a flag that lets inputs to be cleared out during the 
        //next Update(). This ensures that all code gets to use the current inputs
        readyToClear = true;
    }

    void ClearInput()
    {
        //If we're not ready to clear input, exit
        if (!readyToClear)
            return;
        //Reset all inputs
        horizontal = 0f;
        jumpPressed = false;
        jumpHeld = false;
        crouchPressed = false;
        crouchHeld = false;


        readyToClear = false;
    }

    void ProcessInputs()
    {
        //Accumulate horizontal axis input
        horizontal += Input.GetAxis(prefix + "Movement");

        //Attack Inputs
        primaryHold = Input.GetAxis(prefix + "Primary") > 0.5f || Input.GetButton(prefix + "Primary");
        secondaryHold = Input.GetAxis(prefix + "Secondary") > 0.5f || Input.GetButton(prefix + "Secondary");
        ultHold = Input.GetAxis(prefix + "Ult") > 0.5f || Input.GetButton(prefix + "Ult");


        RAC.anim.SetBool("PrimaryHold", primaryHold);
        RAC.anim.SetBool("SecondaryHold", secondaryHold);
        RAC.anim.SetBool("UltHold", ultHold);

        //detect trigger
        if (!primaryPressed && primaryHold && !RAC.anim.GetBool("PrimaryActive") && !RAC.CS.primary.onCooldown)
        {
            RAC.anim.SetTrigger("PrimaryTrigger");
            RAC.anim.SetBool("PrimaryActive", true);
            primaryPressed = true;
        }
        else if(!primaryHold)
        {
            if(primaryPressed)
                primaryPressed = false;
        }

        //detect trigger
        if (!secondaryPressed && secondaryHold && !RAC.anim.GetBool("SecondaryActive") && !RAC.CS.secondary.onCooldown)
        {
            print("secondary cast");
            RAC.anim.SetTrigger("SecondaryTrigger");
            RAC.anim.SetBool("SecondaryActive", true);
            secondaryPressed = true;
        }
        else if (!secondaryHold)
        {
            if (secondaryPressed)
                secondaryPressed = false;
        }


        //detect trigger
        if (!ultPressed && ultHold && !RAC.anim.GetBool("UltActive") && !RAC.CS.ultimate.onCooldown)
        {
            print("ult cast");
            RAC.anim.SetTrigger("UltTrigger");
            RAC.anim.SetBool("UltActive", true);
            ultPressed = true;
        }
        else if (!ultHold)
        {
            if (ultPressed)
                ultPressed = false;
        }



        //Accumulate button inputs
        jumpPressed = jumpPressed || Input.GetButtonDown(prefix + "Jump") || Input.GetKeyDown(KeyCode.W);
        jumpHeld = jumpHeld || Input.GetButton(prefix + "Jump") || Input.GetKeyDown(KeyCode.W);

        //crouchPressed = crouchPressed || Input.GetButtonDown("Crouch");
        //crouchHeld = crouchHeld || Input.GetButton("Crouch");
    }

    //Not being used for Prototype
    //void ProcessTouchInputs()
    //{
    //    //If this isn't a mobile platform AND we aren't testing in editor, exit
    //    if (!Application.isMobilePlatform && !testTouchControlsInEditor)
    //        return;

    //    //Record inputs from screen thumbstick
    //    Vector2 thumbstickInput = thumbstick.GetDirection();

    //    //Accumulate horizontal input
    //    horizontal += thumbstickInput.x;

    //    //Accumulate jump button input
    //    jumpPressed = jumpPressed || jumpButton.GetButtonDown();
    //    jumpHeld = jumpHeld || jumpButton.GetButton();

    //    //Using thumbstick, accumulate crouch input
    //    bool dPadCrouch = thumbstickInput.y <= -verticalDPadThreshold;
    //    crouchPressed = crouchPressed || (dPadCrouch && !dPadCrouchPrev);
    //    crouchHeld = crouchHeld || dPadCrouch;

    //    //Record whether or not playing is crouching this frame (used for determining
    //    //if button is pressed for first time or held
    //    dPadCrouchPrev = dPadCrouch;
    //}


    public void SetMappings(int id, bool gamepad)
    {
        playerID = id + 1;

        prefix = !gamepad ? "KB" : "P" + playerID.ToString();

        print("enabling " + prefix);
        enabled = true;
    }

}
