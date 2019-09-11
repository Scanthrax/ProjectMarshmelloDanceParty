using System.Collections;
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
    bool stallInvincibility;

    public List<BaseCharacter> listOfAggro;


    public IEnumerator FlashCharacter()
    {
        invincible = true;

        var flashSpeed = flashSpeedMin + (flashSpeedMax - flashSpeedMin) * (1 - percentHealth);



        float timer = 0f;
        while(timer <= flashTime)
        {
            var diff = maxAplha - minAlpha;
            var topDiff = 1 - maxAplha;

            characterRenderer.color = new Color(characterRenderer.color.r, characterRenderer.color.g, characterRenderer.color.b,
                (Mathf.Sin(Time.time * flashSpeed)
                * 0.5f * diff)
                + (1 - diff * 0.5f) - topDiff);


            if (!stallInvincibility)
                timer += Time.deltaTime;

            yield return null;
        }
        characterRenderer.color = new Color(1,1,1,1);
        invincible = false;
    }

    public override bool RecieveDamage(int damage, bool hitSource)
    {
        if (invincible) return false;
        var dies = base.RecieveDamage(damage, hitSource);
        hud?.hpBar.SetProgress(percentHealth);
        StartCoroutine(FlashCharacter());

        return dies;
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


    public void StallInvincibility(bool b)
    {
        stallInvincibility = b;
    }
}
