// Ron Weeden
// Created: 6//28/19
// Modified: 6/30

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CharacterSelector : MonoBehaviour
{
    /// <summary>
    /// Which player is this? Player 1, player 2, etc...
    /// </summary>
    public int playerID;

    /// <summary>
    /// Which controller is this player using? 0 is keyboard; the rest are gamepads 1,2,3,4
    /// </summary>
    public int controller;

    /// <summary>
    /// The prefix that determines which mappings the device will use
    /// </summary>
    public string prefix;

    /// <summary>
    /// Does this character selector have an active player?
    /// </summary>
    new bool enabled;

    /// <summary>
    /// The index in the list of characters
    /// </summary>
    public int characterIndex;

    /// <summary>
    /// The portrait renderer for this selector
    /// </summary>
    public SpriteRenderer portraitRend;

    /// <summary>
    /// The text for this selector
    /// </summary>
    public TextMeshPro text;

    /// <summary>
    /// The character class that this selector represents; used for when the player locks in a choice
    /// </summary>
    public Class @class;

    /// <summary>
    /// Used to detect if the player is attempting to switch to the next character
    /// </summary>
    public bool @switch;

    /// <summary>
    /// Used to skip a single frame to prevent a character from immediately being selected upon enabling
    /// </summary>
    bool skipFrame;


    private void Start()
    {
        // render the portrait dark at the start (i.e. inactive)
        portraitRend.color = CharacterSelectionManager.instance.noPlayerColor;
    }

    private void Update()
    {
        if (enabled)
        {
            // check if the player is attempting to switch the character
            SwitchToNextCharacter();

            // checks if the player has made a selection
            if (Input.GetButtonDown(prefix + "Jump") && skipFrame)
            {
                portraitRend.color = Color.white;
            }


            // skip the 1st frame of the update
            if (!skipFrame)
                skipFrame = true;
        }
    }


    /// <summary>
    /// Set the mappings of the device based on which player they are
    /// </summary>
    /// <param name="pos">player number</param>
    /// <param name="controller">the int assignment of the controller</param>
    public void SetMappings(int pos,int controller)
    {
        playerID = pos + 1;

        this.controller = controller;

        prefix = controller == 0 ? "KB" : "P" + controller.ToString();

        print("enabling player " + playerID + " with controller " + controller);
        enabled = true;


        portraitRend.color = CharacterSelectionManager.instance.selectionColor;
    }



    public void SwitchToNextCharacter()
    {
        if (!@switch)
        {
            if (Input.GetAxis(prefix + "Movement") > 0.2f)
            {
                characterIndex = (characterIndex + 1) % 4;
                @switch = true;
            }

            else if (Input.GetAxis(prefix + "Movement") < -0.2f)
            {
                characterIndex = (characterIndex - 1) % 4;
                @switch = true;
            }
        }
        else
        {
            if (Mathf.Abs(Input.GetAxis(prefix + "Movement")) < 0.2f)
            {
                @switch = false;
            }
        }

        characterIndex = Mathf.Abs(characterIndex);

        portraitRend.sprite = CharacterSelectionManager.instance.portraits[characterIndex].sprite;
        text.text = CharacterSelectionManager.instance.portraits[characterIndex].@class.ToString();
    }



}
