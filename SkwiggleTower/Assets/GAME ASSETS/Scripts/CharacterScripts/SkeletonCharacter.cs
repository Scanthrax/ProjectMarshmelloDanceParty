using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonCharacter : EnemyCharacter, IPooledObject
{

    public Transform skeleHead;
    public Transform skeleBody;




    public override void Die()
    {
        var head = ObjectPoolManager.instance.SpawnFromPool(skeleHead, (Vector2)root.transform.position + Vector2.up * 1f, Quaternion.identity).GetComponentInChildren<EnemyCharacter>();
        head.rigidBody.AddForce(new Vector2(10f * input.faceDirection,10f),ForceMode2D.Impulse);

        var body = ObjectPoolManager.instance.SpawnFromPool(skeleBody, root.transform.position, Quaternion.identity).GetComponentInChildren<EnemyCharacter>();

        RoomManager.instance.listOfSkeletonBodies.Add(body);
        RoomManager.instance.listOfSkeletonHeads.Add(head);

        ObjectPoolManager.instance.BackToPool(root, false);



        body.DeathEvent += delegate
        {
            RoomManager.instance.listOfSkeletonBodies.Remove(body);

            RoomManager.instance.CalculateClosestBody();
        };

        head.DeathEvent += delegate
        {
            RoomManager.instance.listOfSkeletonHeads.Remove(head);
        };



        RoomManager.instance.CalculateClosestBody();

    }

}
