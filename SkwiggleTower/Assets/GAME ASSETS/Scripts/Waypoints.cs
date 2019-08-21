using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoints : MonoBehaviour
{
    private void OnDrawGizmos()
    {
        for (int i = 0; i < transform.childCount-1; i++)
        {
            var startPos = transform.GetChild(i).position;
            var endPos = transform.GetChild(i + 1).position;

            Gizmos.DrawLine(startPos, endPos);
            Gizmos.DrawWireSphere(startPos + ((startPos - endPos)* -0.47f), 0.2f);
            Gizmos.DrawWireSphere(startPos + ((startPos - endPos) * -0.53f), 0.1f);
        }
    }
}
