using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitProjectile : BaseProjectile
{
    public SpriteRenderer spriteRenderer;
    public List<Sprite> fruits;



    public override void OnObjectSpawn()
    {
        base.OnObjectSpawn();

        ChangeSprite();

    }


    public void ChangeSprite()
    {
        spriteRenderer.sprite = fruits[Random.Range(0, fruits.Count)];
    }

}
