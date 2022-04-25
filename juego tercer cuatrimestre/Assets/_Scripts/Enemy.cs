using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Enemy : Entity
{
    [SerializeField] private Slider slider;


    // Update is called once per frame
    protected override void ArtificialUpdate()
    {
        // Makes slider face towards enemy
        slider.transform.forward = -Camera.main.transform.forward;
        if (Health <= 0)
        {
            Destroy(gameObject);
        }
    }
    

    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);

        // Updates slider value
        slider.value = Health / MaxHealth;
    }
}
