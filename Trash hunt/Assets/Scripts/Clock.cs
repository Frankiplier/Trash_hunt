using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Clock : MonoBehaviour
{
    TimeManager tm;
    
    public RectTransform minuteArm;
    public RectTransform hourArm;

    const float hoursToDegrees = 360/12, minutesToDegrees = 360/60;

    void Start()
    {
        tm = FindObjectOfType<TimeManager>();
    }

    void Update()
    {
        hourArm.rotation = Quaternion.Euler(0, 0, -tm.GetHour()*hoursToDegrees);
        minuteArm.rotation = Quaternion.Euler(0, 0, -tm.GetMinutes()*minutesToDegrees);

        // if na zmiane sceny po uplywie okreslonego czasu
        // na potem: zrobic drugi if i dodac warunek zaliczenia wszystkich czynnosci
        //if (hourArm.rotation == Quaternion.Euler(0, 0, -180))
        //{
        //    SceneManager.LoadSceneAsync(3);
        //}
    }
}
