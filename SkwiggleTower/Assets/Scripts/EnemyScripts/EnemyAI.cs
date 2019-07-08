using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

/// <summary>
/// 
/// Valarie Script, utilizes Pathfinding project to create simple enemy AI
/// </summary>
public class EnemyAI : MonoBehaviour
{

    public Transform target;

    public float speed = 200f;
    public float nextWaypointDistance = 3f;

    public Transform enemyGFX;

    Path path;
    int currentWaypoint = 0;
    bool reachedEndOfPath = false;

    Seeker seeker;
    Rigidbody2D rb;


    // Start is called before the first frame update
    void Start()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();

        InvokeRepeating("UpdatePath", 0f, .5f);
        
    }
    
    void OnPathComplete(Path _path)
    {
        if (!_path.error)
        {
            path = _path;
            currentWaypoint = 0;
        }

        //updates direction enemy is facing 
        if (rb.velocity.x >= 0.01f)
        {
            enemyGFX.localScale = new Vector3(-1, 1f, 1f); //travels to the right
        }
        else if (rb.velocity.x <= -0.01f)
        {
            enemyGFX.localScale = new Vector3(1, 1f, 1f); //flips character to face player on left
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (path == null)
            return;
        if (currentWaypoint >= path.vectorPath.Count)
        {
            reachedEndOfPath = true;
            return;
        }
        else
        {
            reachedEndOfPath = false;
        }
        //Calculated path
        Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;
        Vector2 force = direction * speed * Time.deltaTime;

        //Apply force to enemy in direction of player
        rb.AddForce(force);

        //calculate distance
        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);

        if(distance < nextWaypointDistance)
        {
            currentWaypoint++;
        }
    }

    void UpdatePath()
    {

        var players = RoomManager.instance.playerInputs;

        PlayerInput player = null;
        float shortestDist = 0f;

        for (int i = 0; i < players.Count; i++)
        {
            if (i == 0)
            {
                player = players[i];
                shortestDist = Vector2.Distance(transform.position, player.transform.position);
                continue;
            }

            var currentDist = Vector2.Distance(transform.position, players[i].transform.position);

            if (currentDist < shortestDist)
            {
                shortestDist = currentDist;
                player = players[i];
            }

        }

        if(seeker.IsDone()) //if not currently calculating a path it can update its path
            seeker.StartPath(rb.position, player.transform.position, OnPathComplete);
    }
}
