using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RogueSecondaryAttackUpdated : Ability
{
    //public int damage;
    private Transform playerPos;
    public Vector2 attackPos;
    private Rigidbody2D rbody;
    public float dashSpeed;
    public float dashTime;
    public float startDashTime;
    private bool isClicked;
    public LayerMask whatAreEnemies;
    public Vector2 attackRange;
    public float startDashCooldown;
    public float dashCooldown;



    public List<Collider2D> enemiesHit;


    // Start is called before the first frame update
    new void Start()
    {
        base.Start();

        dashCooldown = 0;
        dashTime = startDashTime;
        playerPos = GetComponent<Transform>();
        rbody = GetComponent<Rigidbody2D>();
        isClicked = false;
    }

    // Update is called once per frame
    new void Update()
    {
        base.Update();
        //Executes dash attack when right clicked
        if(isClicked)
        {
            GetComponent<Animator>().SetBool("SecondaryActive", true);
            dashAttack();
        }
        //Executes end animation and puts back into effect collision between enemy and player
        if (!isClicked)
        {
            GetComponent<Animator>().SetBool("SecondaryActive",false);
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
            Collider2D[] enemiesInRange = Physics2D.OverlapBoxAll(transform.position + new Vector3(attackPos.x * character.direction,attackPos.y) , attackRange, 0, whatAreEnemies);
            for (int i = 0; i < enemiesInRange.Length; i++)
            {
                var characterStats = enemiesInRange[i].GetComponent<CharacterStats>();

                if (enemiesHit.Contains(enemiesInRange[i]) || !characterStats) continue;

                

                //Uses a method in CharacterStats.cs for enemy to take damage

                if (this.character.direction == characterStats.direction)
                {
                    damage = Mathf.RoundToInt(damage * 1.5f);

                    // FOR NOW, reset the rogue's dash on a backstab
                    timer = duration;
                    dashCooldown = 0f;
                }

                print(damage);

                characterStats.TakeDamage(damage);
                Debug.Log("Got 'em");

                enemiesHit.Add(enemiesInRange[i]);


                if((character as PlayerStats).poisonStacks > 0)
                {
                    characterStats.gameObject.AddComponent<RoguePoisonUlt>();
                    (character as PlayerStats).poisonStacks--;

                    if ((character as PlayerStats).poisonStacks <= 0)
                        (character as PlayerStats).poisonBlade.gameObject.SetActive(false);

                    RoomManager.instance.poisonCharges.text = (character as PlayerStats).poisonStacks.ToString();
                }

                //if (GetComponent<RogueUltTestActivation>().active && GetComponent<RoguePoisonUlt>() == null)
                //{
                //    //Adds poison script to enemies if Rogue ult is active
                //    enemiesInRange[i].gameObject.AddComponent<RoguePoisonUlt>();
                //    Debug.Log("This will activate");
                //}
                //else if (!GetComponent<RogueUltTestActivation>().active && GetComponent<RogueUltTestActivation>() != null)
                //{
                //    Debug.Log("This will not activate");
                //}


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
            enemiesHit.Clear();
        }
    }

    public override void Cast()
    {
        
        Click();
        timer = 0f;
        base.Cast();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;

        int i = character ? character.direction : 1;
        Gizmos.DrawWireCube(transform.position + new Vector3(attackPos.x * i, attackPos.y), attackRange);
    }
}
