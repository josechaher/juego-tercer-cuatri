using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

// Todos
public class Demon : Enemy
{
    public NavMeshAgent agent;

    public Transform player;

    [SerializeField] Transform projectileSpawn;

    public LayerMask whatIsGround, whatIsPlayer;

    private float health = 75;

    //patrol
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    //attack
    public float timeBetweenAttacks;
    bool attacking;
    bool alreadyAttacked;
    public GameObject projectile;

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
        if (!walkPointSet) SearchWalkPoint();

        if(walkPointSet)
        {
            agent.SetDestination(walkPoint);
            animator.SetBool("Walking", true);
        }

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        // If distance to walkpoint is less than 1, then find another walkpoint.
        if (distanceToWalkPoint.magnitude < 1f)
            walkPointSet = false;
    }

    private void SearchWalkPoint()
    {
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        // Check if there is ground below walkpoint before confirming walkpoint

        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
        {
            NavMeshHit hit;
            if (NavMesh.SamplePosition(walkPoint, out hit, 1f, NavMesh.AllAreas))
            {
                agent.SetDestination(hit.position);
                walkPointSet = true;
                chasing = false;
            }
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

            Invoke(nameof(ThrowProjectile), 1.2f);

            // Wait before next attack
            alreadyAttacked = true;            
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }

    private void ThrowProjectile()
    {
        // Shoots a projectile forwards (towards the enemy)
        Rigidbody rb = Instantiate(projectile, projectileSpawn.position, Quaternion.LookRotation((player.position + Vector3.up * 1.5f) - projectileSpawn.position)).GetComponent<Rigidbody>();
        rb.AddForce(rb.transform.forward * 32f, ForceMode.Impulse);
    }

    private void ResetAttack()
    {
        alreadyAttacked = false;        
    }
}
