using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static InputManager instance;

    private void Awake()
    {
        instance = this;
    }

    public int amtOfCurrentPlayers;

    public int amtOfPlayers;


    bool isKeyboardDetected;
    bool[] isGamepadDetected;


    public List<PlayerInput> playerInputs;
    
    public bool checkForPlayers;


    private void Start()
    {
        isKeyboardDetected = false;
        isGamepadDetected = new bool[4];
        amtOfCurrentPlayers = 0;
        amtOfPlayers = 0;


        foreach (var player in playerInputs)
        {
            foreach (Ability abilty in player.GetComponents<Ability>())
            {
                abilty.abilityDamageEvent += abilty.UltGain;
            }
        }

    }


    private void Update()
    {

        if (checkForPlayers)
        {
            if (amtOfCurrentPlayers <= 4)
            {
                #region Check for Keyboard player
                // if we haven't detected a keyboard
                if (!isKeyboardDetected)
                {
                    // check for an action button
                    if (Input.GetKeyDown(KeyCode.Space))
                    {
                        // we have now detected a keyboard
                        isKeyboardDetected = true;
                        // set the mappings of the player
                        playerInputs[amtOfCurrentPlayers].SetMappings(amtOfCurrentPlayers, false);
                        // we now have one more player
                        amtOfCurrentPlayers++;
                    }
                }
                #endregion
                #region Check for Gamepad Players
                // check for 4 gamepads
                for (int i = 0; i < isGamepadDetected.Length; i++)
                {
                    // if the gamepad is detected, continue through the loop
                    if (isGamepadDetected[i]) continue;

                    // check for the gamepad's action button
                    if (Input.GetKeyDown("joystick " + (i + 1) + " button 0"))
                    {
                        print("HERE I AM: " + (i + 1));
                        // set the mappings of the player
                        playerInputs[amtOfCurrentPlayers].SetMappings(i, true);
                        // we now have one more player
                        amtOfCurrentPlayers++;
                        isGamepadDetected[i] = true;
                    }

                }
                #endregion
            }
        }
    }
}
