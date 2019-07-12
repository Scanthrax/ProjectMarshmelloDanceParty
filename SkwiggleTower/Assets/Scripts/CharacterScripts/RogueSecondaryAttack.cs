//Jacob Hreshchyshyn
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RogueSecondaryAttack : Ability
{
    //Not all of these variables are used yet. Currently working on getting the dash to work correctly.
    public Vector2 attackRange;
    public int damage;
    public float distanceFromPlayer;
    public float startDashCooldown;
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
    private float dashCoolDown;
    private bool isClicked;
    // Start is called before the first frame update
    void Start()
    {
        dashCoolDown = 0;
        startDashDist = dashDist;
        isClicked = false;
    }

    // Update is called once per frame
    void Update()
    {
        base.Update();

        //if ((Input.GetMouseButtonDown(1) || isClicked))
        if (isClicked)
        {
            dashAttack();
        }
        dashCoolDown--;
    }
    public bool dashAttack()
    {
        //detecting platform layer 9
        int layerMask = 1 << 9;
        //Generates raycasts that will be used to detect walls in front of the player from a certain distance
        //If that distance is shorter than the default dash distance, the player instead travels the differance between the detected platform and the player's current position
        //Right now, mid raycast is working correctly. Since it's a prototype, it just needs to demonstrate basic unpolished functionality
        RaycastHit2D hitMidR = Physics2D.Raycast(playerPos.position, Vector2.right, wallDetectDist, layerMask);
        RaycastHit2D hitTopR = Physics2D.Raycast(playerPosTop.position, Vector2.right, wallDetectDist, layerMask);
        RaycastHit2D hitBottomR = Physics2D.Raycast(playerPosBottom.position, Vector2.right, wallDetectDist, layerMask);
        RaycastHit2D hitMidL = Physics2D.Raycast(playerPos.position, Vector2.left, wallDetectDist, layerMask);
        if (playerPos.localScale == new Vector3(1, 1, 1))//checks if player is facing right
        {
            if (hitMidR.collider == null)// && hitBottomR.collider == null && hitTopR.collider == null)
            {
                //Attack is activated by right click
                if (!isClicked && dashCoolDown <= 0)
                {
                    isClicked = true;
                    dashCoolDown = startDashCooldown;
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
                if (isClicked)
                {
                    Physics2D.IgnoreLayerCollision(8, 11, true);
                    Collider2D[] enemiesInRange = Physics2D.OverlapBoxAll(attackPos.position, attackRange, 0, whatAreEnemies);
                    for (int i = 0; i < enemiesInRange.Length; i++)
                    {
                        //Uses a method in CharacterStats.cs for enemy to take damage
                        enemiesInRange[i].GetComponent<CharacterStats>().TakeDamage(damage);
                        Debug.Log("Got 'em");
                        if (GetComponent<RogueUltTestActivation>().active && GetComponent<RoguePoisonUlt>() == null)
                        {
                            enemiesInRange[i].gameObject.AddComponent<RoguePoisonUlt>();
                            Debug.Log("This will activate");
                        }
                        else if (!GetComponent<RogueUltTestActivation>().active && GetComponent<RogueUltTestActivation>() != null)
                        {
                            Debug.Log("This will not activate");
                        }
                    }
                }
            }
            else
            {
                //If wall is detected and distance is less than dashDist, it calculates new distance and dashes that amount to the right
                //Debug.Log("I see you");
                //float distanceMid = hitMidR.point.x - playerPos.position.x;
                //float distanceTop = hitTopR.point.x - playerPos.position.x;
                float distanceBottom = hitBottomR.point.x - playerPos.position.x;
                //float smallestDist = Mathf.Min(distanceMid, distanceTop, distanceBottom);
                float smallestDist = distanceBottom;
                Debug.Log(smallestDist);
                if (!isClicked && dashCoolDown <= 0)
                {
                    isClicked = true;
                    dashCoolDown = startDashCooldown;
                }
                if (isClicked && smallestDist > 1)
                {
                    playerPos.position += new Vector3(dashSpeed, 0, 0);
                    smallestDist -= 1;
                }
                if (smallestDist <= 1)
                {
                    dashDist = startDashDist;
                    isClicked = false;
                }
                if (isClicked)
                {
                    Collider2D[] enemiesInRange = Physics2D.OverlapBoxAll(attackPos.position, attackRange, whatAreEnemies);
                    for (int i = 0; i < enemiesInRange.Length; i++)
                    {
                        //Uses a method in CharacterStats.cs for enemy to take damage
                        enemiesInRange[i].GetComponent<CharacterStats>().TakeDamage(damage);
                        Debug.Log("Got 'em");
                        if (GetComponent<RogueUltTestActivation>().active && GetComponent<RoguePoisonUlt>() == null)
                        {
                            enemiesInRange[i].gameObject.AddComponent<RoguePoisonUlt>();
                            Debug.Log("This will activate");
                        }
                        else if (!GetComponent<RogueUltTestActivation>().active && GetComponent<RogueUltTestActivation>() != null)
                        {
                            Debug.Log("This will not activate");
                        }
                    }
                }
            }
        }
        /*else//the case if player is facing left
        {
            if (hitMidL.collider == null)
            {
                //Attack is activated by right click
                if (!isClicked && dashCoolDown <= 0)
                {
                    isClicked = true;
                    dashCoolDown = startDashCooldown;
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
                if (isClicked)
                {
                    Collider2D[] enemiesInRange = Physics2D.OverlapBoxAll(attackPos.position, attackRange, whatAreEnemies);
                    for (int i = 0; i < enemiesInRange.Length; i++)
                    {
                        //Uses a method in CharacterStats.cs for enemy to take damage
                        enemiesInRange[i].GetComponent<CharacterStats>().TakeDamage(damage);
                        Debug.Log("Got 'em");
                        if (GetComponent<RogueUltTestActivation>().active && GetComponent<RoguePoisonUlt>() == null)
                        {
                            enemiesInRange[i].gameObject.AddComponent<RoguePoisonUlt>();
                            Debug.Log("This will activate");
                        }
                        else if (!GetComponent<RogueUltTestActivation>().active && GetComponent<RogueUltTestActivation>() != null)
                        {
                            Debug.Log("This will not activate");
                        }
                    }
                }
            }
            else
            {
                //if wall to left is detected, calculates distance and dashes that amount to the left
                //Debug.Log("I see you");
                float distanceMid = playerPos.position.x - hitMidR.point.x;

                if (!isClicked && dashCoolDown <= 0)
                {
                    isClicked = true;
                    dashCoolDown = startDashCooldown;
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
                if (isClicked)
                {
                    Collider2D[] enemiesInRange = Physics2D.OverlapBoxAll(attackPos.position, attackRange, whatAreEnemies);
                    for (int i = 0; i < enemiesInRange.Length; i++)
                    {
                        //Uses a method in CharacterStats.cs for enemy to take damage
                        enemiesInRange[i].GetComponent<CharacterStats>().TakeDamage(damage);
                        Debug.Log("Got 'em");
                        if (GetComponent<RogueUltTestActivation>().active && GetComponent<RoguePoisonUlt>() == null)
                        {
                            enemiesInRange[i].gameObject.AddComponent<RoguePoisonUlt>();
                            Debug.Log("This will activate");
                        }
                        else if (!GetComponent<RogueUltTestActivation>().active && GetComponent<RogueUltTestActivation>() != null)
                        {
                            Debug.Log("This will not activate");
                        }
                    }
                }
            }
        }*/
        dashCoolDown -= 1;



        if (!isClicked)
            GetComponent<Animator>().SetTrigger("secondaryEnd");

        return isClicked;
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(attackPos.position, attackRange);
       // Gizmos.DrawWireSphere(playerPosTop.position, 1);
    }


    public void Click()
    {
        isClicked = true;
    }

}

