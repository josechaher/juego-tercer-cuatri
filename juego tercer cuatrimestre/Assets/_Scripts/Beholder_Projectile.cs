using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beholder_Projectile : MonoBehaviour
{
    public float speed = 3;
    Transform player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider collider)
    {
        Ball ball = collider.gameObject.GetComponent<Ball>();
        Player player = collider.gameObject.GetComponent<Player>();
        if (player)
        {
            player.TakeDamage(20);
            Debug.Log("Le pegue al player.");
            Destroy(gameObject);

        }
        else if (ball)
        {
            Destroy(gameObject);
        }
    }
}
