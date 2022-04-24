using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    // Start is called before the first frame update
    public int damage = 25;

    private void OnCollisionEnter(Collision collision)
    {
            Entity health = collision.gameObject.GetComponent<Entity>();
            if (health != null)
            {
                health.takeDamage(damage);
                GetComponent<AudioSource>().enabled = true;
            }
    }
}
