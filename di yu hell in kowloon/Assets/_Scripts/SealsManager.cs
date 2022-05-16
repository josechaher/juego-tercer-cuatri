using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SealsManager : MonoBehaviour
{
    
    public int points;

    public void OnGUI()
    {
        GUI.Label(new Rect(10, 10, 100, 20), "Seals : " + points);
        if (points >= 3)
        {
            SceneManager.LoadScene("Coming Soon");
        }
    }
}
