using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beholder_Projectile : MonoBehaviour
{
    public float speed = 3;
    Transform player;
    public ParticleSystem explosion;
    public ParticleSystem trail;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        trail.Play();
    }

    private void Update()
    {
        transform.LookAt(player);
        transform.position = Vector3.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
        
    }

    private void OnTriggerEnter(Collider collider)
    {
        Ball ball = collider.gameObject.GetComponent<Ball>();
        Player player = collider.gameObject.GetComponent<Player>();
        if (player)
        {
            Instantiate(explosion, transform.position, Quaternion.identity);
            player.TakeDamage(20);
            Debug.Log("Le pegue al player.");
            Destroy(gameObject);

        }
        else if (ball)
        {
            Instantiate(explosion, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        
    }
}
