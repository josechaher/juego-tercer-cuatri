using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Beholder : Enemy
{
    public Transform player;

    public float timeBetweenAttacks;
    bool alreadyAttacked;
    public GameObject beholder_projectile;

    public Transform beholderEye;
    public Transform spawnAttack, spawnAttack1, spawnAttack2, spawnAttack3;

    public LayerMask whatIsPlayer;
     
    public float attackRange;
    public bool playerInAttackRange;

    private static float health = 200;

    public bool isAllEnemiesDied;

    private void Awake()
    {
        player = GameObject.Find("Player").transform;
        SetHealth(health);
    }

    private void Update()
    {
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);
        if (playerInAttackRange)
            AttackPlayer();

        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        if (enemies.Length == 0 && isAllEnemiesDied == true)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }

        for (int i = 0; i < enemies.Length; i++)
        {
            if (enemies[i] != null && isAllEnemiesDied == false)
            {
                isAllEnemiesDied = false;
                // Debug.Log("There are a " + enemies.Length + " enemy alive =(");
            }
        }
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
