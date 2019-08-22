﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayableCharacter : BaseCharacter
{
    public PlayerHUD hud;


    #region Invincibility flashing
    [Header("Invincibility flashing")]
    [Range(5f, 30f)]
    public float flashSpeedMin;
    [Range(5f, 30f)]
    public float flashSpeedMax;
    [Range(0f, 1f)]
    public float minAlpha, maxAplha;

    public float flashTime;
    #endregion


    public bool invincible;



    public IEnumerator FlashCharacter()
    {
        invincible = true;

        var flashSpeed = flashSpeedMin + (flashSpeedMax - flashSpeedMin) * (1 - percentHealth);

        for (float i = 0; i < flashTime; i += Time.deltaTime)
        {
            var diff = maxAplha - minAlpha;
            var topDiff = 1 - maxAplha;

            characterRenderer.color = new Color(characterRenderer.color.r, characterRenderer.color.g, characterRenderer.color.b,
                (Mathf.Sin(Time.time * flashSpeed)
                * 0.5f * diff)
                + (1 - diff * 0.5f) - topDiff);

            yield return null;
        }
        characterRenderer.color = new Color(1,1,1,1);
        invincible = false;
    }

    public override void RecieveDamage(int damage, bool hitSource)
    {
        if (invincible) return;
        base.RecieveDamage(damage, hitSource);
        hud?.hpBar.SetProgress(percentHealth);
        StartCoroutine(FlashCharacter());
    }


    public virtual void RecieveUltCharge(int ult)
    {
        currentUltCharge = Mathf.Min(currentUltCharge += ult, maxUltCharge);
        hud?.ultBar.SetProgress(percentUltCharge);
    }


    public void ResetUltCharge()
    {
        currentUltCharge = 0;
        hud?.ultBar.SetProgress(percentUltCharge);
    }

}
