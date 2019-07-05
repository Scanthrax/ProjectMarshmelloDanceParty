// Ron Weeden
// Created: 6//28/19
// Modified: 6/30

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Experimental.Rendering.LWRP;

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
    public List<SpriteRenderer> portraitRends;

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

    public SpriteMask mask;

    public Animator slidePortrait;

    public CharacterSelectionManager.Portraits portrait;


    public Light2D light;


    private void Start()
    {
        light.color = portrait.color;
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
                foreach (var rend in portraitRends)
                    rend.color = Color.white;
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

        foreach (var rend in portraitRends)
        {
            rend.color = CharacterSelectionManager.instance.selectionColor;
        }

        text.text = portrait.@class.ToString();
    }



    public void SwitchToNextCharacter()
    {
        if (!@switch)
        {
            if (Input.GetAxis(prefix + "Movement") > 0.2f)
            {
                characterIndex++;
                if (characterIndex > 3)
                    characterIndex = 0;


                slidePortrait.Play("SlidePortraitRight");

                @switch = true;
                characterIndex = Mathf.Abs(characterIndex);
                portrait = CharacterSelectionManager.instance.portraits[characterIndex];
                text.text = portrait.@class.ToString();

                portraitRends[0].sprite = portrait.sprite;


                light.color = portrait.color;
            }

            else if (Input.GetAxis(prefix + "Movement") < -0.2f)
            {
                characterIndex--;
                if (characterIndex < 0)
                    characterIndex = 3;

                slidePortrait.Play("SlidePortraitLeft");


                @switch = true;
                characterIndex = Mathf.Abs(characterIndex);
                portrait = CharacterSelectionManager.instance.portraits[characterIndex];
                text.text = portrait.@class.ToString();

                portraitRends[1].sprite = portrait.sprite;


                light.color = portrait.color;
            }
        }

    }


    public void SetPortraits(int i)
    {

        @switch = false;
        portraitRends[i].sprite = portrait.sprite;
    }



}
