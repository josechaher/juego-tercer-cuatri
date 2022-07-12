using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Diego Salazar
public class Beholder : Enemy
{
    public Transform player;

    public float timeBetweenAttacks;
    bool alreadyAttacked;
    public GameObject beholder_projectile;

    public Transform spawnAttack, spawnAttack1, spawnAttack2, spawnAttack3;

    public LayerMask whatIsPlayer;
     
    public float attackRange;
    public bool playerInAttackRange;

    private float health = 200;

    public ParticleSystem explodeParticles;

    protected override void ArtificialAwake()
    {
        base.ArtificialAwake();
        player = GameObject.Find("Player").transform;
        SetHealth(health);
    }

    protected override void ArtificialUpdate()
    {
        base.ArtificialUpdate();
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

            Instantiate(beholder_projectile, spawnAttack1.position, Quaternion.identity);
            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);

            Instantiate(beholder_projectile, spawnAttack2.position, Quaternion.identity);
            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);

            Instantiate(beholder_projectile, spawnAttack3.position, Quaternion.identity);
            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }

    public override void TakeDamage(float damage, bool crit = false)
    {
        if (crit)
            damage *= 2;

        CurrentHealth -= damage;
        UpdateSlider(damage, crit);

        if (CurrentHealth <= 0)
        {
            Instantiate(explodeParticles, transform.position, Quaternion.identity, null);
            FindObjectOfType<Level3Events>().BeholderDestroyed();
            Destroy(gameObject);
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
