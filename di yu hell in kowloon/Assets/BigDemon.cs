using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BigDemon : Enemy
{
    public NavMeshAgent agent;

    public Transform player;

    public LayerMask whatIsGround, whatIsPlayer;

    [SerializeField] private float health = 150;

    [SerializeField] private GameObject splash;

    //patrol
    [SerializeField] private Vector3 walkPoint1;
    [SerializeField] private Vector3 walkPoint2;
    private Vector3 currentWalkPoint;

    bool walkPointSet;
    public float walkPointRange;

    //attack
    public float timeBetweenAttacks;
    bool attacking;
    bool alreadyAttacked;

    bool chasing = false;

    //States
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;

    Animator animator;

    protected override void ArtificialAwake()
    {
        base.ArtificialAwake();
        player = FindObjectOfType<Player>().transform;
        agent = GetComponent<NavMeshAgent>();

        walkPoint1.y = transform.position.y;
        walkPoint2.y = transform.position.y;

        currentWalkPoint = walkPoint1;
        agent.SetDestination(currentWalkPoint);

        animator = GetComponent<Animator>();
        SetHealth(health);
    }

    protected override void ArtificialUpdate()
    {
        base.ArtificialUpdate();

        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        // If player is not in Sight, patrol
        if (!playerInSightRange && !playerInAttackRange) Patroling();

        // If player is in sight, chase
        if (playerInSightRange && !playerInAttackRange && !alreadyAttacked) ChasePlayer();

        // If player is in range, attack
        if (playerInAttackRange && playerInSightRange) AttackPlayer();
    }

    private void Patroling()
    {
        agent.SetDestination(currentWalkPoint);

        Vector3 distanceToWalkPoint = transform.position - currentWalkPoint;

        // If distance to walkpoint is less than 1, then find another walkpoint.
        if (distanceToWalkPoint.magnitude < 1f)
        {
            animator.SetBool("Walking", true);

            if (currentWalkPoint == walkPoint1)
            {
                currentWalkPoint = walkPoint2;
            }
            else
            {
                currentWalkPoint = walkPoint1;
            }

            agent.SetDestination(currentWalkPoint);
        }
    }

    private void ChasePlayer()
    {
        if (!chasing)
        {
            attacking = false;
            chasing = true;
            animator.SetBool("Walking", true);
        }

        if (chasing)
        {
            agent.SetDestination(player.position);
        }
    }

    private void AttackPlayer()
    {
        if (!attacking)
        {
            attacking = true;
            chasing = false;
            animator.SetBool("Walking", false);
        }

        transform.LookAt(player);

        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);

        if (!alreadyAttacked)
        {
            animator.SetTrigger("Attack");

            agent.SetDestination(transform.position);

            // Attack
            Instantiate(splash, transform.position + Vector3.up * (splash.transform.localScale.y/2), Quaternion.identity, transform);

            // Wait before next attack
            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }

    private void ResetAttack()
    {
        alreadyAttacked = false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(walkPoint1, 1f);
        Gizmos.DrawWireSphere(walkPoint2, 1f);
    }
}
