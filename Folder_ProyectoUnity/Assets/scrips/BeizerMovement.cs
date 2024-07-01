using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;
using Unity.VisualScripting;

using UnityEngine.Rendering.Universal;


public class BeizerMovement : MonoBehaviour
{
    [SerializeField] private BeizerCubic beizerCubic;
    [SerializeField] private float speed;
    [SerializeField] private float sampleTime = 0f;
    private NavMeshAgent navMeshAgent;
    public Transform jugador;
    public float tiemposalto;
    private Animator animator;
    public float rotationSpeed = 5.0f;
    public float nexataque;
    public float coldwun = 2;
    public static event Action ataque2;


    private void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();

        // Desactivamos inicialmente el NavMeshAgent
        navMeshAgent.enabled = false;

        // Inicializa el Animator
        animator = GetComponent<Animator>();
    }
    private void Update() 
    {
        if (tiemposalto < 4)
        {
            animator.SetBool("salto", true);
            sampleTime += Time.deltaTime * speed;

            transform.position = beizerCubic.Position(sampleTime);

            transform.forward = beizerCubic.Position(sampleTime + 0.001f) - transform.position;
            /*    if(sampleTime >= 1f){
                     sampleTime = 0;
                 }*/
            tiemposalto =tiemposalto+Time.deltaTime;
        }
        else
        {
            animator.SetBool("salto", false);
            navMeshAgent.enabled = true;
            PerseguirJugador();
        }
        if (Vector3.Distance(transform.position, jugador.position) < 10 && Time.time >= nexataque)
        {
            ataque2?.Invoke();
            animator.SetBool("IsAttacking", true);
            Debug.Log("ataque");
            nexataque = Time.time + coldwun;
        }
        else
        {

            animator.SetBool("IsAttacking", false);
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
}
