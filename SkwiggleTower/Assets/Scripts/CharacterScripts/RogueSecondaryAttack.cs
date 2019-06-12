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
        //Attack is activated by right click
        if (Input.GetMouseButtonDown(1) && !isClicked)
        {
            isClicked = true;
        }
        if(isClicked && dashDist >= 0)
        {
            playerPos.position += new Vector3(dashSpeed, 0, 0);
            dashDist -= 1;
        }
        if(dashDist < 0)
        {
            dashDist = startDashDist;
            isClicked = false;
        }
    }
}
