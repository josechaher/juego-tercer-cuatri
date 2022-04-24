using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{

    public float health;
    public float maxHealth = 100;

    public Slider slider;

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        slider.transform.forward = -Camera.main.transform.forward;
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void TakeDamage(float damage = 40)
    {
        health -= damage;
        slider.value = health / maxHealth;
    }
}
