using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class UI_Manager : MonoBehaviour
{
    // Matt Thompson
    // Last Modified: 7/7/2019


    #region Variables
    
    // rect transforms for health and mana bars, for rescaling with percentage
    [Header ("Rect Transforms")]
    [SerializeField]
    RectTransform P1_HealthBarTransform;
    [SerializeField]
    RectTransform P2_HealthBarTransform;
    [SerializeField]
    RectTransform P3_HealthBarTransform;
    [SerializeField]
    RectTransform P4_HealthBarTransform;

    [SerializeField]
    RectTransform P1_ManaBarTransform;
    [SerializeField]
    RectTransform P2_ManaBarTransform;
    [SerializeField]
    RectTransform P3_ManaBarTransform;
    [SerializeField]
    RectTransform P4_ManaBarTransform;

    // Objects for players, used for determining which icon to show in which position, etc.
    [Header("Player Objects")]
    public GameObject Player1;
    public GameObject Player2;
    public GameObject Player3;
    public GameObject Player4;
    public int numPlayers;

    // Variables for player max, current and health ratio, used for Red health bar
    [Header("Player Health")]
    private float P1_healthPct;
    private float P2_healthPct;
    private float P3_healthPct;
    private float P4_healthPct;

    // variables keeping track of most recently recorded value for currentHealth, 
    // so as to only run update logic if health has changed rather than every tick
    private int P1_previousHealth;
    private int P2_previousHealth;
    private int P3_previousHealth;
    private int P4_previousHealth;

    // Variables for player max, current and mana ratio, used for Blue mana bar
    [Header("Player Mana")]
    private float P1_manaPct;
    private float P2_manaPct;
    private float P3_manaPct;
    private float P4_manaPct;

    // variables keeping track of most recently recorded value for currentMana, 
    // so as to only run update logic if mana has changed rather than every tick
    private int P1_previousMana;
    private int P2_previousMana;
    private int P3_previousMana;
    private int P4_previousMana;

    [Header("Bars, bar sizes, etc.")]

    // "foreground" bars for player health (red bars)
    public GameObject P1_healthBarFG;
    public GameObject P2_healthBarFG;
    public GameObject P3_healthBarFG;
    public GameObject P4_healthBarFG;

    // "foreground" bars for player mana (blue bars)
    public GameObject P1_manaBarFG;
    public GameObject P2_manaBarFG;
    public GameObject P3_manaBarFG;
    public GameObject P4_manaBarFG;

    // temporary vector used for adjusting bars
    private Vector3 tempVector;

    // icons for player face, primary and secondary attacks
    [Header("Player Icons")]
    public GameObject P1faceRenderer;
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

    public static UI_Manager instance;

    public List<PlayerHUD> HUDs;


    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        //// player object can be assigned in inspector but otherwise would need to be assigned in some sort of script after players choose characters

        //// num players cant be less than  or more than 4
        //if (numPlayers < 1) numPlayers = 1;
        //if (numPlayers > 4) numPlayers = 4;

        //if (numPlayers >= 1) // should never be <1 anyways?
        //{
        //    // Gets values for health Percentage from player scripts
        //    P1_healthPct = (Player1.GetComponent<PlayerStats>().currentHealth) / (Player1.GetComponent<PlayerStats>().maxHealth);
        //    // sets "previous health" values to current health (which should be max at start), most recently recored value of current health
        //    P1_previousHealth = (Player1.GetComponent<PlayerStats>().currentHealth);
        //    //gets variables for mana percentages
        //    // P1_manaPct = (Player1.GetComponent<PlayerStats>().currentMana) / (Player1.GetComponent<PlayerStats>().maxMana);
        //    //gets variables for most recent mana value
        //    // P1_previousMana = (Player1.GetComponent<PlayerStats>().currentMana);
        //}
        //if (numPlayers >= 2)
        //{
        //    P2_healthPct = (Player2.GetComponent<PlayerStats>().currentHealth) / (Player2.GetComponent<PlayerStats>().maxHealth);
        //    P2_previousHealth = (Player2.GetComponent<PlayerStats>().currentHealth);
        //    // P2_manaPct = (Player3.GetComponent<PlayerStats>().currentMana) / (Player3.GetComponent<PlayerStats>().maxMana);
        //    //P2_previousMana = (Player2.GetComponent<PlayerStats>().currentMana);
        //}
        //if (numPlayers >= 3)
        //{
        //    P3_healthPct = (Player3.GetComponent<PlayerStats>().currentHealth) / (Player3.GetComponent<PlayerStats>().maxHealth);
        //    P3_previousHealth = (Player3.GetComponent<PlayerStats>().currentHealth);
        //    // P3_manaPct = (Player3.GetComponent<PlayerStats>().currentMana) / (Player3.GetComponent<PlayerStats>().maxMana);
        //    // P3_previousMana = (Player2.GetComponent<PlayerStats>().currentMana);
        //}
        //if (numPlayers >= 4)
        //{
        //    P4_healthPct = (Player4.GetComponent<PlayerStats>().currentHealth) / (Player4.GetComponent<PlayerStats>().maxHealth);
        //    P4_previousHealth = (Player4.GetComponent<PlayerStats>().currentHealth);
        //    // P4_manaPct = (Player3.GetComponent<PlayerStats>().currentMana) / (Player3.GetComponent<PlayerStats>().maxMana);
        //    // P4_previousMana = (Player2.GetComponent<PlayerStats>().currentMana);
        //}

        //// need to assign icons depending on which character each player has chosen
        ///* if (player1 is rogue)
        // * {
        // *      P1_face = rogue face
        // *      P1_primaryicon = rogue primary icon
        // *      P2_secondaryicon = rogue secondary icon
        // * } etc.
        //*/



        

    }

    // Update is called once per frame
    void Update()
    {
        //var p1 = RoomManager.instance.playerInputs[0];

        //var health = p1.GetComponent<CharacterStats>().percentHealth;
        //P1_healthBarFG.transform.localScale = new Vector3(health, 1, 1);

        //var ult = p1.GetComponent<PlayerStats>().percentUlt;
        //P1_manaBarFG.transform.localScale = new Vector3(ult, 1, 1);

        //var primary = p1.GetComponent<PlayerStats>().primary.percentage;
        //P1_primaryIcon.transform.localScale = new Vector3(32f - (primary * 32f), 32, 1);

        //var secondary = p1.GetComponent<PlayerStats>().secondary.percentage;
        //P1_secondaryIcon.transform.localScale = new Vector3(32f - (secondary * 32f), 32, 1);

        //// testing code, deals one damage to player
        //if (Input.GetKeyDown(KeyCode.M))
        //{
        //    // player takes damage equal to their armor value +1, essentially taking 1 damage
        //    Player1.GetComponent<CharacterStats>().TakeDamage(Player1.GetComponent<CharacterStats>().armor.GetValue() + 1);
        //}

        //// logic for updating scale of bars, uses a temporary vector3 to store and modify scale before altering scale of bar
        //// only updates if player health or mana has changed since last check, so as to not be constantly running all calculations
        //#region Health and Mana bar checks and updates
        //if (numPlayers >= 1)
        //{
        //    if (P1_previousHealth != Player1.GetComponent<PlayerStats>().currentHealth)
        //    {
        //        Debug.Log("Health should be updating");
        //        // calculated based on current/max health
        //        P1_healthPct = (float) (Player1.GetComponent<PlayerStats>().currentHealth) / (Player1.GetComponent<PlayerStats>().maxHealth);
        //        // adjusts size of health rectangle with health percentage as variable
        //        SetBarAmount(P1_HealthBarTransform, P1_healthPct);
        //        // updates most recently recored health value
        //        P1_previousHealth = Player1.GetComponent<PlayerStats>().currentHealth;
        //    }
        //    /*
        //    if (P1_previousMana != Player1.GetComponent<PlayerStats>().currentMana)
        //    {
        //        // calculated based on current/max mana
        //        P1_manaPct = (Player1.GetComponent<PlayerStats>().currentMana) / (Player1.GetComponent<PlayerStats>().maxMana);
        //        // adjusts size of mana rectangle with health percentage as variable
        //        SetBarAmount(P1_ManaBarTransform, P1_manaPct);
        //        // updates most recently recored mana value
        //        P1_previousMana = Player1.GetComponent<PlayerStats>().currentMana;
        //    } */
        //}
        //if (numPlayers >= 2)
        //{
        //    if (P2_previousHealth != Player2.GetComponent<PlayerStats>().currentHealth)
        //    {
        //        // calculated based on current/max health
        //        P2_healthPct = (Player2.GetComponent<PlayerStats>().currentHealth) / (Player2.GetComponent<PlayerStats>().maxHealth);
        //        // adjusts size of health rectangle with health percentage as variable
        //        SetBarAmount(P2_HealthBarTransform, P2_healthPct);
        //        // updates most recently recored health value
        //        P2_previousHealth = Player2.GetComponent<PlayerStats>().currentHealth;
        //    }
        //    /*
        //    if (P2_previousMana != Player1.GetComponent<PlayerStats>().currentMana)
        //    {
        //        // calculated based on current/max mana
        //        P2_manaPct = (Player2.GetComponent<PlayerStats>().currentMana) / (Player2.GetComponent<PlayerStats>().maxMana);
        //        // adjusts size of mana rectangle with health percentage as variable
        //        SetBarAmount(P2_ManaBarTransform, P2_manaPct);
        //        // updates most recently recored mana value
        //        P2_previousMana = Player2.GetComponent<PlayerStats>().currentMana;
        //    } */
        //}
        //if (numPlayers >= 3)
        //{
        //    if (P3_previousHealth != Player3.GetComponent<PlayerStats>().currentHealth)
        //    {
        //        // calculated based on current/max health
        //        P3_healthPct = (Player3.GetComponent<PlayerStats>().currentHealth) / (Player3.GetComponent<PlayerStats>().maxHealth);
        //        // adjusts size of health rectangle with health percentage as variable
        //        SetBarAmount(P3_HealthBarTransform, P3_healthPct);
        //        // updates most recently recored health value
        //        P3_previousHealth = Player1.GetComponent<PlayerStats>().currentHealth;
        //    }
        //    /*
        //    if (P3_previousMana != Player3.GetComponent<PlayerStats>().currentMana)
        //    {
        //        // calculated based on current/max mana
        //        P3_manaPct = (Player3.GetComponent<PlayerStats>().currentMana) / (Player3.GetComponent<PlayerStats>().maxMana);
        //        // adjusts size of mana rectangle with health percentage as variable
        //        SetBarAmount(P3_ManaBarTransform, P3_manaPct);
        //        // updates most recently recored mana value
        //        P3_previousMana = Player3.GetComponent<PlayerStats>().currentMana;
        //    } */
        //}
        //if (numPlayers >= 4)
        //{
        //    if (P4_previousHealth != Player4.GetComponent<PlayerStats>().currentHealth)
        //    {
        //        // calculated based on current/max health
        //        P4_healthPct = (Player4.GetComponent<PlayerStats>().currentHealth) / (Player4.GetComponent<PlayerStats>().maxHealth);
        //        // adjusts size of health rectangle with health percentage as variable
        //        SetBarAmount(P4_HealthBarTransform, P4_healthPct);
        //        // updates most recently recored health value
        //        P4_previousHealth = Player1.GetComponent<PlayerStats>().currentHealth;
        //    }
        //    /*
        //        if (P4_previousMana != Player4.GetComponent<PlayerStats>().currentMana)
        //        {
        //        // calculated based on current/max mana
        //        P4_manaPct = (Player4.GetComponent<PlayerStats>().currentMana) / (Player4.GetComponent<PlayerStats>().maxMana);
        //        // adjusts size of mana rectangle with health percentage as variable
        //        SetBarAmount(P4_ManaBarTransform, P4_manaPct);
        //        // updates most recently recored mana value
        //        P4_previousMana = Player4.GetComponent<PlayerStats>().currentMana;
        //        }*/
        //}

        //#endregion






    }

    // Method that takes in a rect transform and a percentage amount to rescale health/mana bar accordingly
    void SetBarAmount (RectTransform rect, float _amount)
    {
        tempVector = rect.localScale;
        tempVector.x = tempVector.x * _amount;
        rect.localScale = tempVector;
    }
}