using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public Transform pointA;
    public Transform pointB;
    public Transform player;
    public float detectionRange = 10f;
    public float speed = 3f;

    private Vector3 targetPoint;
    private bool isPlayerDetected = false;

    void Start()
    {
        
        targetPoint = pointA.position;
    }

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer < detectionRange)
        {
         
            isPlayerDetected = true;
            targetPoint = player.position;
        }
        else
        {
          
            isPlayerDetected = false;
            Patrol();
        }

        MoveToTarget();
    }

    void Patrol()
    {
       
        if (Vector3.Distance(transform.position, targetPoint) < 0.5f)
        {
            targetPoint = targetPoint == pointA.position ? pointB.position : pointA.position;
        }
    }

    void MoveToTarget()
    {
      
        Vector3 direction = (targetPoint - transform.position).normalized;
        transform.position += direction * speed * Time.deltaTime;

       
        if (direction != Vector3.zero)
        {
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
        }
    }
}
