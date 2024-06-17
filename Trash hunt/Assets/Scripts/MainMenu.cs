using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] GameObject credits;


    public void Start()
    {
        Time.timeScale = 0f;
        
        credits.SetActive(false);
    }

    public void PlayGame()
    {
        SceneManager.LoadSceneAsync(1);
    }

    public void Credits()
    {
        credits.SetActive(true);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    void OnDisable()
    {
        Time.timeScale = 1f;
    }
}
