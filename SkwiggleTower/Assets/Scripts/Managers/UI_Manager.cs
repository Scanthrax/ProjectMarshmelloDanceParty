using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Manager : MonoBehaviour
{
    // Matt Thompson
    // Last Modified: 6/30/2019

    #region Variables
    // Objects for players, used for determining which icon to show in which position, etc.
    [Header("Player Objects")]
    public GameObject Player1;
    public GameObject Player2;
    public GameObject Player3;
    public GameObject Player4;

    public int numPlayers;

    // Variables for player max, current and health ratio, used for Red health bar (current health not needed?)
    [Header("Player Health")]
    public float P1_healthPct;
    public float P2_healthPct;
    public float P3_healthPct;
    public float P4_healthPct;

    public int P1_maxHealth;
    public int P2_maxHealth;
    public int P3_maxHealth;
    public int P4_maxHealth;

    // variables keeping track of most recently recorded value for currentHealth, 
    // so as to only run update logic if health has changed rather than every tick
    private int P1_previousHealth;
    private int P2_previousHealth;
    private int P3_previousHealth;
    private int P4_previousHealth;

    // Variables for player max, current and mana ratio, used for Blue mana bar
    [Header("Player Mana")]
    public float P1_manaPct;
    public float P2_manaPct;
    public float P3_manaPct;
    public float P4_manaPct;

    public float P1_maxMana;
    public float P2_maxMana;
    public float P3_maxMana;
    public float P4_maxMana;

    // variables keeping track of most recently recorded value for currentMana, 
    // so as to only run update logic if mana has changed rather than every tick
    private int P1_previousMana;
    private int P2_previousMana;
    private int P3_previousMana;
    private int P4_previousMana;

    [Header("Bars, bar sizes, etc.")]
    // background bars for player health
    public GameObject P1_healthBarBG;
    public GameObject P2_healthBarBG;
    public GameObject P3_healthBarBG;
    public GameObject P4_healthBarBG;

    // "foreground" bars for player health (red bars)
    public GameObject P1_healthBarFG;
    public GameObject P2_healthBarFG;
    public GameObject P3_healthBarFG;
    public GameObject P4_healthBarFG;

    // background bars for player mana
    public GameObject P1_manaBarBG;
    public GameObject P2_manaBarBG;
    public GameObject P3_manaBarBG;
    public GameObject P4_manaBarBG;

    // "foreground" bars for player mana (blue bars)
    public GameObject P1_manaBarFG;
    public GameObject P2_manaBarFG;
    public GameObject P3_manaBarFG;
    public GameObject P4_manaBarFG;

    // max size of player heath and mana bars, used for determining bar size to preserve scaling
    public Vector3 P1_barMaxSize;
    public Vector3 P2_barMaxSize;
    public Vector3 P3_barMaxSize;
    public Vector3 P4_barMaxSize;
    private Vector3 tempVector;

    // icons for player face, primary and secondary attacks
    [Header("Player Icons")]
    public GameObject P1_faceIcon;
    public GameObject P1_primaryIcon;
    public GameObject P1_secondaryIcon;

    public GameObject P2_faceIcon;
    public GameObject P2_primaryIcon;
    public GameObject P2_secondaryIcon;

    public GameObject P3_faceIcon;
    public GameObject P3_primaryIcon;
    public GameObject P3_secondaryIcon;

    public GameObject P4_faceIcon;
    public GameObject P4_primaryIcon;
    public GameObject P4_secondaryIcon;

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        // player object can be assigned in inspector but otherwise would need to be assigned in some sort of script after players choose characters

        // num players cant be less than  or more than 4
        if (numPlayers < 1) numPlayers = 1;
        if (numPlayers > 4) numPlayers = 4;

        if (numPlayers >= 1) // should never be <1 anyways?
        {
            // Gets values for health Percentage from player scripts
            P1_healthPct = (Player1.GetComponent<PlayerStats>().currentHealth) / (Player1.GetComponent<PlayerStats>().maxHealth);
            // sets "previous health" values to current health (which should be max at start), most recently recored value of current health
            P1_previousHealth = (Player1.GetComponent<PlayerStats>().currentHealth);
            //gets variables for mana percentages
            // P1_manaPct = (Player1.GetComponent<PlayerStats>().currentMana) / (Player1.GetComponent<PlayerStats>().maxMana);
            //gets variables for most recent mana value
            // P1_previousMana = (Player1.GetComponent<PlayerStats>().currentMana);
        }
        if (numPlayers >= 2)
        {
            P2_healthPct = (Player2.GetComponent<PlayerStats>().currentHealth) / (Player2.GetComponent<PlayerStats>().maxHealth);
            P2_previousHealth = (Player2.GetComponent<PlayerStats>().currentHealth);
            // P2_manaPct = (Player3.GetComponent<PlayerStats>().currentMana) / (Player3.GetComponent<PlayerStats>().maxMana);
            //P2_previousMana = (Player2.GetComponent<PlayerStats>().currentMana);
        }
        if (numPlayers >= 3)
        {
            P3_healthPct = (Player3.GetComponent<PlayerStats>().currentHealth) / (Player3.GetComponent<PlayerStats>().maxHealth);
            P3_previousHealth = (Player3.GetComponent<PlayerStats>().currentHealth);
            // P3_manaPct = (Player3.GetComponent<PlayerStats>().currentMana) / (Player3.GetComponent<PlayerStats>().maxMana);
            // P3_previousMana = (Player2.GetComponent<PlayerStats>().currentMana);
        }
        if (numPlayers >= 4)
        {
            P4_healthPct = (Player4.GetComponent<PlayerStats>().currentHealth) / (Player4.GetComponent<PlayerStats>().maxHealth);
            P4_previousHealth = (Player4.GetComponent<PlayerStats>().currentHealth);
            // P4_manaPct = (Player3.GetComponent<PlayerStats>().currentMana) / (Player3.GetComponent<PlayerStats>().maxMana);
            // P4_previousMana = (Player2.GetComponent<PlayerStats>().currentMana);
        }

        // need to assign icons depending on which character each player has chosen
        /* if (player1 is rogue)
         * {
         *      P1_face = rogue face
         *      P1_primaryicon = rogue primary icon
         *      P2_secondaryicon = rogue secondary icon
         * } etc.
        */
    }

    // Update is called once per frame
    void LateUpdate()
    {
        // logic for updating scale of bars, uses a temporary vector3 to store and modify scale before altering scale of bar
        // only updates if player health or mana has changed since last check, so as to not be constantly running all calculations
        #region Health and Mana bar checks and updates
        if (numPlayers >= 1)
        {
            if (P1_previousHealth != Player1.GetComponent<PlayerStats>().currentHealth)
            {
                // calculated based on current/max health
                P1_healthPct = (Player1.GetComponent<PlayerStats>().currentHealth) / (Player1.GetComponent<PlayerStats>().maxHealth);
                // uses temp Vector3 to update x value of bar scale (width?)
                tempVector = P1_healthBarFG.transform.localScale;
                tempVector.x = P1_healthPct * P1_barMaxSize.x;
                P1_healthBarFG.transform.localScale = tempVector;
                // updates most recently recored health value
                P1_previousHealth = Player1.GetComponent<PlayerStats>().currentHealth;
            }
            /*
            if (P1_previousMana != Player1.GetComponent<PlayerStats>().currentMana)
            {
                // calculated based on current/max mana
                P1_manaPct = (Player1.GetComponent<PlayerStats>().currentMana) / (Player1.GetComponent<PlayerStats>().maxMana);
                // uses temp Vector3 to update x value of bar scale (width?)
                tempVector = P1_manaBarFG.transform.localScale;
                tempVector.x = P1_manaPct * P1_barMaxSize.x;
                P1_manaBarFG.transform.localScale = tempVector;
                // updates most recently recored mana value
                P1_previousMana = Player1.GetComponent<PlayerStats>().currentMana;
            } */
        }
        if (numPlayers >= 2)
        {
            if (P2_previousHealth != Player2.GetComponent<PlayerStats>().currentHealth)
            {
                // calculated based on current/max health
                P2_healthPct = (Player2.GetComponent<PlayerStats>().currentHealth) / (Player2.GetComponent<PlayerStats>().maxHealth);
                // uses temp Vector3 to update x value of bar scale
                tempVector = P2_healthBarFG.transform.localScale;
                tempVector.x = P2_healthPct * P2_barMaxSize.x;
                P2_healthBarFG.transform.localScale = tempVector;
                // updates most recently recored health value
                P2_previousHealth = Player2.GetComponent<PlayerStats>().currentHealth;
            }
            /*
            if (P2_previousMana != Player1.GetComponent<PlayerStats>().currentMana)
            {
                // calculated based on current/max mana
                P2_manaPct = (Player2.GetComponent<PlayerStats>().currentMana) / (Player2.GetComponent<PlayerStats>().maxMana);
                // uses temp Vector3 to update x value of bar scale (width?)
                tempVector = P2_manaBarFG.transform.localScale;
                tempVector.x = P2_manaPct * P2_barMaxSize.x;
                P2_manaBarFG.transform.localScale = tempVector;
                // updates most recently recored mana value
                P2_previousMana = Player2.GetComponent<PlayerStats>().currentMana;
            } */
        }
        if (numPlayers >= 3)
        {
            if (P3_previousHealth != Player3.GetComponent<PlayerStats>().currentHealth)
            {
                // calculated based on current/max health
                P3_healthPct = (Player3.GetComponent<PlayerStats>().currentHealth) / (Player3.GetComponent<PlayerStats>().maxHealth);
                // uses temp Vector3 to update x value of bar scale
                tempVector = P3_healthBarFG.transform.localScale;
                tempVector.x = P3_healthPct * P3_barMaxSize.x;
                P3_healthBarFG.transform.localScale = tempVector;
                // updates most recently recored health value
                P3_previousHealth = Player1.GetComponent<PlayerStats>().currentHealth;
            }
            /*
            if (P3_previousMana != Player3.GetComponent<PlayerStats>().currentMana)
            {
                // calculated based on current/max mana
                P3_manaPct = (Player3.GetComponent<PlayerStats>().currentMana) / (Player3.GetComponent<PlayerStats>().maxMana);
                // uses temp Vector3 to update x value of bar scale (width?)
                tempVector = P3_manaBarFG.transform.localScale;
                tempVector.x = P3_manaPct * P3_barMaxSize.x;
                P3_manaBarFG.transform.localScale = tempVector;
                // updates most recently recored mana value
                P3_previousMana = Player3.GetComponent<PlayerStats>().currentMana;
            } */
        }
        if (numPlayers >= 4)
        {
            if (P4_previousHealth != Player4.GetComponent<PlayerStats>().currentHealth)
            {
                // calculated based on current/max health
                P4_healthPct = (Player4.GetComponent<PlayerStats>().currentHealth) / (Player4.GetComponent<PlayerStats>().maxHealth);
                // uses temp Vector3 to update x value of bar scale
                tempVector = P4_healthBarFG.transform.localScale;
                tempVector.x = P4_healthPct * P4_barMaxSize.x;
                P4_healthBarFG.transform.localScale = tempVector;
                // updates most recently recored health value
                P4_previousHealth = Player1.GetComponent<PlayerStats>().currentHealth;
            }
            /*
                if (P4_previousMana != Player4.GetComponent<PlayerStats>().currentMana)
                {
                // calculated based on current/max mana
                P4_manaPct = (Player4.GetComponent<PlayerStats>().currentMana) / (Player4.GetComponent<PlayerStats>().maxMana);
                // uses temp Vector3 to update x value of bar scale (width?)
                tempVector = P4_manaBarFG.transform.localScale;
                tempVector.x = P4_manaPct * P4_barMaxSize.x;
                P4_manaBarFG.transform.localScale = tempVector;
                // updates most recently recored mana value
                P4_previousMana = Player4.GetComponent<PlayerStats>().currentMana;
                }*/
        }

        #endregion
    }
}