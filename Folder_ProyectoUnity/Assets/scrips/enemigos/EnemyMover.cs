using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMover : MonoBehaviour
{
    public Transform[] nodes; 
    public float speed = 2.0f; 
    private int currentNodeIndex = 0; 

    void Start()
    {
        if (nodes.Length > 0)
        {
            transform.position = nodes[currentNodeIndex].position;
           // MoveToNextNode();
        }
    }

    void Update()
    {
        if (nodes.Length > 0)
        {
            MoveTowardsTarget();
        }
    }

    void MoveTowardsTarget()
    {
        transform.position = Vector3.MoveTowards(transform.position, nodes[currentNodeIndex].position, speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, nodes[currentNodeIndex].position) < 0.1f)
        {
            currentNodeIndex++; 

            if (currentNodeIndex >= nodes.Length)
            {
                currentNodeIndex = 0; 
            }

           // MoveToNextNode(); 
        }
    }

   /* void MoveToNextNode()
    {
        // Aquí puedes agregar cualquier lógica adicional antes de moverse al siguiente nodo
    }*/
}
