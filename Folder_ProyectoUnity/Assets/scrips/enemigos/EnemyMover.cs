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
    public Transform[] puntos; 
    public Transform jugador; 
    public float speed = 2.0f;
    public float life;
    public float rotationSpeed = 5.0f; 
    public float detectionRadius = 5.0f; 
    private DoubleCircularList<Transform>.Node current;
    private NavMeshAgent navMeshAgent;
    private bool persiguiendoJugador = false;
    private Animator animator;
    public float nexataque;
    public float coldwun=2;
    private bool muerte=false;
    public float timedead = 0;
    public float timeup;
    public AudioClip sonidoAtaque; 
    private AudioSource audioSource;
    public static event Action ataque;

    private void Awake()
    {
        
        for (int i = 0; i < puntos.Length; i++)
        {
            nodos.InsertNodeAtEnd(puntos[i]);
        }
        current = nodos.GetFirstNodeA();

        navMeshAgent = GetComponent<NavMeshAgent>();

        navMeshAgent.enabled = false;

        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    void Start()
    {
        if (current != null)
        {
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
            PerseguirJugador();
        }
        else
        {
            if (Vector3.Distance(transform.position, jugador.position) <= detectionRadius)
            {
                persiguiendoJugador = true;

                navMeshAgent.enabled = true;

                navMeshAgent.Warp(transform.position);
            }
            else
            {
                MoverEntrePuntos();
            }
        }

     
    }

    void MoverEntrePuntos()
    {
        if (current != null)
        {
            Vector3 direction = (current.Value.position - transform.position).normalized;

            Quaternion lookRotation = Quaternion.LookRotation(direction);

            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);

            transform.position = Vector3.MoveTowards(transform.position, current.Value.position, speed * Time.deltaTime);

            if (Vector3.Distance(transform.position, current.Value.position) < 0.1f)
            {
                current = current.Next;
            }
        }
    }

    void PerseguirJugador()
    {
        if (navMeshAgent != null && jugador != null)
        {
            navMeshAgent.SetDestination(jugador.position);

            Vector3 direction = (jugador.position - transform.position).normalized;
            if (direction != Vector3.zero) 
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
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}
