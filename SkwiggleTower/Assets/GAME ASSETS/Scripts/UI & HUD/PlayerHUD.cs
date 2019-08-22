using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerHUD : MonoBehaviour
{
    public SpriteRenderer playerIcon;

    public ProgressBar hpBar, ultBar, primaryBar, secondaryBar, ultimateBar;

    public PlayableCharacter character;

    public TextMeshPro primarySeconds, secondarySeconds, ultimateSeconds;

    private void Start()
    {
        if (character)
        {
            character.hud = this;

            #region Set ability delegates that call these Cooldown functions
            character.primary.cooldownHUD = StartCooldownPrimary;
            character.secondary.cooldownHUD = StartCooldownSecondary;
            character.ultimate.cooldownHUD = StartCooldownUltimate;
            #endregion
        }

        #region Hide the timers on each ability icon
        primarySeconds.gameObject.SetActive(false);
        secondarySeconds.gameObject.SetActive(false);
        ultimateSeconds.gameObject.SetActive(false);
        #endregion

        #region Set each icon progress to 0
        primaryBar.SetProgress(0f);
        secondaryBar.SetProgress(0f);
        ultimateBar.SetProgress(0f);
        #endregion

        #region Set HP and Ult charge progress
        hpBar.SetProgress(1f);
        ultBar.SetProgress(0f);
        #endregion
    }

    #region Start Cooldown Coroutines
    public void StartCooldownPrimary()
    {
        StartCoroutine(StartCooldown(character.primary, primaryBar, primarySeconds));
    }
    public void StartCooldownSecondary()
    {
        StartCoroutine(StartCooldown(character.secondary, secondaryBar, secondarySeconds));
    }
    public void StartCooldownUltimate()
    {
        StartCoroutine(StartCooldown(character.ultimate, ultimateBar, ultimateSeconds));
    }
    #endregion

    #region Cooldown Coroutine
    public IEnumerator StartCooldown(Ability ability, ProgressBar bar, TextMeshPro seconds)
    {
        // display the timer on the icon
        seconds.gameObject.SetActive(true);
        // start out with no decimal places
        var decimalPlaces = "f0";

        while(ability.onCooldown)
        {
            // start showing a decimal place when the ability is below 0.6 seconds
            if (decimalPlaces == "f0" && ability.remainingTime < 0.6f)
                decimalPlaces = "f1";

            // set the progress of the par based on the ability's cooldown timer
            bar.SetProgress(1 - ability.percentage);

            // display the remaining time
            seconds.text = ability.remainingTime.ToString(decimalPlaces);

            yield return null;
        }

        // set progress to 0 upon completion
        bar.SetProgress(0);

        // get rid of the timer on the icon
        seconds.gameObject.SetActive(false);
    }
    #endregion

}
