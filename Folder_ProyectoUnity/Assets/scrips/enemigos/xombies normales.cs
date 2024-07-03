using System;
using UnityEngine;
using UnityEngine.AI;

public class xombinormales : MonoBehaviour
{
    public Transform jugador; // Referencia al transform del jugador asignado desde el Inspector
    public float detectionRadius = 10.0f; // Radio de detección para el ataque
    public float attackCooldown = 2f; // Tiempo de espera entre ataques
    public float life = 5f; // Vida del enemigo
    public float deathTime = 2f; // Tiempo hasta desaparecer después de morir
    public AudioClip attackSound; // Clip de sonido para el ataque

    private NavMeshAgent navMeshAgent;
    private Animator animator;
    private AudioSource audioSource;
    private float nextAttackTime = 0f;
    private bool isDead = false;
    private float deathTimer = 0f;

    public static event Action ataque;

    private void Awake()
    {
        // Inicializa los componentes necesarios
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();

        // Asegúrate de que el NavMeshAgent esté activado
        navMeshAgent.enabled = true;
    }

    private void Update()
    {
        if (isDead)
        {
            HandleDeath();
            return; // Salir del método Update si el enemigo está muerto
        }

        navMeshAgent.SetDestination(jugador.position);
        float distanceToPlayer = Vector3.Distance(transform.position, jugador.position);
        // Intentar atacar si está dentro del rango y el cooldown ha pasado
        if (distanceToPlayer < navMeshAgent.stoppingDistance && Time.time >= nextAttackTime)
        {
            AttackPlayer();
        }

     

     
    }

    private void AttackPlayer()
    {
        // Reproducir animación de ataque
        animator.SetTrigger("Attack");
        audioSource.PlayOneShot(attackSound);
        // Reproducir sonido de ataque


        // Establecer el próximo tiempo de ataque
        nextAttackTime = Time.time + attackCooldown;

        // Invocar el evento de ataque si hay suscriptores
        ataque?.Invoke();
    }

    private void HandleDeath()
    {
        deathTimer += Time.deltaTime;
        if (deathTimer >= deathTime)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("bala"))
        {
            life--;
            if (life <= 0)
            {
                Die();
            }
        }
    }

    private void Die()
    {
        isDead = true;
        navMeshAgent.enabled = false; // Desactivar el NavMeshAgent
        animator.SetTrigger("Die"); // Reproducir animación de muerte
    }

    private void OnDrawGizmosSelected()
    {
        // Dibuja un gizmo para visualizar el radio de detección en el editor
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}
