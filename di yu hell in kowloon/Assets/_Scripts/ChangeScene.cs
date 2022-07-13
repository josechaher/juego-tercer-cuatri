using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Rodrigo Chiale, Jose Chaher, Pedro Chiswell
public class ChangeScene : MonoBehaviour
{
    public static int currentLevelSceneIndex;

    public void LoadScene(string nameScene)
    {
        SceneManager.LoadScene(nameScene);
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(currentLevelSceneIndex);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Keypad1))
        {
            LoadScene("Level 1 - Roro");
        }

        if (Input.GetKeyDown(KeyCode.Keypad2))
        {
            LoadScene("Easter Egg");
        }

        if (Input.GetKeyDown(KeyCode.Keypad3))
        {
            LoadScene("Level 2");
        }
        
        if (Input.GetKeyDown(KeyCode.Keypad4))
        {
            LoadScene("Level 3");
        }
    }

    public static IEnumerator DelaySceneChange(string nameScene, float seconds) {
        yield return new WaitForSeconds(seconds);
        SceneManager.LoadScene(nameScene);
    }
}
