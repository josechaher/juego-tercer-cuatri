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
    private Animator textAnimator;

    public ParticleSystem bloodParticles;
    public Collider critCollider;

    protected override void ArtificialAwake()
    {
        textAnimator = damageDisplay.gameObject.GetComponent<Animator>();
    }

    // ArtificialUpdate is called on Entity's update function
    protected override void ArtificialUpdate()
    {
        // Makes slider face enemy
        canvas.transform.forward = -Camera.main.transform.forward;
    }
    

    public override void TakeDamage(float damage, bool crit = false)
    {
        base.TakeDamage(damage, crit);

        UpdateSlider(damage, crit);
    }
    // Updates slider value
    protected void UpdateSlider(float damage, bool crit = false)
    {
        damageDisplay.text = Mathf.RoundToInt(damage).ToString();

        if (crit)
            damageDisplay.color = Color.red;
        else
            damageDisplay.color = Color.white;

        textAnimator.SetTrigger("Fade");

        slider.value = CurrentHealth / MaxHealth;
    }
}
