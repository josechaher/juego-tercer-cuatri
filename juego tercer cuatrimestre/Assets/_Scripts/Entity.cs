using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Entity : MonoBehaviour
{
	public float health = 100;
	public float maxHealth = 100;
	
	void Start()
	{
		health = maxHealth;
	}


	public virtual void TakeDamage(float damage)
	{
		health -= damage;
	}
}
