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

            character.primary.cooldownHUD = StartCooldownPrimary;
            character.secondary.cooldownHUD = StartCooldownSecondary;
            //character.ultimate.cooldownHUD = StartCooldownUltimate;
        }

        primarySeconds.gameObject.SetActive(false);
        secondarySeconds.gameObject.SetActive(false);
        ultimateSeconds.gameObject.SetActive(false);

        primaryBar.SetProgress(0f);
        secondaryBar.SetProgress(0f);
        ultimateBar.SetProgress(0f);

        hpBar.SetProgress(1f);
        ultBar.SetProgress(0f);
    }


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


    public IEnumerator StartCooldown(Ability ability, ProgressBar bar, TextMeshPro seconds)
    {
        seconds.gameObject.SetActive(true);
        var decimalPlaces = "f0";

        while(ability.onCooldown)
        {
            if (ability.remainingTime < 0.6f)
                decimalPlaces = "f1";

            bar.SetProgress(1 - ability.percentage);
            seconds.text = ability.remainingTime.ToString(decimalPlaces);
            yield return null;
        }

        bar.SetProgress(0);
        seconds.gameObject.SetActive(false);
    }

}
