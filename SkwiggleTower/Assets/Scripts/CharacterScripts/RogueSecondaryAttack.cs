﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RogueSecondaryAttack : MonoBehaviour
{
    //Not all of these variables are used yet. Currently working on getting the dash to work correctly.
    public Vector2 attackRange;
    public int damage;
    public float distanceFromPlayer;
    public float dashCooldown;
    public float dashDist;
    public float dashSpeed;
    public float ultPowerGenerated;
    public float wallDetectDist;
    public Transform attackPos;
    public Transform playerPos;
    public Transform playerPosBottom;
    public Transform playerPosTop;
    public LayerMask whatAreEnemies;
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
        //Right now, mid raycast is working correctly. Since it's a prototype, it just needs to demonstrate basic unpolished functionality
        RaycastHit2D hitMidR = Physics2D.Raycast(playerPos.position, Vector2.right, wallDetectDist, layerMask);
        RaycastHit2D hitMidL = Physics2D.Raycast(playerPos.position, Vector2.left, wallDetectDist, layerMask);
        //RaycastHit2D hitTopR = Physics2D.Raycast(playerPosTop.position, Vector2.right, wallDetectDist, layerMask);
        //RaycastHit2D hitBottomR = Physics2D.Raycast(playerPosBottom.position, Vector2.right, wallDetectDist, layerMask);
        if(playerPos.localScale == new Vector3(1, 1, 1))//checks if player is facing right
        {
            if (hitMidR.collider == null)
            {
                //Attack is activated by right click
                if (Input.GetMouseButtonDown(1) && !isClicked)
                {
                    isClicked = true;
                }
                if (isClicked && dashDist >= 0)
                {
                    //dashes forward until dashDist depletes
                    playerPos.position += new Vector3(dashSpeed, 0, 0);
                    dashDist -= 1;
                }
                if (dashDist < 0)
                {
                    dashDist = startDashDist;
                    isClicked = false;
                }
                Collider2D[] enemiesInRange = Physics2D.OverlapBoxAll(attackPos.position, attackRange, whatAreEnemies);
                for (int i = 0; i < enemiesInRange.Length; i++)
                {
                    //Uses a method in CharacterStats.cs for enemy to take damage
                    enemiesInRange[i].GetComponent<CharacterStats>().TakeDamage(damage);
                    Debug.Log("Got 'em");
                }
            }
            else
            {
                //If wall is detected and distance is less than dashDist, it calculates new distance and dashes that amount to the right
                //Debug.Log("I see you");
                float distanceMid = hitMidR.point.x - playerPos.position.x;

                if (Input.GetMouseButtonDown(1) && !isClicked)
                {
                    isClicked = true;
                }
                if (isClicked && distanceMid > 1)
                {
                    playerPos.position += new Vector3(dashSpeed, 0, 0);
                    distanceMid -= 1;
                }
                if (distanceMid <= 1)
                {
                    dashDist = startDashDist;
                    isClicked = false;
                }
                Collider2D[] enemiesInRange = Physics2D.OverlapBoxAll(attackPos.position, attackRange, whatAreEnemies);
                for (int i = 0; i < enemiesInRange.Length; i++)
                {
                    //Uses a method in CharacterStats.cs for enemy to take damage
                    enemiesInRange[i].GetComponent<CharacterStats>().TakeDamage(damage);
                    Debug.Log("Got 'em");
                }
            }
        }
        else//the case if player is facing left
        {
            if (hitMidL.collider == null)
            {
                //Attack is activated by right click
                if (Input.GetMouseButtonDown(1) && !isClicked)
                {
                    isClicked = true;
                }
                if (isClicked && dashDist >= 0)
                {
                    //dashes left while dashDist is greater than or equal to 0
                    playerPos.position -= new Vector3(dashSpeed, 0, 0);
                    dashDist -= 1;
                }
                if (dashDist < 0)
                {
                    dashDist = startDashDist;
                    isClicked = false;
                }
                Collider2D[] enemiesInRange = Physics2D.OverlapBoxAll(attackPos.position, attackRange, whatAreEnemies);
                for (int i = 0; i < enemiesInRange.Length; i++)
                {
                    //Uses a method in CharacterStats.cs for enemy to take damage
                    enemiesInRange[i].GetComponent<CharacterStats>().TakeDamage(damage);
                    Debug.Log("Got 'em");
                }
            }
            else
            {
                //if wall to left is detected, calculates distance and dashes that amount to the left
                //Debug.Log("I see you");
                float distanceMid = playerPos.position.x - hitMidR.point.x;

                if (Input.GetMouseButtonDown(1) && !isClicked)
                {
                    isClicked = true;
                }
                if (isClicked && distanceMid >= -2)
                {
                    playerPos.position -= new Vector3(dashSpeed, 0, 0);
                    distanceMid -= 1;
                }
                if (distanceMid < -2)
                {
                    dashDist = startDashDist;
                    isClicked = false;
                }
                Collider2D[] enemiesInRange = Physics2D.OverlapBoxAll(attackPos.position, attackRange, whatAreEnemies);
                for (int i = 0; i < enemiesInRange.Length; i++)
                {
                    //Uses a method in CharacterStats.cs for enemy to take damage
                    enemiesInRange[i].GetComponent<CharacterStats>().TakeDamage(damage);
                    Debug.Log("Got 'em");
                }
            }
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(attackPos.position, attackRange);
    }
}