using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Digital : MonoBehaviour
{
    TimeManager tm;
    TMP_Text display;

    // Start is called before the first frame update
    void Start()
    {
        tm = FindObjectOfType<TimeManager>();
        display = GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        display.text = tm.Clock24Hour();
    }
}
