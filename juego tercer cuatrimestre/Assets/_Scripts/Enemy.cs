using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : Entity
{
    public Slider slider;

    protected override void ArtificialUpdate()
    {
        base.ArtificialUpdate();
        slider.transform.forward = -Camera.main.transform.forward;
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    public override void TakeDamage(float damage)
    {
        health -= damage;

        // Updates slider value
        slider.value = health / maxHealth;
    }
}
