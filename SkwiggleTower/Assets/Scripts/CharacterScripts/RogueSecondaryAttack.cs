using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RogueSecondaryAttack : MonoBehaviour
{
    //Not all of these variables are used yet. Currently working on getting the dash to work correctly.
    public float attackRangex;
    public float attackRangey;
    public float distanceFromPlayer;
    public float dashCooldown;
    public float dashDamage;
    public float dashDist;
    public float dashSpeed;
    public float ultPowerGenerated;
    public float wallDetectDist;
    public Transform playerPos;
    public Transform playerPosBottom;
    public Transform playerPosTop;
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
        //Generates raycasts that will be used to detect walls in front of the player from a certain distance
        //If that distance is shorter than the default dash distance, the player instead travels the differance between the detected platform and the player's current position
        RaycastHit2D hitMidR = Physics2D.Raycast(playerPos.position, Vector2.right, wallDetectDist, layerMask);
        RaycastHit2D hitTopR = Physics2D.Raycast(playerPosTop.position, Vector2.right, wallDetectDist, layerMask);
        RaycastHit2D hitBottomR = Physics2D.Raycast(playerPosBottom.position, Vector2.right, wallDetectDist, layerMask);
        if (hitMidR.collider == null || hitBottomR.collider == null || hitTopR.collider == null)
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
            float distance = hitMidR.point.x - playerPos.position.x;
            if (Input.GetMouseButtonDown(1) && !isClicked)
            {
                isClicked = true;
            }
            if (isClicked && distance > 1)
            {
                playerPos.position += new Vector3(dashSpeed, 0, 0);
                distance -= 1;
            }
            if (distance <= 1)
            {
                dashDist = startDashDist;
                isClicked = false;
            }
        }
    }
}
