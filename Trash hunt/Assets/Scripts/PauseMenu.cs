using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;
    public static bool isPaused;

    [SerializeField] AudioSource music;

    void Start()
    {
        isPaused = false;
        pauseMenu.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
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
        Time.timeScale = 0f;
        isPaused = true;

        music.Pause();
    }

    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;

        music.Play();
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}