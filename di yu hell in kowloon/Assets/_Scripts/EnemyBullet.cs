using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Alguien
public class EnemyBullet : MonoBehaviour
{
    [SerializeField] float damage = 25;
    private void OnTriggerEnter(Collider other)
    {
        Player player = other.GetComponent<Player>();

        if (player)
        {
            player.TakeDamage(damage);
        }
    }
}
