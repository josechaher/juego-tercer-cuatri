using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Enemy : Entity
{
    [SerializeField] private Slider slider;
    public ParticleSystem bloodParticles;
    public Collider critCollider;

    // ArtificialUpdate is called on Entity's update function
    protected override void ArtificialUpdate()
    {
        // Makes slider face enemy
        slider.transform.forward = -Camera.main.transform.forward;
    }
    

    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);

        UpdateSlider();
    }
    // Updates slider value
    protected void UpdateSlider()
    {
        slider.value = CurrentHealth / MaxHealth;
    }
}
