using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressBar : MonoBehaviour
{
    public SpriteRenderer backgroundBar, foregroundBar;

    public Color foregourndColor, backgroundColor;

    Vector2 foregroundScale;

    private void Start()
    {
        foregroundScale = foregroundBar.transform.localScale;
    }



    public void SetProgress(float unitInterval)
    {
        foregroundBar.transform.localScale = foregroundScale * new Vector2(unitInterval,1);
    }



    private void OnValidate()
    {
        backgroundBar.color = backgroundColor;
        foregroundBar.color = foregourndColor;
    }
}
