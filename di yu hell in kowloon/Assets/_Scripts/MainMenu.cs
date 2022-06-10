using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public GameObject MainMenuUI;
    
    public GameObject InstructionsUI;

    public GameObject CreditsUI;

    public GameObject LevelsUI;

    public void Play()
    {
        SceneManager.LoadScene("Level 1 - Roro");
    }

    public void Menu()
    {
        MainMenuUI.SetActive(true);
        InstructionsUI.SetActive(false);
        CreditsUI.SetActive(false);
        LevelsUI.SetActive(false);
    }

    public void Instructions()
    {
        MainMenuUI.SetActive(false);
        InstructionsUI.SetActive(true);
        CreditsUI.SetActive(false);
        LevelsUI.SetActive(false);
    }

    public void Levels()
    {
        MainMenuUI.SetActive(false);
        InstructionsUI.SetActive(false);
        CreditsUI.SetActive(false);
        LevelsUI.SetActive(true);
    }

    public void Credits()
    {
        MainMenuUI.SetActive(false);
        InstructionsUI.SetActive(false);
        CreditsUI.SetActive(true);
        LevelsUI.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
