using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject press;
    public GameObject pauseMenu;
    public static bool isPaused;
    public static bool canPause;

    [SerializeField] AudioSource music;

    void Start()
    {
        isPaused = false;
        canPause = true;
        pauseMenu.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && canPause == true)
        {
            if (isPaused == false)
            {
                PauseGame();
            }

            else if (isPaused == true)
            {
                ResumeGame();
            }
        }

        if (isPaused == true && Input.GetKeyDown(KeyCode.Escape))
        {
            QuitGame();
        }
    }

    public void PauseGame()
    {
        pauseMenu.SetActive(true);
        press.SetActive(false);
        Time.timeScale = 0f;
        isPaused = true;

        music.Pause();
    }

    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        press.SetActive(true);
        Time.timeScale = 1f;
        isPaused = false;

        music.Play();
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}