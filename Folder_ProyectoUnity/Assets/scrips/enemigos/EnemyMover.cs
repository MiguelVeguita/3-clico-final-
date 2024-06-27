using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMover : MonoBehaviour
{
    public DoubleCircularList<Transform> nodos = new DoubleCircularList<Transform>();
    public Transform[] puntos; // Array de puntos para la patrulla
    public Transform jugador; // Referencia al transform del jugador
    public float speed = 2.0f;
    public float rotationSpeed = 5.0f; // Velocidad de rotación
    public float detectionRadius = 5.0f; // Radio de detección para comenzar la persecución
    private DoubleCircularList<Transform>.Node current;
    private NavMeshAgent navMeshAgent;
    private bool persiguiendoJugador = false;
    private Animator animator; // Referencia al Animator

    private void Awake()
    {
        // Inicializa la lista circular con los puntos
        for (int i = 0; i < puntos.Length; i++)
        {
            nodos.InsertNodeAtEnd(puntos[i]);
        }
        current = nodos.GetFirstNodeA();

        // Inicializa el NavMeshAgent
        navMeshAgent = GetComponent<NavMeshAgent>();

        // Desactivamos inicialmente el NavMeshAgent
        navMeshAgent.enabled = false;

        // Inicializa el Animator
        animator = GetComponent<Animator>();
    }

    void Start()
    {
        if (current != null)
        {
            // Coloca al enemigo en la posición del primer nodo
            transform.position = current.Value.position;
        }
    }

    void Update()
    {
        if (persiguiendoJugador)
        {
            // Si está persiguiendo al jugador, usa el NavMeshAgent para seguir al jugador
            PerseguirJugador();
        }
        else
        {
            // Si no está persiguiendo al jugador, sigue los puntos y revisa si el jugador está cerca
            if (Vector3.Distance(transform.position, jugador.position) <= detectionRadius)
            {
                // Si el jugador está dentro del radio de detección, comienza a perseguirlo
                persiguiendoJugador = true;

                // Activamos el NavMeshAgent para la persecución
                navMeshAgent.enabled = true;

                // Asignamos la posición actual del enemigo al NavMeshAgent para evitar saltos
                navMeshAgent.Warp(transform.position);
            }
            else
            {
                // Si el jugador no está cerca, sigue los puntos
                MoverEntrePuntos();
            }
        }

        // Actualiza la animación dependiendo de si el enemigo está en movimiento o no
       /* if (animator != null)
        {
            bool isMoving = navMeshAgent.velocity.sqrMagnitude > 0.1f || (!navMeshAgent.enabled && current != null && Vector3.Distance(transform.position, current.Value.position) > 0.1f);
            animator.SetBool("IsWalking", isMoving);
        }*/
    }

    void MoverEntrePuntos()
    {
        if (current != null)
        {
            // Calcula la dirección hacia el siguiente punto
            Vector3 direction = (current.Value.position - transform.position).normalized;

            // Calcula la rotación hacia esa dirección
            Quaternion lookRotation = Quaternion.LookRotation(direction);

            // Aplica una rotación suave hacia el siguiente punto
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);

            // Mueve al enemigo hacia el nodo actual
            transform.position = Vector3.MoveTowards(transform.position, current.Value.position, speed * Time.deltaTime);

            // Verifica si el enemigo ha alcanzado el nodo actual
            if (Vector3.Distance(transform.position, current.Value.position) < 0.1f)
            {
                // Mueve al siguiente nodo en la lista
                current = current.Next;
            }
        }
    }

    void PerseguirJugador()
    {
        if (navMeshAgent != null && jugador != null)
        {
            // Establece el destino del NavMeshAgent hacia la posición del jugador
            navMeshAgent.SetDestination(jugador.position);

            // Asegúrate de que el enemigo mire hacia el jugador mientras lo persigue
            Vector3 direction = (jugador.position - transform.position).normalized;
            if (direction != Vector3.zero) // Evitar la rotación si no hay dirección
            {
                Quaternion lookRotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);
            }
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "bala")
        {
            Destroy(this.gameObject);
        }
    }
    void OnDrawGizmosSelected()
    {
        // Dibuja un gizmo para visualizar el radio de detección en el editor
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}
