using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Transform target;
    //NavMeshAgent agent;
    private float rotSpeed = 5f;

    // Start is called before the first frame update
    void Start()
    {
      
    }

    void Update()
    {
        if (target != null)
        {
          
            FaceTarget();
        }
    }

    public void Movement()
    {
        
    }

    public void FollowTarget(Interactable newTarget)
    {
       

        target = newTarget.interactionTransform;
    }

    public void stopFollowing()
    {
     
        target = null;
    }

    public void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0f, direction.z)); //prevents player from looking up and down
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotSpeed);

    }
}
