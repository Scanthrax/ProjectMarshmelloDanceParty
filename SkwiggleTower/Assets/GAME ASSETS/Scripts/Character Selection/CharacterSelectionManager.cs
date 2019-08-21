// Ron Weeden
// Created: 6//28/19
// Modified: 6/30

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Enums for the different character classes
/// </summary>
public enum Class { Rogue, Warrior, Bard, Mage }

public class CharacterSelectionManager : MonoBehaviour
{
    /// <summary>
    /// Singleton instance
    /// </summary>
    public static CharacterSelectionManager instance;


    /// <summary>
    /// Contains information about the characters to be selected
    /// </summary>
    [System.Serializable]
    public struct Portraits
    {
        /// <summary>
        /// The sprite (portrait) to be displayed in the character select screen
        /// </summary>
        public Sprite sprite;
        /// <summary>
        /// The character class that this instance belongs to
        /// </summary>
        public Class @class;


        public Color color;
    }

    /// <summary>
    /// A list that contains the information of all portraits to be displayed in the character select screen
    /// </summary>
    public List<Portraits> portraits;

    /// <summary>
    /// Keeps track of how many players are currently detected
    /// </summary>
    public int amtOfCurrentPlayers;

    /// <summary>
    /// Keeps track of the detection of KB+M as well as 4 gamepads
    /// </summary>
    public bool[] isDeviceDetected;

    /// <summary>
    /// The panel that the player will navigate to select a player
    /// </summary>
    public List<CharacterSelector> characterSelectors;

    /// <summary>
    /// The sound source that plays a sound when a new player is detected
    /// </summary>
    public AudioSource newPlayerSource;

    /// <summary>
    /// The color that will modify the portrait sprite renderer
    /// </summary>
    public Color noPlayerColor, selectionColor;



    private void Awake()
    {
        // obtain the singleton
        instance = this;
    }

    private void Start()
    {
        // initialize array
        isDeviceDetected = new bool[5];

        for (int i = 0; i < characterSelectors.Count; i++)
        {
            foreach (var rend in characterSelectors[i].portraitRends)
            {
                // render the portrait dark at the start (i.e. inactive)
                rend.color = noPlayerColor;
                rend.sortingOrder = i;
                rend.sprite = portraits[i].sprite;
            }

            characterSelectors[i].mask.frontSortingOrder = i;
            characterSelectors[i].mask.backSortingOrder = i - 1;
            characterSelectors[i].characterIndex = i;
            characterSelectors[i].portrait = portraits[i];
        }



    }

    private void Update()
    {
        #region Check for players
        // only check if there are less than 4 players
        if (amtOfCurrentPlayers < 4)
        {
            #region Check input for the 5 devices
            for (int i = 0; i < isDeviceDetected.Length; i++)
            {
                // if the device is detected, continue through the loop
                if (isDeviceDetected[i]) continue;

                // save a string that will check which button to check for, depending on the device
                // if i is 0, we're checking the keyboard
                // otherwise, we're checking a gamepad
                var cont = i == 0 ?
                    "space" :
                    "joystick " + i + " button 0";

                // check for the gamepad's action button
                if (Input.GetKeyDown(cont))
                {

                    // go to the first available character selector & set its mappings
                    characterSelectors[amtOfCurrentPlayers].SetMappings(amtOfCurrentPlayers, i);
                    
                    // we have now detected this device
                    isDeviceDetected[i] = true;

                    // we now have one more player
                    amtOfCurrentPlayers++;

                    // play a sound now that we've added a player
                    newPlayerSource.Play();

                }

            }
            #endregion
        }
        #endregion


    }
}
