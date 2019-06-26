using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RogueSecondaryAttackUpdated : MonoBehaviour
{
    private Transform attackPos;
    private Rigidbody2D rbody;
    public float dashSpeed;
    private float dashTime;
    public float startDashTime;
    private bool isClicked;
    // Start is called before the first frame update
    void Start()
    {
        attackPos = GetComponent<Transform>();
        rbody = GetComponent<Rigidbody2D>();
        dashTime = startDashTime;
        isClicked = false;
    }

    // Update is called once per frame
    void Update()
    {
        /*if(isClicked)
        {
            dashAttack();
        }*/
        //rbody.velocity = Vector2.right * 3;
        rbody.AddForce(Vector2.right * dashSpeed);
    }
    public bool dashAttack()
    {
        if(!isClicked)
        {
            isClicked = true;
        }
        if(isClicked && dashTime > 0)
        {
            rbody.velocity = Vector2.right * dashSpeed;
            Debug.Log("Should dash");
        }
        else
        {
            dashTime = startDashTime;
            isClicked = false;
        }
        dashTime--;
        if (!isClicked)
            GetComponent<Animator>().SetTrigger("secondaryEnd");
        return isClicked;
    }
    public void Click()
    {
        isClicked = true;
    }
}
