using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Todos
public abstract class Entity : MonoBehaviour, IDamageable<float>
{
	protected float CurrentHealth { get; set; } // Entity's current health
	protected float MaxHealth { get; set; } // Entity's maximum health

    private void Awake()
    {
		ArtificialAwake();
    }

    private void Update()
    {
		ArtificialUpdate();
    }

	/// <summary>
	/// Sets Entity's maximum health and makes that its current health
	///</summary>
	/// <param name="h"></param>
	protected void SetHealth(float h)
    {
		MaxHealth = h;
		CurrentHealth = MaxHealth;
    }

	protected abstract void ArtificialAwake();
	protected abstract void ArtificialUpdate();

    public virtual void TakeDamage(float damage, bool crit = false)
	{
		CurrentHealth -= damage;

		if (CurrentHealth <= 0)
		{
			Destroy(gameObject);
		}
	}
}
