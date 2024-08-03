using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TimeManager : MonoBehaviour
{
    public GameObject winGameOver;
    public GameObject loseGameOver;
    public GameObject trashIcon;
    public GameObject score;
    public GameObject pause;
    public GameObject hearts;

    public const int hoursInDay = 24, minutesInHour = 60;

    public float dayDuration; //ile w grze trwaja 24 h

    float totalTime = 0;
    float currentTime = 0;

    void Start()
    {
        winGameOver.SetActive(false);
        loseGameOver.SetActive(false);
        Time.timeScale = 1f;

        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;

        StartCoroutine(EndGame());
    }


    // Update is called once per frame
    void Update()
    {
        totalTime += Time.deltaTime;
        currentTime = totalTime % dayDuration;

        // if na zamkniecie gry po zostaniu zlapanym
        if (PauseMenu.isPaused == true && Input.GetKeyDown(KeyCode.Escape))
        {
            QuitGame();
        }
    }

    public float GetHour()
    {
        return currentTime * hoursInDay / dayDuration;
    }

    public float GetMinutes()
    {
        return (currentTime * hoursInDay * minutesInHour / dayDuration) % minutesInHour;
    }

    public string Clock24Hour()
    {
        return Mathf.FloorToInt(GetHour()).ToString("00") + ":" + Mathf.FloorToInt(GetMinutes()).ToString("00");
    }

    // kod na ewentualny zegar 12 godzin
    public string Clock12Hour()
    {
        int hour = Mathf.FloorToInt(GetHour());
        string abbreviation = "AM";

        if (hour >= 12)
        {
            abbreviation = "PM";
            hour -= 12;
        }

        if (hour == 0) hour = 12;

        return hour.ToString("00") + ":" + Mathf.FloorToInt(GetMinutes()).ToString("00") + " " + abbreviation;
    }

    private IEnumerator EndGame()
    {
        yield return new WaitForSeconds(720);
        {
            if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Alley"))
            {
                winGameOver.SetActive(true);
                pause.SetActive(false);
                hearts.SetActive(false);

                Time.timeScale = 0f;
                PauseMenu.canPause = false;
                PauseMenu.isPaused = true;
            }

            else if (SceneManager.GetActiveScene() != SceneManager.GetSceneByName("Alley"))
            {
                loseGameOver.SetActive(true);
                pause.SetActive(false);
                hearts.SetActive(false);

                Time.timeScale = 0f;
                PauseMenu.canPause = false;
                PauseMenu.isPaused = true;
            }
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
