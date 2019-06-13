using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RogueSecondaryAttack : MonoBehaviour
{
    public float attackRangex;
    public float attackRangey;
    public float distanceFromPlayer;
    public float dashCooldown;
    public float dashDamage;
    public float dashDist;
    public float dashSpeed;
    public float ultPowerGenerated;
    public Transform playerPos;
    private float startDashDist;
    private bool isClicked;
    // Start is called before the first frame update
    void Start()
    {
        startDashDist = dashDist;
        isClicked = false;
    }

    // Update is called once per frame
    void Update()
    {
        //detecting platform layer 9
        int layerMask = 1 << 9;
        RaycastHit2D hit = Physics2D.Raycast(playerPos.position, Vector2.right, dashDist, layerMask);
        if (hit.collider == null)
        {
            //Attack is activated by right click
            if (Input.GetMouseButtonDown(1) && !isClicked)
            {
                isClicked = true;
            }
            if (isClicked && dashDist >= 0)
            {
                playerPos.position += new Vector3(dashSpeed, 0, 0);
                dashDist -= 1;
            }
            if (dashDist < 0)
            {
                dashDist = startDashDist;
                isClicked = false;
            }
        }
        else
        {
            Debug.Log("I see you");
            float distance = hit.point.x - playerPos.position.x;
            if (Input.GetMouseButtonDown(1) && !isClicked)
            {
                isClicked = true;
            }
            if (isClicked && distance >= 0)
            {
                playerPos.position += new Vector3(dashSpeed, 0, 0);
                distance -= 1;
            }
            if (dashDist < 0)
            {
                dashDist = startDashDist;
                isClicked = false;
            }
        }
    }
}
