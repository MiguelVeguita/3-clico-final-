using System;
using UnityEngine;
using UnityEngine.AI;

public class xombinormales : MonoBehaviour
{
    public Transform jugador; 
    public float detectionRadius = 10.0f; 
    public float attackCooldown = 2f; 
    public float life = 5f; 
    public float deathTime = 2f; 
    public AudioClip attackSound; 

    private NavMeshAgent navMeshAgent;
    private Animator animator;
    private AudioSource audioSource;
    private float nextAttackTime = 0f;
    private bool isDead = false;
    private float deathTimer = 0f;

    public static event Action ataque;

    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();

        navMeshAgent.enabled = true;
    }

    private void Update()
    {
        if (isDead)
        {
            HandleDeath();
            return; 
        }

        navMeshAgent.SetDestination(jugador.position);
        float distanceToPlayer = Vector3.Distance(transform.position, jugador.position);
        if (distanceToPlayer < navMeshAgent.stoppingDistance && Time.time >= nextAttackTime)
        {
            AttackPlayer();
        }

     

     
    }

    private void AttackPlayer()
    {
        animator.SetTrigger("Attack");
        audioSource.PlayOneShot(attackSound);


        nextAttackTime = Time.time + attackCooldown;

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
        navMeshAgent.enabled = false; 
        animator.SetTrigger("Die"); 
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}
