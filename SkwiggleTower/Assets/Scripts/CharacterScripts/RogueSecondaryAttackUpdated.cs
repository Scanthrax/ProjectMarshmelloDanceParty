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
    // Start is called before the first frame update
    void Start()
    {
        playerPos = GetComponent<Transform>();
        rbody = GetComponent<Rigidbody2D>();
        isClicked = false;
    }

    // Update is called once per frame
    void Update()
    {
       /* if(dashTime > 0)
        {
            rbody.AddForce(Vector2.right * dashSpeed);
        }*/
        if(dashTime > 0)
        {
            dashAttack();
        }
        //rbody.velocity = Vector2.right * 3;
        //rbody.AddForce(Vector2.right * dashSpeed);
    }
    public bool dashAttack()
    {
        if (!isClicked)
        {
            isClicked = true;
        }
        if (isClicked)
        {
            Physics2D.IgnoreLayerCollision(8, 11, true);
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
        if (isClicked && dashTime > 0)
        {
            if (playerPos.localScale == new Vector3(1, 1, 1))
            {
                rbody.AddForce(Vector2.right * dashSpeed);
                Debug.Log("Should dash right");
            }
            else
            {
                rbody.AddForce(Vector2.left * dashSpeed);
                Debug.Log("Should dash left");
            }
            dashTime--;
        }
        if(dashTime <= 0)
        {
            //dashTime = startDashTime;
            isClicked = false;
        }
        if (!isClicked)
        {
            GetComponent<Animator>().SetTrigger("secondaryEnd");
            Physics2D.IgnoreLayerCollision(8, 11, false);
        }
        return isClicked;
    }
    public void Click()
    {
        dashTime = startDashTime;
        isClicked = true;
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(attackPos.position, attackRange);
    }
}
