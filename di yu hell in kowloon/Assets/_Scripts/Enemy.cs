using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Enemy : Entity
{
    [SerializeField] private Slider slider;

    // ArtificialUpdate is called on Entity's update function
    protected override void ArtificialUpdate()
    {
        // Makes slider face enemy
        slider.transform.forward = -Camera.main.transform.forward;
    }
    

    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);

        // Updates slider value
        slider.value = CurrentHealth / MaxHealth;
    }
}
