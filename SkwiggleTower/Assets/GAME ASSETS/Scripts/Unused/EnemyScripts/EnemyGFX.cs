using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

/// <summary>
/// 
///Valarie Script, creates a the flipping of the character while pathfinding
/// </summary>
public class EnemyGFX : MonoBehaviour
{
    public AIPath aipath;

    private void Update()
    {
        if (aipath.desiredVelocity.x  >= 0.01f)
        {
            transform.localScale = new Vector3(-1, 1f, 1f); //travels to the right
        }
        else if (aipath.desiredVelocity.x <= -0.01f)
        {
            transform.localScale = new Vector3(1, 1f, 1f); //flips character to face player on left
        }
    }
}
