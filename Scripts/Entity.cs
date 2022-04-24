using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Entity : MonoBehaviour
{
	public int health = 100;
	public int maxHealth = 100;
	
	void Start()
	{
		health = maxHealth;
	}


	public virtual void takeDamage(int amount)
	{
		health -= amount;
		
		if (health <= 0)
		{
			Debug.Log("Has Muerto");
			SceneManager.LoadScene("Try Again");
		}
	}
}
