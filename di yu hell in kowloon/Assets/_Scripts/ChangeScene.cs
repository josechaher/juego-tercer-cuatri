using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
}
