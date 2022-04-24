using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : Entity

{
	public int points;

	private void OnTriggerEnter(Collider collision)
	{
		if (collision.CompareTag("Enemy"))
		{
			Destroy(gameObject, 0.5f);
			SceneManager.LoadScene("Try Again");
		}
	}

	/*public void OnGUI()
	{
		GUI.Label(new Rect(10, 10, 100, 20), "Coins : " + points);
		if (points >= 8)
		{
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
		}
	}	
	*/
}


