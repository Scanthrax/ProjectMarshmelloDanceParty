using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeGuardAI : MonoBehaviour
{

    public float speed = 200f;
    public float nextWaypointDistance = 3f;


    Path path;
    int previousWaypointIndex;
    int currentWaypointIndex;
    bool reachedEndOfPath = false;

    Seeker seeker;
    Rigidbody2D rb;

    public bool inAttackRange;
    Animator anim;
    public Ability rangedAttack;



    public int waypointIndex;
    public Transform waypoints;
    public bool canPathfind;


    public float impulse;



    public List<PlayerStats> playersInRange;


    public bool arePlayersInRange { get { return playersInRange.Count > 0; } }

    public int direction;



    Vector3 previousWaypoint, currentWaypoint;

    Vector3 waypointPosition;

    public Collider2D enemyCollider;


    public float attackRange;

    public Vector3 destination;
    Transform destTransform;

    public bool idle;

    // Start is called before the first frame update
    void Start()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();

        InvokeRepeating("UpdatePath", 0f, .5f);
        anim = GetComponent<Animator>();

        previousWaypointIndex = 1;
        currentWaypointIndex = 2;
    }

    void OnPathComplete(Path _path)
    {
        if (!_path.error)
        {
            path = _path;
            currentWaypointIndex = 0;
        }

    }

    // Update is called once per frame
    void FixedUpdate()
    {



        // Cast a ray straight down.
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.right * direction, attackRange);

        // If it hits something...
        if (hit.collider)
        {
            var x = hit.collider.GetComponent<PlayerStats>();

            if (x)
            {
                GetComponent<EnemyStats>().primary.Cast();
            }
        }

        if (path == null || !canPathfind)
            return;

        //if (currentWaypointIndex >= path.vectorPath.Count)
        //{
        //    reachedEndOfPath = true;
        //    return;
        //}
        //else
        //{
        //    reachedEndOfPath = false;
        //}

        previousWaypoint = path.vectorPath[previousWaypointIndex];



        //Calculated path
        var newDirection = destination.x < rb.transform.position.x ? -1 : 1;
        if (direction != newDirection) ChangeDirection(newDirection);


        Vector2 force = new Vector2(direction * impulse, rb.velocity.y);

        if (!idle)
            //Apply force to enemy in direction of player
            rb.velocity = force;
        else
            rb.velocity = new Vector2(0, rb.velocity.y);

        //calculate distance
        float distance = Vector2.Distance(rb.position, previousWaypoint);






    }





    void UpdatePath()
    {
        if (!canPathfind) return;
        //var players = RoomManager.instance.playerInputs;

        //PlayerInput player = null;
        //float shortestDist = 0f;

        //for (int i = 0; i < players.Count; i++)
        //{
        //    if (i == 0)
        //    {
        //        player = players[i];
        //        shortestDist = Vector2.Distance(transform.position, player.transform.position);
        //        continue;
        //    }

        //    var currentDist = Vector2.Distance(transform.position, players[i].transform.position);

        //    if (currentDist < shortestDist)
        //    {
        //        shortestDist = currentDist;
        //        player = players[i];
        //    }

        //}

        //inAttackRange = shortestDist < 5f;
        //anim.SetBool("inAttackRange", inAttackRange);
        //if(inAttackRange)
        //    rangedAttack.Cast();

        if (destTransform)
            destination = destTransform.position;

        if (seeker.IsDone()) //if not currently calculating a path it can update its path
            seeker.StartPath(rb.position, destination, OnPathComplete);
    }


    public void NextIndex()
    {
        waypointIndex++;
        if (waypointIndex >= waypoints.childCount)
            waypointIndex = 0;
    }




    public void ChangeDirection(int dir)
    {
        direction = dir;
        transform.localScale = new Vector3(direction, 1, 1);
        GetComponent<CharacterStats>().direction = direction;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(previousWaypoint, 0.1f);


        Gizmos.DrawLine(transform.position, transform.position + transform.right * attackRange * direction);
    }


    public void SetWaypoint()
    {
        destination = waypoints.GetChild(waypointIndex).transform.position;
        destTransform = null;
        path = null;
    }
}



