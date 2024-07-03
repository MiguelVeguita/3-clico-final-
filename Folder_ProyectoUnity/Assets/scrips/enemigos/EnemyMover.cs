using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Rendering.Universal;



public class EnemyMover : MonoBehaviour
{
    public DoubleCircularList<Transform> nodos = new DoubleCircularList<Transform>();
    public Transform[] puntos; // Array de puntos para la patrulla
    public Transform jugador; // Referencia al transform del jugador
    public float speed = 2.0f;
    public float life;
    public float rotationSpeed = 5.0f; // Velocidad de rotaci�n
    public float detectionRadius = 5.0f; // Radio de detecci�n para comenzar la persecuci�n
    private DoubleCircularList<Transform>.Node current;
    private NavMeshAgent navMeshAgent;
    private bool persiguiendoJugador = false;
    private Animator animator; // Referencia al Animator
    public float nexataque;
    public float coldwun=2;
    private bool muerte=false;
    public float timedead = 0;
    public float timeup;
    public AudioClip sonidoAtaque; // Clip de sonido para el ataque
    private AudioSource audioSource;
    public static event Action ataque;

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
        audioSource = GetComponent<AudioSource>();
    }

    void Start()
    {
        if (current != null)
        {
            // Coloca al enemigo en la posici�n del primer nodo
            transform.position = current.Value.position;
        }
    }

    void Update()
    {
        if (muerte == true)
        {
            navMeshAgent.enabled = false;

            timedead = Time.deltaTime+timedead;
            if (timedead >= timeup)
            {

                Destroy(this.gameObject);
            }
        }
        if(Vector3.Distance(transform.position, jugador.position) < 10 && Time.time>= nexataque)
        {


            animator.SetBool("IsAttacking", true);
            if (sonidoAtaque != null && audioSource != null)
            {
                audioSource.Play();
            }
            Debug.Log("ataque");
            nexataque = Time.time + coldwun;
            ataque?.Invoke();
        }
        else
        {
           
            animator.SetBool("IsAttacking", false);
        }

        if (persiguiendoJugador)
        {
            // Si est� persiguiendo al jugador, usa el NavMeshAgent para seguir al jugador
            PerseguirJugador();
        }
        else
        {
            // Si no est� persiguiendo al jugador, sigue los puntos y revisa si el jugador est� cerca
            if (Vector3.Distance(transform.position, jugador.position) <= detectionRadius)
            {
                // Si el jugador est� dentro del radio de detecci�n, comienza a perseguirlo
                persiguiendoJugador = true;

                // Activamos el NavMeshAgent para la persecuci�n
                navMeshAgent.enabled = true;

                // Asignamos la posici�n actual del enemigo al NavMeshAgent para evitar saltos
                navMeshAgent.Warp(transform.position);
            }
            else
            {
                // Si el jugador no est� cerca, sigue los puntos
                MoverEntrePuntos();
            }
        }

     
    }

    void MoverEntrePuntos()
    {
        if (current != null)
        {
            // Calcula la direcci�n hacia el siguiente punto
            Vector3 direction = (current.Value.position - transform.position).normalized;

            // Calcula la rotaci�n hacia esa direcci�n
            Quaternion lookRotation = Quaternion.LookRotation(direction);

            // Aplica una rotaci�n suave hacia el siguiente punto
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
            // Establece el destino del NavMeshAgent hacia la posici�n del jugador
            navMeshAgent.SetDestination(jugador.position);

            // Aseg�rate de que el enemigo mire hacia el jugador mientras lo persigue
            Vector3 direction = (jugador.position - transform.position).normalized;
            if (direction != Vector3.zero) // Evitar la rotaci�n si no hay direcci�n
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
            life--;
            if (life <= 0)
            {
                animator.SetBool("dead", true);
                muerte = true;
            }
            
        }
    }
    
    void OnDrawGizmosSelected()
    {
        // Dibuja un gizmo para visualizar el radio de detecci�n en el editor
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}
