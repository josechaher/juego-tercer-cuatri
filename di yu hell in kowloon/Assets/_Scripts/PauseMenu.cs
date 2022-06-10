using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;

    public GameObject PauseMenuUI;

    public GameObject OptionsMenuUI;

    public Slider slider;

    public float sliderValue;

    public Image imageMute;

    private void Start()
    {
        slider.value = PlayerPrefs.GetFloat("AudioVolume", 0.5f);
        AudioListener.volume = slider.value;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }

    }

    public void Resume()
    {
        PauseMenuUI.SetActive(false);
        OptionsMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;

        Cursor.lockState = CursorLockMode.Locked;
    }

    void Pause()
    {
        PauseMenuUI.SetActive(true);
        OptionsMenuUI.SetActive(false);
        Time.timeScale = 0f;
        GameIsPaused = true;

        Cursor.lockState = CursorLockMode.None;
    }

    public void Options()
    {
        PauseMenuUI.SetActive(false);
        OptionsMenuUI.SetActive(true);
    }

    public void LoadMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("DiYu"); 
    }

    public void ExitOptions()
    {
        PauseMenuUI.SetActive(true);
        OptionsMenuUI.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
