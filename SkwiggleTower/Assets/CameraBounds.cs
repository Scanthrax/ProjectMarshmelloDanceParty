using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBounds : MonoBehaviour
{

    private void OnDrawGizmos()
    {
        
        foreach (Transform item in transform)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(item.position, item.GetComponent<RectTransform>().rect.size);
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireCube(item.position, item.GetComponent<RectTransform>().rect.size - new Vector2(Camera.main.orthographicSize * (16f/9f),Camera.main.orthographicSize) * 2f);
        }
        
    }
}
