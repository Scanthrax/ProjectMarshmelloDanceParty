using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerManager : MonoBehaviour
{
    #region Singleton

    public static PlayerManager instance;

    void Awake()
    {
        instance = this;
    }

    #endregion

    public GameObject player;
    public CharacterStats charStats;
    private int xpPM;
    public TextMeshProUGUI xpUI;
   // public GameObject winText;

    void Start()
    {
        charStats = player.GetComponent<CharacterStats>();        
    }

    void Update()
    {
        xpPM = charStats.xp;
        xpUI.SetText("XP: " + xpPM);
        LevelUp();
    }

    public void KillPlayer()
    {
        // SceneManager.LoadScene("");
    }

    public void LevelUp()
    {
        charStats.armor.AddModifier(2);
        charStats.damage.AddModifier(2);
    }
}
