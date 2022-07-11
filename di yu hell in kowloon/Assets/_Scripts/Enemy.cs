using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Todos
public abstract class Enemy : Entity
{
    [SerializeField] private Canvas canvas;
    [SerializeField] private Slider slider;
    [SerializeField] private Text damageDisplay;
    public ParticleSystem bloodParticles;
    public Collider critCollider;

    // ArtificialUpdate is called on Entity's update function
    protected override void ArtificialUpdate()
    {
        // Makes slider face enemy
        canvas.transform.forward = -Camera.main.transform.forward;
    }
    

    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);

        UpdateSlider(damage);
    }
    // Updates slider value
    protected void UpdateSlider(float damage)
    {
        damageDisplay.text = Mathf.RoundToInt(damage).ToString();
        damageDisplay.color = Color.white;
        slider.value = CurrentHealth / MaxHealth;
    }
}
