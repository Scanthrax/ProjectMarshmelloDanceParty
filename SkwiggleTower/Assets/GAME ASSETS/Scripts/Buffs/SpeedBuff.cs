using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedBuff : BaseBuff
{
    public float percentIncrease;
    public float duration;
    float timer;

    float diff;

    public override void StartBuff()
    {
        base.StartBuff();
        StartCoroutine(Speed());
    }


    public IEnumerator Speed()
    {
        timer = 0f;
        print("TURN BLUE");
        character.characterRenderer.color = Color.blue;
        diff = character.movement.movementSpeed * (1 + percentIncrease * 0.01f) - character.movement.movementSpeed;
        character.movement.movementSpeed += diff;
        while (timer < duration)
        {
            timer += Time.deltaTime;
            yield return null;
        }

        EndBuff();

    }


    public override void EndBuff()
    {
        character.movement.movementSpeed -= diff;
        character.characterRenderer.color = Color.white;
        DestroyScriptInstance();
    }
}
