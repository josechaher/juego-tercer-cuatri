using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Entity : MonoBehaviour
{
	protected float health = 100;
	protected float maxHealth = 100;
	
	void Start()
	{
		SetHealth();
	}

    private void Update()
    {
		ArtificialUpdate();
    }

    public virtual void TakeDamage(float damage)
	{
		health -= damage;
	}

	protected virtual void ArtificialUpdate() { }

	protected virtual void SetHealth() { }

}
