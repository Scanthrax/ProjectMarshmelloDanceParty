using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RogueSecondaryAttackUpdated : MonoBehaviour
{
    public int damage;
    private Transform playerPos;
    public Transform attackPos;
    private Rigidbody2D rbody;
    public float dashSpeed;
    public float dashTime;
    public float startDashTime;
    private bool isClicked;
    public LayerMask whatAreEnemies;
    public Vector2 attackRange;
    public float startDashCooldown;
    public float dashCooldown;
    // Start is called before the first frame update
    void Start()
    {
        dashCooldown = 0;
        dashTime = startDashTime;
        playerPos = GetComponent<Transform>();
        rbody = GetComponent<Rigidbody2D>();
        isClicked = false;
    }

    // Update is called once per frame
    void Update()
    {
        //Executes dash attack when right clicked
        if(isClicked)
        {
            dashAttack();
        }
        //Executes end animation and puts back into effect collision between enemy and player
        if (!isClicked)
        {
            GetComponent<Animator>().SetTrigger("secondaryEnd");
            Physics2D.IgnoreLayerCollision(8, 11, false);
        }
        //Cooldown constantly decreases
        if (dashCooldown > 0)
        {
            dashCooldown--;
        }
    }
    public bool dashAttack()
    {
        if (isClicked && dashTime > 0)
        {
            //Moves player to right by dash speed until dashTime runs out
            if (playerPos.localScale == new Vector3(1, 1, 1))
            {
                rbody.AddForce(Vector2.right * dashSpeed);
                Debug.Log("Should dash right");
                dashTime--;
                //Ensures dash attack ends once dashTime runs out
                if(dashTime <= 0)
                {
                    dashTime = startDashTime;
                    return isClicked = false;
                }
            }
            //Same as above, but for moving left
            else
            {
                rbody.AddForce(Vector2.left * dashSpeed);
                Debug.Log("Should dash left");
                dashTime--;
                if (dashTime <= 0)
                {
                    dashTime = startDashTime;
                    return isClicked = false;
                }
            }
        }
        //Activates attack field when dash attack is active
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
                    //Adds poison script to enemies if Rogue ult is active
                    enemiesInRange[i].gameObject.AddComponent<RoguePoisonUlt>();
                    Debug.Log("This will activate");
                }
                else if (!GetComponent<RogueUltTestActivation>().active && GetComponent<RogueUltTestActivation>() != null)
                {
                    Debug.Log("This will not activate");
                }
            }
        }
        return isClicked;
    }
    public void Click()
    {
        if (dashCooldown <= 0)
        {
            isClicked = true;
            dashCooldown = startDashCooldown;
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(attackPos.position, attackRange);
    }
}
