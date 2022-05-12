using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beholder : Enemy
{
    public Transform player;

    public float timeBetweenAttacks;
    bool alreadyAttacked;
    public GameObject beholder_projectile;
    
    public Transform beholderEye;
    public Transform spawnAttack;

    public LayerMask whatIsPlayer;
     
    public float attackRange;
    public bool playerInAttackRange;

    public float health;

    private void Awake()
    {
        player = GameObject.Find("Player").transform;
    }

    private void Update()
    {

        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);
        if (playerInAttackRange)
            AttackPlayer();
    }

    private void AttackPlayer()
    {
        transform.LookAt(player);

        if(!alreadyAttacked)
        {
            Instantiate(beholder_projectile, spawnAttack.position, Quaternion.identity);
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
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
