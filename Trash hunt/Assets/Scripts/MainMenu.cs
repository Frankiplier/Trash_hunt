using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] GameObject credits;
    bool creditsEnabled;

    public void Start()
    {
        Time.timeScale = 0f;

        credits.SetActive(false);
        creditsEnabled = false;
        
        gameObject.GetComponent<Button>().onClick.AddListener(Credits);
    }

    public void PlayGame()
    {
        SceneManager.LoadSceneAsync(1);
    }

    public void Credits()
    {
        creditsEnabled ^= true;
        credits.SetActive(creditsEnabled);
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
