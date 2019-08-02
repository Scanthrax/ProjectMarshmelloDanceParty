using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHUD : MonoBehaviour
{
    public SpriteRenderer playerIcon;

    public ProgressBar hpBar, ultBar, primaryBar, secondaryBar, ultimateBar;

    public BaseCharacter character;

    bool primary;

    private void Start()
    {
        character.hud = this;
    }

    //private void Update()
    //{
        
    //    //secondaryBar.SetProgress(1 - character.secondary.percentage);
    //}


    public IEnumerator StartCooldownPrimary()
    {
        Debug.Log("primary hud");
        while(character.primary.onCooldown)
        {
            primaryBar.SetProgress(1 - character.primary.percentage);
            yield return null;
        }
    }

}
