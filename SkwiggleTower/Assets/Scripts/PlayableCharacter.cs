using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayableCharacter : BaseCharacter
{
    public PlayerHUD hud;


    #region Invincibility flashing
    [Header("Invincibility flashing")]
    [Range(5f, 20f)]
    public float flashSpeed;
    [Range(0f, 1f)]
    public float minAlpha, maxAplha;

    public float flashTime;
    #endregion


    public IEnumerator FlashCharacter()
    {
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
    }


}
