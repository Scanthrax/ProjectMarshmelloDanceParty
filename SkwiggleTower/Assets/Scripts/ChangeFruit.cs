using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeFruit : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;

    public List<Sprite> fruits;

    private void Start()
    {
        spriteRenderer.sprite = fruits[Random.Range(0, fruits.Count)];
    }
}
