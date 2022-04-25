using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public abstract class Entity : MonoBehaviour
{
	protected float Health { get; set; }
	protected float MaxHealth { get; set; }
	
	private void Start()
	{
		Health = MaxHealth;
		print(MaxHealth);
	}

    private void Update()
    {
		ArtificialUpdate();
    }

	protected abstract void ArtificialUpdate();


    public virtual void TakeDamage(float damage)
	{
		Health -= damage;
	}
}
